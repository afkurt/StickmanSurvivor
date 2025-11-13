using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour 
{
    [HideInInspector] public Vector3 defaultPos;
    [HideInInspector] public Vector3 defaultScale;
    [HideInInspector] public Transform defaultParent;

    public enum CardType { Attack, Health, Cooldown}
    public CardType cardType;

    private void Awake()
    {
        defaultPos = transform.position;
        defaultScale = transform.localScale;
        defaultParent = transform.parent;
    }
}
