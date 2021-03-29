using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini : MonoBehaviour {

    public PathTemplate path;
    [ContextMenu("bee")]
    void bee() {
        int a = 0;
        for (int i = -1; i < 2; i++) {
            for (int j = -1; j < 2; j++) {
                path.directionAndPrices[a].direction = new Vector3(i, j);
                a++;
            }
        }
    }

}
