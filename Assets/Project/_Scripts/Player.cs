using System;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Attributes")]
    [SerializeField] private Vector3 _direction;
    private float _gravity = -3f;
    private Animator _animator;
    private float AttackTimer;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onDamageTake?.Invoke(10);
            Debug.Log($"{CurrentHealth}");
        }
    }


    public void PlayerMove()
    {
        _direction.x= _joystick.Horizontal;
        _direction.z = _joystick.Vertical;
        
        _controller.Move(_direction * MovingSpeed * Time.deltaTime);
        transform.LookAt(transform.position + new Vector3 (_direction.x , 0, _direction.z));

        float animSpeed = new Vector3(_direction.x, 0 , _direction.z).magnitude;
        _animator.SetFloat("Speed", animSpeed);
    }
    public void ApplyGravity()
    {
        if (!_controller.isGrounded)
        {
            _direction.y += _gravity * Time.deltaTime;
            
        }
        else
        {
            _direction.y = -1f;
        }
    }

    public void SpawnProjectile()
    {

        Transform Target = FindNearestTarget();

        if (Target != null)
        {
            GameObject _projectile = ObjectPoolingManager.Instance.GetProjectile();
            _projectile.transform.position = transform.position + transform.forward + transform.up;
            _projectile.transform.rotation = transform.rotation;
            _projectile.SetActive(true);
            _projectile.GetComponent<Projectile>().SetTarget(Target);
            
        }

    }

    private Transform FindNearestTarget()
    {
        
        Collider[] hits = Physics.OverlapSphere(transform.position, AttackRange, TargetLayer);
        if (hits.Length == 0) return null;

        Transform nearest = hits[0].transform;
        return nearest;
    }

    void OnDrawGizmosSelected()
    {
        // Editörde detection alanını görselleştirmek için
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
