using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Attack = 1,
    Defense = 2,
    Skill = 3,
    Equip = 4
};

public enum SkillType
{
    Attack = 1,
    Defense = 2,
    Cure = 3
};

public class Card
{
    public int card_id;
    public string card_name;
    public string card_description;
    public string card_icon;
    public int card_damage;
    public int card_strengdamage;
    public int card_cost;
    public int card_num;
    public int card_gold;
    public CardType card_type;
    public SkillType card_skill_type;
    public int card_level = 1;

    public Card(int id,string name,string description,string icon,int damage,int strengdamage,int cost,int num,int card_gold,CardType type,SkillType skillType)
    {
        this.card_id = id;
        this.card_name = name;
        this.card_description = description;
        this.card_icon = icon;
        this.card_damage = damage;
        this.card_strengdamage = strengdamage;
        this.card_cost = cost;
        this.card_num = num;
        this.card_gold = card_gold;
        this.card_type = type;
        this.card_skill_type = skillType;
    }

}
