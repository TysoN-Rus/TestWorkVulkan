using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ForSetText : MonoBehaviour {

    private TextMeshProUGUI text;

    private void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string str) {
        text.text = str;
    }

    public void SetText(float val) {
        text.text = val.ToString("0.00");
    }

}
