using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour, ISave, ILoad{

    public void Save(string name, string str) {
        Debug.Log(Application.persistentDataPath + "/" + name + ".json");
        StreamWriter sw = File.CreateText(Application.persistentDataPath + "/" + name + ".json");
        sw.WriteLine(str);
        sw.Close();
    }
    public string Load(string name) {
        if (File.Exists(Application.persistentDataPath + "/" + name + ".json")) {
            string str;
            StreamReader sr = File.OpenText(Application.persistentDataPath + "/" + name + ".json");
            str = sr.ReadLine();
            sr.Close();
            return str;
        } else {
            return "";
        }
    }
}
