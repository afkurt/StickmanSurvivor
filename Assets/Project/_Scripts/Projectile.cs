using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Target;
    public int Damage = 10;
    public float Speed = 10f;
    public float Lifetime = 2f;
    public float Timer;
    public TrailRenderer _trailRenderer;
    public Entity Entity;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        Timer = 0;
        Target = null;
    }

    private void Update()
    {
        if(Timer > Lifetime)
        {
            ObjectPoolingManager.Instance.ReturnQueue(gameObject);
            Timer = 0;
            return;
        }
        else
        {
            if (Entity.CurrentHealth <= 0)
            {
                Target = null;
                
            }
            ProtectileMove();

        }
        Timer += Time.deltaTime;
        
    }

    
    public void SetTarget(Enemy t)
    {
        Entity = t.GetComponent<Entity>();
        Target = Entity.AimPoint;
    }

    public void ProtectileMove()
    {
        Vector3 dir = transform.position;
        if (Target == null)
        {
            dir = transform.forward ;
        }
        else
        {
            dir = (Target.position - transform.position).normalized;
            transform.LookAt(Target);
        }
        transform.position += dir * Speed * Time.deltaTime;
    }

     
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy == null ) return;
        enemy.TakeDamage(25);

        ObjectPoolingManager.Instance.ReturnQueue(gameObject);

    }
}
