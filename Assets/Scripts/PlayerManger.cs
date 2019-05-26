using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger
{
    public void PlayerDataSet(Playerdata playerdata)
    {
        PlayerPrefs.SetString("playername", playerdata.name);
        PlayerPrefs.SetInt("hp", playerdata.hp);
        PlayerPrefs.SetInt("enegy", playerdata.enegy);
        PlayerPrefs.SetInt("armor", playerdata.armor);
        PlayerPrefs.SetInt("money", playerdata.money);
        PlayerPrefs.SetInt("curepotion", playerdata.curepotion);
        PlayerPrefs.SetInt("armorpotion", playerdata.armorpotion);
        PlayerPrefs.SetInt("damagepotion", playerdata.damagepotion);
    }
}
