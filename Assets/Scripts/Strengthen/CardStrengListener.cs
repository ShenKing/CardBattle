using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardStrengListener : MonoBehaviour, IPointerClickHandler
{
    public Text cost;
    public Text count;
    public Text nametext;
    public Image bg_card;
    public Image bg_cost;
    public RectTransform cardwith;
    private Card setcard;
    private string color;

    public void Textset(Card dic)
    {
        Vector2 newsize = new Vector2(100f, 200f);
        this.setcard = dic;
        if (setcard.card_damage == setcard.card_strengdamage)
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
            case CardType.Defense: count.text = setcard.card_description + color + setcard.card_damage.ToString() + "</color>" + "点护甲"; break;
            case CardType.Equip: count.text = setcard.card_description + color + setcard.card_damage.ToString() + "</color>" + "点"; break;
            case CardType.Skill: if (setcard.card_skill_type == SkillType.Attack)
                {
                    count.text=setcard.card_description + color + setcard.card_damage.ToString() + "</color>" + "点伤害";
                }
                else
                {
                    count.text = setcard.card_description + color + setcard.card_damage.ToString() + "</color>" + "点护甲并且抽" + "<color=#0070FF>" + setcard.card_num + "</color>" + "张卡";
                }
                break;
        }
        cost.text =setcard.card_cost.ToString();
        nametext.text = (string)setcard.card_name;
        bg_card.sprite = Resources.Load<Sprite>("Textures/bg_card");
        bg_cost.sprite = Resources.Load<Sprite>("Textures/bg_cost");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.Find("StrenManager").GetComponent<StrengCard>().ShowCardArea();
        GameObject.Find("ChoosedCard").GetComponentInChildren<CardStrengListener>().Textset(setcard);
    }

    public void StrenCard()
    {
        setcard = GameObject.Find("StrenManager").GetComponent<StrengCard>().StrengthenCard(setcard);
        Textset(setcard);
    }

}
