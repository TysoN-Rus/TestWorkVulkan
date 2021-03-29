using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSceneTwo : MonoBehaviour {

    public void NextScene(string str) {
        Settings.Instance.NextScene(str);
    }

}
