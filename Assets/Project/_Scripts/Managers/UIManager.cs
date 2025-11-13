using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI XPText;
    public TextMeshProUGUI HealthText;
    public delegate void OnChestOpen();
    public OnChestOpen onChestOpen;
    public CardsUI CardsUI;
    public Canvas PlayerDieUI;

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

    public void UpdateHealthUI(float health)
    {
        HealthText.text = ("Health  " + health.ToString());
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
}

