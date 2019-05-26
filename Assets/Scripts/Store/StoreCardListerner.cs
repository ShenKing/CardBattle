using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreCardListerner : MonoBehaviour,IPointerClickHandler
{
    public Text cost;
    public Text count;
    public Text nametext;
    public Image bg_card;
    public Image bg_cost;
    public Text text_gold;
    public RectTransform cardwith;
    private Card setcard;
    private string color;

    public void OnPointerClick(PointerEventData eventData)
    {
        bool issela = GameObject.Find("StoreManager").GetComponent<LoadStore>().BuyCard(setcard);
        if (issela) gameObject.SetActive(false);
    }

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
        cost.text = setcard.card_cost.ToString();
        text_gold.text = setcard.card_gold.ToString();
        nametext.text = (string)setcard.card_name;
        bg_card.sprite = Resources.Load<Sprite>("Textures/bg_card");
        bg_cost.sprite = Resources.Load<Sprite>("Textures/bg_cost");
    }
}
