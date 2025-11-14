using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI KillCountUI;
    public delegate void OnChestOpen();
    public OnChestOpen onChestOpen;
    public CardsUI CardsUI;
    public Canvas PlayerDieUI;
    public Slider xpSlider;
    public int KillCount;

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
        float progress = XpManager.Instance.CurrentXp / XpManager.Instance.RequiredXP;
        xpSlider.DOValue(progress, 0.2f).SetEase(Ease.OutQuad);
    }

    public void UpdateHealthUI(float health)
    {
        HealthText.text = (health.ToString());
        HealthText.transform.DOScale(transform.localScale * 1.2f, 0.2f)
            .OnComplete(() =>
            {
                HealthText.transform.localScale *= 0.8f;
            });
    }

    private void Update()
    {
        UpdateXPUI();
    }

    public void ShowPlayerDie()
    {
        PlayerDieUI.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;

    }
    public void UpdateKillCount()
    {
        KillCount++;
        KillCountUI.text = KillCount.ToString();

    }


}

