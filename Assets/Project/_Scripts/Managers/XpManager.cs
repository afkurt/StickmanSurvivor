using UnityEngine;

public class XpManager : MonoBehaviour
{
    public static XpManager Instance;

    public float CurrentXp;
    public float RequiredXP = 10;

    private void OnEnable()
    {
        Instance = this;
    }

    public void AddXP(int xp)
    {
        Debug.Log("1");
        CurrentXp += xp;
        if(CurrentXp >= RequiredXP)
        {
            ChestManager.Instance.SpawnChest();
            CurrentXp = 0;
            RequiredXP *= 1.2f;
        }
    }

}
