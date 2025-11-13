using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI XPText;
    public delegate void OnChestOpen();
    public OnChestOpen onChestOpen; 
    public CardsUI CardsUI;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        onChestOpen += LevepUpUI;
    }
    private void OnDisable()
    {
        onChestOpen -= LevepUpUI;
    }

    private void LevepUpUI()
    {
        CardsUI.gameObject.SetActive(true);
    }

    public void UpdateXPUI()
    {
        XPText.text = XpManager.Instance.CurrentXp.ToString();
    }

    private void Update()
    {
        UpdateXPUI();
    }
}

