using UnityEngine;

public class XpManager : MonoBehaviour
{
    public static XpManager Instance;

    public int Xp;


    private void OnEnable()
    {
        Instance = this;
    }

    public void AddXP(int xp)
    {
        Xp += xp;
    }

}
