using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class StrengCard : MonoBehaviour
{
    public List<Card> cardlist;
    public ScrollRect rect;
    public Transform newcard;
    public GameObject btn;
    public GameObject cardarea;
    private string playername;

    private void Awake()
    {
        playername = PlayerPrefs.GetString("playername");
        string filepath = Application.dataPath + "/Doc/" +playername + "/cardlist.json";
        if (File.Exists(filepath))
        {
            Debug.Log("file is real");
            StreamReader sr = new StreamReader(filepath);//读取JSON文件
            string str = sr.ReadToEnd();
             cardlist = JsonConvert.DeserializeObject<List<Card>>(str);//反序列化为List

        }
        foreach (var pair in cardlist)
        {
            GameObject go = Instantiate(newcard.gameObject, rect.content);
            CardStrengListener setcard = go.GetComponentInChildren<CardStrengListener>();
            setcard.Textset(pair);
        }

    }

    public Card StrengthenCard(Card card)
    {
        if (card.card_damage==card.card_strengdamage) return card;
        cardlist.Remove(card);
        card.card_damage = card.card_strengdamage;
        cardlist.Add(card);
        btn.SetActive(false);
        return card;
    }

    public void OutScene()
    {
        SaveTool saveTool = new SaveTool();
        saveTool.SaveCardList(cardlist);
        saveTool.SavePlayerData();
        Debug.Log("强化成功");
        SceneManager.LoadScene(1);
    }

    public void ShowCardArea()
    {
        cardarea.SetActive(true);
    }

}
