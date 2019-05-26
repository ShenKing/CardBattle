using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settest : MonoBehaviour
{
    public Text cost;
    public Text count;
    public Text nametext;
    public Image bg_card;
    public Image bg_cost;
    public RectTransform cardwith;
    private Card setcard;
    private string color;
    private int nowenegy;

    public void Textset(Card dic)
    {
        Vector2 newsize = new Vector2(100f, 200f);
        this.setcard = dic;
        this.nowenegy = GameObject.Find("Panel").GetComponent<PanelListener>().enegy;
        if(setcard.card_damage == setcard.card_strengdamage)
        {
            color = "<color=#00FF1D>";
        }
        else
        {
            color = "<color=#0070FF>";
        }
        switch (setcard.card_type)
        {
            case CardType.Attack: count.text = setcard.card_description + color + setcard.card_damage.ToString() + "</color>" + "点伤害"; break;
            case CardType.Defense: count.text = setcard.card_description + color + setcard.card_damage.ToString()+"</color>"+"点护甲"; break;
            case CardType.Equip: count.text = setcard.card_description + color + setcard.card_damage.ToString() + "</color>"+"点"; break;
            case CardType.Skill:
                if (setcard.card_skill_type == SkillType.Attack)
                {
                    count.text = setcard.card_description + color + setcard.card_damage.ToString() + "</color>" + "点伤害";
                }
                else
                {
                    count.text = setcard.card_description + color + setcard.card_damage.ToString() + "</color>" + "点护甲并且抽" + "<color=#0070FF>" + setcard.card_num + "</color>" + "张卡";
                }
                break;
        }

        if (setcard.card_cost <= nowenegy)
        {
            cost.text = "<color=#FFFFFF>" + setcard.card_cost.ToString() + "</color>";
        }
        else
        {
            cost.text = "<color=#FF0000>" + setcard.card_cost.ToString() + "</color>";
        }
        
        nametext.text = (string)setcard.card_name;
       //ardwith.sizeDelta = newsize;
        bg_card.sprite = Resources.Load<Sprite>("Textures/bg_card");
       // Debug.Log(bg_card.sprite.name);
        bg_cost.sprite = Resources.Load<Sprite>("Textures/bg_cost");
    }

    public void CardReFresh()
    {
        this.nowenegy = GameObject.Find("Panel").GetComponent<PanelListener>().enegy;
        if (setcard.card_cost <= nowenegy)
        {
            cost.text = "<color=#FFFFFF>" + setcard.card_cost.ToString() + "</color>";
        }
        else
        {
            cost.text = "<color=#FF0000>" + setcard.card_cost.ToString() + "</color>";
        }
    }

    public void AddCardToPlayer()
    {
        GameObject.Find("Stagemanger").GetComponent<BattleSystem>().GetNewCard(setcard);
        GameObject.Find("Stagemanger").GetComponent<BattleSystem>().SceneChange();
    }

}
