using System;
using UnityEngine;
using DG.Tweening;

public class CardsUI : MonoBehaviour
{
    private Card _selectedCard;
    public Card[] Cards;
    public Transform centerPoint;
    public GameObject Joystick;
    

    private void OnEnable()
    {
        Time.timeScale = 0f;
        Joystick.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        Joystick.SetActive(true);

    }

    private void Awake()
    {
        
    }


    public void OnCardSelect(Card SelectedCard)
    {
        foreach (var card in Cards)
        {
            if (card == SelectedCard)
            {
                Debug.Log("1");
                card.transform.SetParent(transform, true);
                card.transform.DOScale(2f,1f).SetEase(Ease.OutBack).SetUpdate(true);
                card.transform.DOMove(centerPoint.position, 1f).SetEase(Ease.OutBack).SetUpdate(true)
                    .OnComplete(() =>
                    {
                        ResetCards();
                        Debug.Log(card.name);
                        transform.gameObject.SetActive(false);
                    });
            }
            else
            {
                card.gameObject.SetActive(false);
            }
        }
       
    }

    private void ApplyReward(Card Card)
    {
        
    }

    public void ResetCards()
    {
        foreach(var card in Cards)
        {
            card.transform.SetParent(card.defaultParent);
            card.transform.position = card.defaultPos;
            card.transform.localScale = card.defaultScale;
            card.gameObject.SetActive(true);
        }
        
    }
}
