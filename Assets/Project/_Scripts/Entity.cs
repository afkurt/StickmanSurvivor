using UnityEngine;

public abstract class Entity : LivingEntity
{
    public EntityData EntityData;
    public LayerMask TargetLayer;
    public Transform AimPoint;
    public float MovingSpeed;
    public float AttackRange;
    public float AttackCD;
    protected Animator _animator;
    protected Collider _collider;



    protected virtual void Start()
    {
        CurrentHealth = EntityData.MaxHealth;
        TargetLayer = EntityData.TargetLayer;
        MovingSpeed = EntityData.MovingSpeed;  
        AttackRange = EntityData.AttackRange;
        AttackCD = EntityData.AttackCD;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }
}
