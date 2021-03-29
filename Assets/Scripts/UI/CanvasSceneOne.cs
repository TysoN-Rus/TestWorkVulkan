using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSceneOne : MonoBehaviour {

    [SerializeField] private TMP_InputField meshSize;
    [SerializeField] private TMP_InputField cubeSize;
    [SerializeField] private Slider randomHole;

    private const string nextScene = "SceneTwo";

    private void Start() {
        meshSize.text = Settings.Instance.generatorSettings.sideLength.ToString();
        cubeSize.text = Settings.Instance.generatorSettings.sizeCube.ToString();
        randomHole.value = Settings.Instance.generatorSettings.randomHole;
    }

    public void SetLength(string str) {
        Settings.Instance.generatorSettings.sideLength = int.Parse(str);
    }
    public void SetSizeCube(string str) {
        Settings.Instance.generatorSettings.sizeCube = float.Parse(str);
    }
    public void SetRandomHole(float val) {
        Settings.Instance.generatorSettings.randomHole = val;
    }

    public void NewGenerator() {
        Settings.Instance.NewGenerator();
        Settings.Instance.NextScene(nextScene);
    }

    [SerializeField] private GameObject errorText;
    public void LoadGeneratorSettings() {
        if (Settings.Instance.LoadGeneratorSettings()) {
            Settings.Instance.NextScene(nextScene);
        } else {
            errorText.SetActive(true);
        }
        
    }

    public void Exit() {
        Application.Quit();
    }

}
