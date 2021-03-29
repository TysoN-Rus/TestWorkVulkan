using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    [SerializeField] private GameObject prefabCube;

    private GeneratorSettings generator;

    private void Start() {
        generator = Settings.Instance.GetGeneratorSettings();

        if (generator.data.Count == 0) {
            NewGenerate(prefabCube);
        } else {
            LoadGenerate();
        }
    }

    private void LoadGenerate() {
        print("LoadGenerate");
        GameObject parent = CreateParent(generator.sideLength);
        GameObject temp;
        for (int i = 0; i < generator.data.Count; i++) {
            temp = Instantiate(prefabCube, generator.data[i].positon, Quaternion.identity, parent.transform);
            temp.name = generator.data[i].name;
        }
    }

    private GameObject CreateParent(int count) {
        prefabCube.transform.localScale = Vector3.one * generator.sizeCube;

        GameObject parent = new GameObject("ParentCubes");
        float center = (count - 1) / 2f * generator.sizeCube;
        parent.transform.position = new Vector3(center, center, 0);

        //////
        Camera.main.transform.position = new Vector3(center, center, -count * generator.sizeCube);
        if (generator.sizeCube < 0) {
            Camera.main.transform.eulerAngles = Vector3.up * 180;
        }
        //////
        return parent;
    }

    private void NewGenerate(GameObject prefab) {
        print("NewGenerate");
        GameObject parent = CreateParent(generator.sideLength);
        GameObject temp;
        for (int y = 0; y < generator.sideLength; y++) {
            for (int x = 0; x < generator.sideLength; x++) {

                float rand = Settings.Instance.MyRand(1f);
                if (rand >= generator.randomHole) {
                    DataCube cube = new DataCube(new Vector3(x, y, 0) * generator.sizeCube);

                    temp = Instantiate(prefab, cube.positon, Quaternion.identity, parent.transform);
                    temp.name = cube.name;

                    generator.data.Add(cube);
                }
            }
        }

        Settings.Instance.SaveGeneratorSettings(generator);
    }
}
