using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public struct DataCube {
    public string name;
    public Vector3 positon;

    public DataCube(Vector3 positon) : this() {
        this.positon = positon;
        name = "Cube" + "CorX:" + positon.x + "CorY:" + positon.y;
    }
}
[Serializable]
public class GeneratorSettings {
    [HideInInspector] public List<DataCube> data;
    public int sideLength;
    public float sizeCube;
    [Range(0,1)] public float randomHole;

    public GeneratorSettings() {
        data = new List<DataCube>();
    }
}