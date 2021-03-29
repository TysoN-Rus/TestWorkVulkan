using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    [SerializeField] private Material matBase;
    [SerializeField] private Material matGreen;
    [SerializeField] private Material matRed;

    private LineRenderer line;

    private Dictionary<Transform, float> allPoints = new Dictionary<Transform, float>();

    [SerializeField] private PathTemplate template;


    private void Start() {
        GetComponent<ClickingObject>().unityAction += SelectGO;
        line = GetComponent<LineRenderer>();
    }

    private Transform selectEndPoint;
    private Transform selectStartPoint;

    private void SelectGO(Transform target) {
        if (selectEndPoint == target) {
            ResetEndPoint();
            return;
        }

        if (allPoints.Count == 0) {
            allPoints.Add(target, 0);
            SetMaterialGO(target, matGreen);
            PathCalculation(target);
            selectStartPoint = target;
        } else if (allPoints.ContainsKey(target)) {
            if (allPoints[target] == 0) {
                SetMaterialGO(selectStartPoint, matBase);
                selectStartPoint = null;
                allPoints.Clear();
                ResetEndPoint();
            } else {
                ResetEndPoint();
                selectEndPoint = target;
                SetMaterialGO(target, matGreen);

                SetLinePoints(SearchPath(selectEndPoint));
            }

        } else {
            ResetEndPoint();
            selectEndPoint = target;
            SetMaterialGO(target, matRed);
        }

    }

    private List<Vector3> SearchPath(Transform end) {
        Transform nextPoint;
        List<Vector3> pathPoints = new List<Vector3>();
        Transform temp = end;
        Collider[] colliders;
        float sizeCube = end.localScale.x;

        do {
            nextPoint = temp;
            pathPoints.Add(nextPoint.position + Vector3.back * sizeCube / 1.99f);
            for (int i = 0; i < template.directionAndPrices.Length; i++) {
                Vector3 newPos = nextPoint.position + template.directionAndPrices[i].direction * sizeCube;
                colliders = Physics.OverlapSphere(newPos, sizeCube / 10f);
                if (colliders.Length > 0) {
                    if (allPoints[temp] > allPoints[colliders[0].transform]) {
                        temp = colliders[0].transform;
                    }
                }
            }
        } while (allPoints[nextPoint] != 0);
        return pathPoints;
    }

    private void SetLinePoints(List<Vector3> pathPoints) {
        line.positionCount = pathPoints.Count;
        line.SetPositions(pathPoints.ToArray());
    }

    private void PathCalculation(Transform start) {
        List<Transform> cells = new List<Transform>();
        cells.Add(start);
        float sizeCube = start.localScale.x;
        Collider[] colliders;
        Transform temp;
        int k = 0;
        while (cells.Count > k) {
            for (int i = 0; i < template.directionAndPrices.Length; i++) {
                Vector3 newPos = cells[k].position + template.directionAndPrices[i].direction * sizeCube;
                colliders = Physics.OverlapSphere(newPos, sizeCube / 10f);
                if (colliders.Length > 0) {
                    temp = colliders[0].transform;
                    if (!allPoints.ContainsKey(temp)) {
                        allPoints.Add(temp, allPoints[cells[k]] + template.directionAndPrices[i].price);
                        cells.Add(temp);
                    } else if (allPoints[temp] > allPoints[cells[k]] + template.directionAndPrices[i].price) {
                        allPoints[temp] = allPoints[cells[k]] + template.directionAndPrices[i].price;
                        cells.Add(temp);
                    }
                }
            }
            k++;
#if UNITY_EDITOR
            if (k > 100000) {
                Debug.LogError("Ты слишком зациклился на себе");
                return;
            }
#endif
        }
    }

    //private void ResetPath() {
    //    ResetEndPoint();
    //}

    private void ResetEndPoint() {
        if (selectEndPoint) {
            SetMaterialGO(selectEndPoint, matBase);
            selectEndPoint = null;
        }
        SetLinePoints(new List<Vector3>());
    }

    private void SetMaterialGO(Transform target, Material mt) {
        Renderer renderer = target.GetComponent<Renderer>();
        renderer.material = mt;
    }

    public void SetPathTemplate(PathTemplate val) {
        template = val;
        if (selectStartPoint) {
            allPoints.Clear();
            allPoints.Add(selectStartPoint, 0);
            PathCalculation(selectStartPoint);

            if (selectEndPoint) {
                if (allPoints.ContainsKey(selectEndPoint)) {
                    SetLinePoints(SearchPath(selectEndPoint));
                    SetMaterialGO(selectEndPoint, matGreen);
                } else {
                    SetLinePoints(new List<Vector3>());
                    SetMaterialGO(selectEndPoint, matRed);
                }
            }
        }

    }

    private void OnDestroy() {
        GetComponent<ClickingObject>().unityAction -= SelectGO;
    }
}
