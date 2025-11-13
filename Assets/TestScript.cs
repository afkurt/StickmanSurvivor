using UnityEngine;

public class TestScript : MonoBehaviour
{
    public CardsUI CardsUI;
    
    public void Show()
    {
        CardsUI.gameObject.SetActive(true);
    }
}
