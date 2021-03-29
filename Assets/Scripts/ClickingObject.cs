using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickingObject : MonoBehaviour {

    private Camera mainCamera;

    public UnityAction<Transform> unityAction;

    private void Start() {
        mainCamera = Camera.main;
    }

    RaycastHit hit;
    Ray ray;
    private void Update() {
        MyRaycast();
    }

    private void MyRaycast() {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {
                unityAction.Invoke(hit.transform);
            }
        }
    }
}
