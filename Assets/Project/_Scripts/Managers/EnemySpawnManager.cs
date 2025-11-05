using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _minRad = 2f;
    [SerializeField] private float _maxRad = 10f;
    [SerializeField] private Transform _player;

    private void Start()
    {
        for (int i = 0; i < 5;i++)
        {
            SpawnEnemy();
        }
        
        
    }

    public void SpawnEnemy()
    {
        Vector2 _dir = (Random.insideUnitCircle.normalized ) * _radius;  // 5 birim çemberde rastgele nokta al onu birim vektöre çevir
        Debug.Log(_dir);
        float _distance = Random.Range( _minRad, _maxRad );
        Debug.Log(_distance);
        Vector3 SpawnPoint = _player.transform.position  + new Vector3(_dir.x , 0, _dir.y) * _distance ;

        GameObject obj = ObjectPoolingManager.Instance.GetEnemy();
        obj.transform.position = SpawnPoint;
        obj.SetActive(true);

    }
}
