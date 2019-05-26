using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveTool
{
    public void SaveCardList(List<Card> cards)
    {
        string playername = PlayerPrefs.GetString("playername");
        string text = JsonConvert.SerializeObject(cards, Formatting.Indented);
        string path = Application.dataPath + "/Doc/" + playername + "/cardlist.json";
        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.Write(text);
        streamWriter.Close();
    }

    public void SavePlayerData()
    {
        string playername = PlayerPrefs.GetString("playername");
        int hp = PlayerPrefs.GetInt("hp");
        int enegy = PlayerPrefs.GetInt("enegy");
        int armor = PlayerPrefs.GetInt("armor");
        int money = PlayerPrefs.GetInt("money");
        int curepotion = PlayerPrefs.GetInt("curepotion");
        int armorpotion = PlayerPrefs.GetInt("armorpotion");
        int damagepotion = PlayerPrefs.GetInt("damagepotion");
        string path = Application.dataPath + "/Doc/" + playername + "/playerdata.json";
        Playerdata playerdata = new Playerdata(playername, hp, enegy, armor, money, curepotion, armorpotion, damagepotion);
        string text = JsonConvert.SerializeObject(playerdata,Formatting.Indented);
        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.Write(text);
        streamWriter.Close();
    }
}
