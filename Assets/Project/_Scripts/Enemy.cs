using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [Header("Detection Settings")]
    public float detectionRadius = 30f;
    public float moveSpeed = 3f;
    
    private Transform target;
    public TextMeshProUGUI textMeshPro;

    private NavMeshAgent _navMeshAgent;

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
    }

    protected override void OnEnable()
    {
        base.OnEnable();
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
        _animator.SetFloat("Speed", _navMeshAgent.speed);

    }
    void DetectTarget()
    {
        if (target) return;
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, EntityData.TargetLayer);

        if (hits.Length > 0)
        {
            target = hits[0].transform; // en yakýn ilk hedefi al
        }
        else
        {
            target = null;
        }
    }

    void MoveTowardsTarget()
    {
        if (target == null)
        {
            
            return;
        }

        
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        float AnimSpeed = new Vector3(direction.x, 0, direction.z).magnitude;
        _animator.SetFloat("Speed", AnimSpeed);
        
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public override void Die()
    {
        base.Die();
        
        _animator.SetTrigger("Dead");
        
        
    }
    public void OnDeadAnimation()
    {
        XpManager.Instance.AddXP(1);
        ObjectPoolingManager.Instance.ReturnQueue(gameObject);
    }

    
}
