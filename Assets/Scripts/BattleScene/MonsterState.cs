using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class MonsterState : MonoBehaviour
{
    public Image icon_monster_hp;
    public Text text_monster_hp;
    public GameObject icon_chooseframe;
    private Monster monster;
    private int monster_hp_max;

    private void Awake()
    {
        Monster test= new Monster(2, "树精","monster_tree", 35, 0, 10, 1);
        monster = test;
        GetMonsterData(test);
        icon_chooseframe.SetActive(false);
    }

    public void GetMonsterData(Monster setmonster)
    {
        this.monster = setmonster;
        monster_hp_max = setmonster.monster_hp;
        text_monster_hp.text = monster.monster_hp.ToString() + "/" + monster_hp_max.ToString();
        icon_monster_hp.fillAmount = (monster.monster_hp / (float)monster_hp_max);
    }

    public void BeDamage()
    {
        int lasthp = monster.monster_hp;
        Debug.Log("cache"+lasthp);
        Debug.Log("monster" + monster.monster_hp);
        monster = GameObject.Find("Panel").GetComponent<PanelListener>().DamageEvent(monster);
        Debug.Log("处理后" + monster.monster_hp);
        Debug.Log(lasthp);
        if (lasthp == monster.monster_hp) return;
        if (monster.monster_hp <= 0)
        {
            monster.monster_hp = 0;
            text_monster_hp.text = monster.monster_hp.ToString() + "/" + monster_hp_max.ToString();
            icon_monster_hp.fillAmount = (monster.monster_hp / (float)monster_hp_max);
            GameObject.Find("Stagemanger").GetComponent<BattleSystem>().MonsterDeath();
            gameObject.SetActive(false);
        }
        else
        {
            text_monster_hp.text = monster.monster_hp.ToString() + "/" + monster_hp_max.ToString();
            icon_monster_hp.fillAmount = (monster.monster_hp / (float)monster_hp_max);
            gameObject.transform.DOShakePosition(1f, new Vector3(20, 0, 0), 10, 90);
        }
    }

    public void BeSkillDamage(int damage)
    {
        monster.monster_hp -= damage;
        if (monster.monster_hp <= 0)
        {
            monster.monster_hp = 0;
            text_monster_hp.text = monster.monster_hp.ToString() + "/" + monster_hp_max.ToString();
            icon_monster_hp.fillAmount = (monster.monster_hp / (float)monster_hp_max);
            GameObject.Find("Stagemanger").GetComponent<BattleSystem>().MonsterDeath();
            gameObject.SetActive(false);
        }
        else
        {
            text_monster_hp.text = monster.monster_hp.ToString() + "/" + monster_hp_max.ToString();
            icon_monster_hp.fillAmount = (monster.monster_hp / (float)monster_hp_max);
            gameObject.transform.DOShakePosition(1f, new Vector3(20, 0, 0), 10, 90);
        }
    }

    public void OnMonsterBeChoose()
    {
            icon_chooseframe.SetActive(true);
            //monster_hp -= 7;
            /*text_monster_hp.text = monster_hp.ToString() + "/" + monster_hp_max.ToString();
            icon_monster_hp.fillAmount = (monster_hp / (float)monster_hp_max);
            gameObject.transform.DOShakePosition(1f, new Vector3(20, 0, 0), 10, 90);*/
    }
    
    public void OnMonsterNotBechoose()
    {
        icon_chooseframe.SetActive(false);
    }

    public void Attack()
    {
        Tween tween = gameObject.transform.DOLocalMove(new Vector3(-30, 0, 0), 0.2f);
        tween.SetEase(Ease.InSine);
        tween.SetAutoKill(false);
        gameObject.transform.DOPlayForward();
        tween.OnComplete(()=> { gameObject.transform.DOPlayBackwards(); });
        
        GameObject.Find("Stagemanger").GetComponent<BattleSystem>().BeAttack(monster.monster_damage);
    }

}
