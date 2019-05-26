using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardListener : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private bool trigger = false;
    private PanelListener panel;
    private Card carddata;
    private bool isclick = false;
    private CardType type;

    private void Awake()
    {
        panel = GameObject.Find("Panel").GetComponent<PanelListener>();
    }

    public void GetCardData(Card card)
    {
        carddata = card;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!trigger)
        {
            gameObject.layer = 8;
            gameObject.GetComponent<Transform>().DOLocalMoveY(20f, 0.3f);
            gameObject.GetComponent<Transform>().DOScale(1.15f, 0.3f);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!trigger)
        {
            gameObject.GetComponent<Transform>().DOScale(1f, 0.3f);
            gameObject.GetComponent<Transform>().DOLocalMoveY(25.5f, 0.3f);
        }
        
    }

    public void SetCardBack()
    {
        gameObject.GetComponent<Transform>().DOScale(1f, 0.3f);
        gameObject.GetComponent<Transform>().DOLocalMoveY(25.5f, 0.3f);
        trigger = false;
        isclick = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if((isclick == true && carddata.card_type == CardType.Skill)||(isclick == true && carddata.card_type == CardType.Defense))
        {
            switch (carddata.card_skill_type)
            {
                case SkillType.Attack: panel.UseAttackSkill(gameObject.name, carddata);break;
                case SkillType.Defense: panel.UseDefenceSkill(gameObject.name, carddata);break;
                case SkillType.Cure:panel.UseCureSkill(gameObject.name, carddata);break;
            }
        }
        else
        {
            isclick = true;
            trigger = true;
            gameObject.GetComponent<Transform>().DOLocalMoveY(80f, 0.3f);
            panel.OnCardbeClick(gameObject.name, carddata);
        }
    }

    public void CardBeUsed()
    {     
        gameObject.transform.parent.gameObject.SetActive(false);  
    }

    public Card ChangeCard()
    {
        return carddata;
    }

}
