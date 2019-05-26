using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class LoadStore : MonoBehaviour
{
    public ScrollRect rect;
    public List<Card> licards;
    public List<Card> playercards;
    public Transform newcard;
    public Text playermoney;

    private int money;
    private string playername;

    private void Awake()
    {
        money = PlayerPrefs.GetInt("money");
        playername = PlayerPrefs.GetString("playername");
        LoadStoreList();
        LoadPlayerCardList();
        playermoney.text = money.ToString();
    }

    public void LoadStoreList()
    {
        string filepath = Application.dataPath + "/Doc" + "/cardlist_all.json";
        if (File.Exists(filepath))
        {
            Debug.Log("file is real");
            StreamReader sr = new StreamReader(filepath);//读取JSON文件
            string str = sr.ReadToEnd();
            licards = JsonConvert.DeserializeObject<List<Card>>(str);//反序列化为List
        }
        RandomCard randomCard = new RandomCard();
        licards = randomCard.RandomCards(licards);
        for(int i = 0; i < 8; i++)
        {
            GameObject go = Instantiate(newcard.gameObject, rect.content);
            StoreCardListerner setcard = go.GetComponentInChildren<StoreCardListerner>();
            setcard.Textset(licards[i]);
        }
    }

    public void LoadPlayerCardList()
    {
        string filepath = Application.dataPath + "/Doc/" + playername +"/cardlist.json";
        if (File.Exists(filepath))
        {
            Debug.Log("file is real");
            StreamReader sr = new StreamReader(filepath);//读取JSON文件
            string str = sr.ReadToEnd();
            playercards = JsonConvert.DeserializeObject<List<Card>>(str);//反序列化为List
        }
    }

    public bool BuyCard(Card card)
    {
        if (money < card.card_gold) return false;
        money -= card.card_gold;
        playercards.Add(card);
        return true;
    }

    public void Leave()
    {
        PlayerPrefs.SetInt("money", money);
        SaveTool saveTool = new SaveTool();
        saveTool.SaveCardList(playercards);
        saveTool.SavePlayerData();
        SceneManager.LoadScene(1);
    }

}
