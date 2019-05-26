using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelListener : MonoBehaviour,IPointerClickHandler
{
    public bool cardbeclick = false;
    public string cardname;
    private bool havacardbeclick = false;
    private Card choosecard;
    public int enegy = 0;
    private Monster choosemonster;
    private GameObject[] cards;
    private GameObject[] monsters;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (havacardbeclick)
        {
            GameObject.Find(cardname).SendMessage("SetCardBack");
            havacardbeclick = false;
        }
        
    }

    public void OnCardbeClick(string name,Card card)
    {
        choosecard = card;
        if (!havacardbeclick)
        {
            this.cardname = name;
            havacardbeclick = true;
        }
        if (this.cardname != name)
        {
            GameObject.Find(cardname).SendMessage("SetCardBack");
            this.cardname = name;
        }
    }

    public Monster DamageEvent(Monster monster)
    { 
        choosemonster = monster;
        if (!havacardbeclick) return choosemonster;
        if (enegy < choosecard.card_cost) return monster;
        if (choosecard.card_type != CardType.Attack) return monster;
        if (choosecard.card_damage < choosemonster.monster_armor)
        {
            choosemonster.monster_armor -= choosecard.card_damage;
            GameObject.Find(cardname).SendMessage("CardBeUsed");
            enegy = GameObject.Find("Stagemanger").GetComponent<BattleSystem>().UseCard(choosecard);
            havacardbeclick = false;
            ReFreshCard();
            return choosemonster;
        }
        else
        {
            choosemonster.monster_hp += choosemonster.monster_armor;
            choosemonster.monster_hp -= choosecard.card_damage;
            choosemonster.monster_armor = 0;
            GameObject.Find(cardname).SendMessage("CardBeUsed");
            enegy = GameObject.Find("Stagemanger").GetComponent<BattleSystem>().UseCard(choosecard);
            havacardbeclick = false;
            ReFreshCard();
            return choosemonster;
        }
    }

    public void ReFreshCard()
    {
        cards = GameObject.FindGameObjectsWithTag("card");
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].SendMessage("CardReFresh");
        }
    }

    public void UseDefenceSkill(string name, Card card)
    {
        choosecard = card;
        /*if (!havacardbeclick)
        {
            this.cardname = name;
            havacardbeclick = true;
        }*/
        if (enegy < choosecard.card_cost) return;
        GameObject.Find(name).SendMessage("CardBeUsed");
        enegy = GameObject.Find("Stagemanger").GetComponent<BattleSystem>().UseCard(card);
        GameObject.Find("Stagemanger").GetComponent<BattleSystem>().playerarmor += card.card_damage;
        havacardbeclick = false;
        ReFreshCard();
    }

    public void UseAttackSkill(string name, Card card)
    {
        if (enegy < choosecard.card_cost) return;
        choosecard = card;
        GameObject.Find(name).SendMessage("CardBeUsed");
        enegy = GameObject.Find("Stagemanger").GetComponent<BattleSystem>().UseCard(card);
        monsters = GameObject.FindGameObjectsWithTag("monster");
        for(int i = 0; i < monsters.Length; i++)
        {
            monsters[i].GetComponent<MonsterState>().BeSkillDamage(card.card_damage);
        }
        havacardbeclick = false;
        ReFreshCard();
    }

    public void UseCureSkill(string name, Card card)
    {
        if (enegy < choosecard.card_cost) return;
        Debug.Log(card.card_name);
        GameObject.Find(name).SendMessage("CardBeUsed");
        enegy = GameObject.Find("Stagemanger").GetComponent<BattleSystem>().UseCard(card);
        GameObject.Find("Stagemanger").GetComponent<BattleSystem>().playerhp += card.card_damage;
        havacardbeclick = false;
        ReFreshCard();
    }

}
