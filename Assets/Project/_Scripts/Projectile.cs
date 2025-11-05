using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Target;
    public int Damage = 10;
    public float Speed = 10f;
    public float Lifetime = 10f;
    public float Timer;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Timer > Lifetime)
        {
            ObjectPoolingManager.Instance.ReturnQueue(gameObject);
            Timer = 0;
        }
        Timer += Time.deltaTime;
        ProtectileMove();
    }
    public void SetTarget(Transform t)
    {
        Target = t.GetComponent<Entity>().AimPoint;
    }

    public void ProtectileMove()
    {
        if(Target == null)
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
            return;
        }

        Vector3 dir = (Target.position - transform.position).normalized;
        transform.position += dir * Speed * Time.deltaTime;
        transform.LookAt(Target);
    }

     
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy == null ) return;
        enemy.TakeDamage(10);
        ObjectPoolingManager.Instance.ReturnQueue(gameObject);

    }
}
