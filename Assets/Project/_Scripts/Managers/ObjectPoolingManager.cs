using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public GameObject EnemyPrefab;
    public int MaxProjectileCount = 100;
    public int MaxEnemyCount = 10;

    public Queue<GameObject> ProjectileQueue = new Queue<GameObject>();
    public Queue<GameObject> EnemyQueue = new Queue<GameObject>();

    public static ObjectPoolingManager Instance;
    private void Awake()
    {
        if(Instance == null) Instance = this;
        CreatePool(ProjectilePrefab, ProjectileQueue, MaxProjectileCount);
        CreatePool(EnemyPrefab, EnemyQueue, MaxEnemyCount);
    }
    
    private void Start()
    {
        
    }
    

    private void CreatePool(GameObject prefab, Queue<GameObject> pool, int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetProjectile()
    {
        Debug.Log(ProjectileQueue.Count);
        return GetFromPool(ProjectileQueue, ProjectilePrefab);
    }

    public GameObject GetEnemy()
    {
        return GetFromPool(EnemyQueue, EnemyPrefab);
    }

    public GameObject GetFromPool(Queue<GameObject> pool, GameObject prefab)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            Collider objCollider = obj.GetComponent<Collider>();
            objCollider.enabled = true;
            return obj;
        }
        return null;
    }

    public void ReturnQueue(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = transform.position;
        if (obj.GetComponent<Enemy>() != null)
        {
            EnemyQueue.Enqueue(obj);
            return;
        }
        obj.GetComponent<Projectile>().Target = null;
        ProjectileQueue.Enqueue(obj);
    }

}
