using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private float _radius = 8f;
    [SerializeField] private float _minRad = 2f;
    [SerializeField] private float _maxRad = 10f;
    [SerializeField] private Transform _player;
    [SerializeField] private float _timer;
    [SerializeField] private float _enemyCount;
    public int SpawnEnemyCount;
    public static EnemySpawnManager Instance;


    private void OnEnable()
    {
        Instance = this;
    }


    private void Start()
    {
       
    }

    public void UpdateEnemyCount()
    {
        _enemyCount--;
    }
    

    private void Update()
    {
        _timer += Time.deltaTime;
        if( _timer > 5f && _enemyCount < 200f)
        {
            SpawnEnemy(SpawnEnemyCount);
            _timer = 0;
        }
    
}



    public void SpawnEnemy(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 _dir = (Random.insideUnitCircle.normalized) * _radius;  // 5 birim çemberde rastgele nokta al onu birim vektöre çevir
            float _distance = Random.Range(_minRad, _maxRad);
            Vector3 SpawnPoint = _player.transform.position + new Vector3(_dir.x, 0, _dir.y) * _distance;
            GameObject obj = ObjectPoolingManager.Instance.GetEnemy();
            obj.transform.position = SpawnPoint;
            obj.SetActive(true);
            Enemy enemy = obj.GetComponent<Enemy>();
        }
        _enemyCount += count;

    }
}
