using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPoint : MonoBehaviour {

    [SerializeField] private float speed = 1;
    private List<Transform> circle = new List<Transform>();

    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            circle.Add(transform.GetChild(i));
        }
    }

    void Update() {
        for (int i = 0; i < circle.Count; i++) {
            circle[i].Rotate(Vector3.forward * speed * (i / 2 == 0 ? 1 : -1));
        }
        if (Input.GetMouseButtonDown(0)) {
            transform.position = Input.mousePosition;
        }
    }
}
