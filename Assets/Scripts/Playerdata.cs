using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdata
{
    public string name;
    public int hp;
    public int enegy;
    public int armor;
    public int money;
    public int curepotion;
    public int armorpotion;
    public int damagepotion;

    public Playerdata(string name,int hp, int enegy, int armor, int money, int curepotion, int armorpotion, int damagepotion)
    {
        this.name = name;
        this.hp = hp;
        this.enegy = enegy;
        this.armor = armor;
        this.money = money;
        this.curepotion = curepotion;
        this.armorpotion = armorpotion;
        this.damagepotion = damagepotion;
    }
}
