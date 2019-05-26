using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Cube
{
    public float height;
}


public class AssetCreatConfig :ScriptableObject 
{
    // Start is called before the first frame update
    [HideInInspector]
    public float hp;
    public int x;

    public Cube cube; 
}
