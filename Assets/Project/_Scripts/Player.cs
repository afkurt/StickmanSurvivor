using System;
using System.Linq;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Attributes")]
    [SerializeField] private Vector3 _direction;
    private float _gravity = -3f;
    private float AttackTimer;
    public Enemy Target;
    public int i;
    

    private void Awake()
    {
    }

    

    private void Update()
    {
        PlayerMove();
        ApplyGravity();
        AttackTimer += Time.deltaTime;

        if(AttackTimer>AttackCD )
        {
            SpawnProjectile();
            AttackTimer = 0;

        }


        
    }


    public void PlayerMove()
    {
        _direction.x= _joystick.Horizontal;
        _direction.z = _joystick.Vertical;
        
        _controller.Move(_direction * MovingSpeed * Time.deltaTime);
        transform.LookAt(transform.position + new Vector3 (_direction.x , 0, _direction.z)); //buraya bak
        if( Target != null && _direction.magnitude > 0.1f && Target.CurrentHealth >= 1)
        {
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
        float animSpeed = new Vector3(_direction.x, 0 , _direction.z).magnitude;
        _animator.SetFloat("Speed", animSpeed);
    }
    public void ApplyGravity()
    {
        _direction.y = _gravity;
    }
    
    public void SpawnProjectile()
    {
        if (Target == null || Target.CurrentHealth <= 0)
        {
            Target = FindNearestTarget();

            if (Target == null)
                return;
        }

        GameObject _projectile = ObjectPoolingManager.Instance.GetProjectile();
        i++;
        _projectile.transform.position = transform.position + transform.forward + transform.up;
        _projectile.SetActive(true);
        _projectile.GetComponent<Projectile>().SetDamage(AttackDamage);
        _projectile.GetComponent<Projectile>().SetTarget(Target);

    }

    private Enemy FindNearestTarget()
    {
        
        Collider[] hits = Physics.OverlapSphere(transform.position, AttackRange, TargetLayer);
        if (hits.Length == 0) return null;

        hits = hits.OrderBy(h=> (h.transform.position - transform.position).magnitude).ToArray(); 

        Enemy nearest = hits[0].GetComponent<Enemy>();
        return nearest;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    public override void Die()
    {
        base.Die();
        Target = null;
        Time.timeScale = 0f;
        UIManager.Instance.ShowPlayerDie();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UIManager.Instance.UpdateHealthUI(CurrentHealth);
    }
}
