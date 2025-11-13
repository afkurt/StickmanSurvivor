using System;
using Unity.Mathematics;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public GameObject Chest;
    public float distance;
    public Player player;
    public static ChestManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDisable()
    {
    }

    public void SpawnChest()
    {
        Vector2 dis = (UnityEngine.Random.insideUnitCircle.normalized) * 5f;
        GameObject newChest = Instantiate(Chest, player.transform.position + new Vector3(dis.x, 0, dis.y), Quaternion.identity);
    }
}
