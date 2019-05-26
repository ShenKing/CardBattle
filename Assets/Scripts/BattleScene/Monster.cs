using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster 
{
    public int monster_id;
    public string monster_name;
    public string monster_prefab_name;
    public int monster_hp;
    public int monster_armor;
    public int monster_damage;
    public int monster_skill;

    public Monster(int id,string name,string prefabname ,int hp,int armor,int damage,int skill)
    {
        this.monster_id = id;
        this.monster_name = name;
        this.monster_prefab_name = prefabname;
        this.monster_hp = hp;
        this.monster_armor = armor;
        this.monster_damage = damage;
        this.monster_skill = skill;
    }

}
