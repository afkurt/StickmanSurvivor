using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI XPText;

    public void UpdateXPUI()
    {
        XPText.text = XpManager.Instance.Xp.ToString();
    }

    private void Update()
    {
        UpdateXPUI();
    }
}

