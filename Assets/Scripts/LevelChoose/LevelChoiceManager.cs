using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelChoiceManager : MonoBehaviour
{
    public Text playername;
    public Text playermoney;
    private void Awake()
    {
        playername.text = PlayerPrefs.GetString("playername");
        playermoney.text = PlayerPrefs.GetInt("money").ToString();
    }

    public void Level_1()
    {
        PlayerPrefs.SetString("bg_stage", "bg_stage_1");
        SceneManager.LoadScene(2);
    }

    public void BackToMianMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToSotre()
    {
        SceneManager.LoadScene(4);
    }

    public void ToStrengCard()
    {
        SceneManager.LoadScene(3);
    }

}
