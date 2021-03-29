using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PathTemplate", menuName = "ScriptableObjects/PathTemplates", order = 1)]
public class PathTemplate : ScriptableObject {
    [Serializable] public struct DirectionAndPrice {
        public string info;
        public Vector3 direction;
        public float price;
    }

    public DirectionAndPrice[] directionAndPrices; 

}