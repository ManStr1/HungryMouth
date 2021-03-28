using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundCollision : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        string tag = other.tag;

        if (tag.Equals("Food")) {
            Destroy(other.gameObject);
            Debug.Log("food");
        }
        if (tag.Equals("Item")) {
            Destroy(other.gameObject);
            Debug.Log("item");
        }
    }
}
