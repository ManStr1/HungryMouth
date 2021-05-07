using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
    [HideInInspector] new public Rigidbody rigidbody;

    public bool useGravity = true;

    void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rigidbody.useGravity = false;
        if (useGravity) rigidbody.AddForce(Physics.gravity * (rigidbody.mass * rigidbody.mass));
    }
}
