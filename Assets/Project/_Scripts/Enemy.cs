using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Enemy : Entity
{
    [Header("Detection Settings")]
    public float detectionRadius = 30f;
    public float moveSpeed = 3f;
    
    private Transform target;
    Player player;
    public TextMeshProUGUI textMeshPro;

    private NavMeshAgent _navMeshAgent;

    private bool isAttackDone = false;

    protected override void Start()
    {
        base.Start();
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = MovingSpeed;
        
    }


    void Update()
    {
        DetectTarget();
        if(target != null)
        {
            MoveWithNavMesh();
        }
        textMeshPro.text = CurrentHealth.ToString();
        Attack();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        CurrentHealth = MaxHealth;
        onDie += EnemySpawnManager.Instance.UpdateEnemyCount;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onDie -= EnemySpawnManager.Instance.UpdateEnemyCount;
    }

    void MoveWithNavMesh()
    {
        _navMeshAgent.SetDestination(target.position);
        _animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
        if(_navMeshAgent.velocity.magnitude <=  0)
        {
            transform.LookAt(target.position);
        }
        

    }
    void DetectTarget()
    {
        if (target) return;
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, EntityData.TargetLayer);

        if (hits.Length > 0)
        {
            target = hits[0].transform; 
            player = target.GetComponent<Player>();
        }
        else
        {
            target = null;
        }
    }


    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public override void Die()
    {
        base.Die();
        _collider.enabled = false;
        _animator.SetTrigger("Dead");
        XpManager.Instance.AddXP(1);
        ObjectPoolingManager.Instance.ReturnQueue(gameObject);

    }

    private void Attack()
    {
        if (isAttackDone) return;
        
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance <= _navMeshAgent.stoppingDistance)
        {
            isAttackDone = true;
            Debug.Log("Domove çalýþtý");
            transform.DOMove(target.position,1f).SetEase(Ease.OutQuad);
            _animator.SetTrigger("Attack");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;
        Debug.Log("girdi");
        player.TakeDamage(AttackDamage);
        CurrentHealth = 0f;
        Die();
    }


}
