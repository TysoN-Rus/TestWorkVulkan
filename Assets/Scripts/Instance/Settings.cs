using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Settings : MonoBehaviour {

    public static Settings Instance { private set; get; }

    private ISave save;
    private ILoad load;

    public GeneratorSettings generatorSettings = new GeneratorSettings();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
            return;
        }

        save = GetComponent<ISave>();
        load = GetComponent<ILoad>();

        Random.InitState((int)DateTime.Now.Ticks);

        DontDestroyOnLoad(this);
    }

    private const string nameFileSave = "DataCubes";

    public bool LoadGeneratorSettings() {
        string str = load.Load(nameFileSave);
        if (str != "") {
            generatorSettings = JsonUtility.FromJson<GeneratorSettings>(str);
            return true;
        }
        return false;
    }
    public void NewGenerator() {
        generatorSettings.data.Clear();
    }

    public void SaveGeneratorSettings(GeneratorSettings val) {
        generatorSettings = val;
        save.Save(nameFileSave, JsonUtility.ToJson(generatorSettings));
    }

    public GeneratorSettings GetGeneratorSettings() {
        return generatorSettings;
    }

    public void NextScene(string name) {
        SceneManager.LoadScene(name);
    }
    public void NextScene(int val) {
        SceneManager.LoadScene(val);
    }

    #region MyRandom

    public float MyRand() {
        return Random.value;
    }

    public float MyRand(float val) {
        return Random.Range(0, val);
    }

    public int MyRand(int val) {
        return Random.Range(0, val);
    }

    public float MyRand(float min, float max) {
        return Random.Range(min, max);
    }

    public int MyRand(int min, int max) {
        return Random.Range(min, max);
    }

    public Vector2 OnUnitCircle() {
        return Random.insideUnitCircle.normalized;
    }

    #endregion
}
