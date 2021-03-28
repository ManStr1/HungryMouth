using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour {
    [Header("Hole mesh")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [Header("Hole vertices radius")]
    [SerializeField] Vector2 moveLimits;
    //Hole vertices radius from the hole's center
    [SerializeField] float radius;
    [SerializeField] Transform holeCenter;

    [Space]
    [SerializeField] float moveSpeed;

    Mesh mesh;
    List<int> holeVertices;
    //hole vertices offsets from hole center
    List<Vector3> offsets;
    int holeVerticesCount;

    float x, y;
    Vector3 touch, targetPos;

    // Start is called before the first frame update
    void Start() {
        Game.isMoving = false;
        Game.isGameOver = false;

        //Initializing lists
        holeVertices = new List<int>();
        offsets = new List<Vector3>();

        //get the meshFilter's mesh
        mesh = meshFilter.mesh;

        //Find Hole vertices on the mesh
        FindHoleVertices();
    }

    // Update is called once per frame
    void Update() {
        Game.isMoving = Input.GetMouseButton(0);

        if (!Game.isGameOver && Game.isMoving) {
            //Move hole center
            MoveHole();
            //Update hole vertices
            UpdateHoleVerticesPosition();
        }
    }

    void MoveHole() {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        //lerp (smooth) movement
        touch = Vector3.Lerp(
            holeCenter.position,
            holeCenter.position + new Vector3(x, 0f, y), //move hole on x & z 
            moveSpeed * Time.deltaTime
        );

        targetPos = new Vector3(
            Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x),
            touch.y,
            Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y)
        );

        holeCenter.position = targetPos;
    }

    void UpdateHoleVerticesPosition() {
        //Move hole vertices
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++) {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }

        //update mesh vertices
        mesh.vertices = vertices;
        //update meshFilter's mesh
        meshFilter.mesh = mesh;
        //update collider
        meshCollider.sharedMesh = mesh;
    }

    void FindHoleVertices() {
        for (int i = 0; i < mesh.vertices.Length; i++) {
            //Calculate distance between holeCenter & each Vertex
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);

            if (distance < radius) {
                //this vertex belongs to the Hole
                holeVertices.Add(i);
                //offset: how far the Vertex from the HoleCenter
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }
        //save hole vertices count
        holeVerticesCount = holeVertices.Count;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(holeCenter.position, radius);
    }
}
