using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class ConfigTools 
{
    [MenuItem("config/player")]
    static void createPlayerConfig()
    {
        Prefstest prefstest = new Prefstest();
        //AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AssetCreatConfig>(), "Assets/player.asset");
        prefstest.SaveFile();
        Debug.Log("111");
    }
}
