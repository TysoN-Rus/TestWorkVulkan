using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateCounter : MonoBehaviour {

    TextMesh text;

    public int countDeltaFrame = 5;
    float stateFrame = 0;
    float backFrame;


    private void Start() {
        text = GetComponent<TextMesh>();
        if (!text) {
            Debug.LogWarning("Нет текстового поля");
            enabled = false;
        }
        stateFrame = Time.deltaTime * countDeltaFrame;
        backFrame = Time.deltaTime;
    }
    
    void Update () {
        stateFrame -= backFrame;
        backFrame = Time.deltaTime;
        stateFrame += backFrame;
        PrintFPS(stateFrame / countDeltaFrame);
    }

    void PrintFPS(float count) {
        text.text = ((int) (1f /count)).ToString();
    }
    
}
