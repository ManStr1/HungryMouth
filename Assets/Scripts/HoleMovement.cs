using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour {

    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [SerializeField] Vector2 moveLimits;
    [SerializeField] float radius;
    [SerializeField] Transform holeCenter;

    [SerializeField] float moveSpeed;

    Mesh mesh;
    List<int> holeVertices;
    List<Vector3> offsets;
    int holeVerticesCount;

    float x, y;
    Vector3 touch, targetPos;

    void Start() {
        Game.isMoving = false;
        Game.isGameOver = false;

        holeVertices = new List<int>();
        offsets = new List<Vector3>();

        mesh = meshFilter.mesh;

        moveSpeed = DataProcessController.Instance.sense * 10;

        FindHoleVertices();
    }

    void Update() {
        #if UNITY_EDITOR
        Game.isMoving = Input.GetMouseButton(0);

        if (!Game.isGameOver && Game.isMoving) {
            MoveHole();
            UpdateHoleVerticesPosition();
        }
        #else
        Game.isMoving = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;

                if (!Game.isGameOver && Game.isMoving) {
                    MoveHole();
                    UpdateHoleVerticesPosition();
                }
        #endif


    }

    void MoveHole() {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(
            holeCenter.position,
            holeCenter.position + new Vector3(x, 0f, y),
            moveSpeed * Time.deltaTime
        );

        targetPos = new Vector3(
            Mathf.Clamp(touch.x, -5.8f, 6.8f),
            touch.y,
            Mathf.Clamp(touch.z, -16.5f, 6.57f)
        );

        holeCenter.position = targetPos;
    }

    void UpdateHoleVerticesPosition() {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++) {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }

        mesh.vertices = vertices;
        meshFilter.mesh = mesh;

        meshCollider.sharedMesh = mesh;
    }

    void FindHoleVertices() {
        for (int i = 0; i < mesh.vertices.Length; i++) {

            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);

            if (distance < radius) {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }

        holeVerticesCount = holeVertices.Count;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(holeCenter.position, radius);
    }
}
