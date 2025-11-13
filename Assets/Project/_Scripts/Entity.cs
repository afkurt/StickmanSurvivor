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
    public float AttackDamage;
    



    protected override void Start()
    {
        base.Start();
        CurrentHealth = EntityData.MaxHealth;
        MaxHealth = EntityData.MaxHealth;
        TargetLayer = EntityData.TargetLayer;
        MovingSpeed = EntityData.MovingSpeed;  
        AttackRange = EntityData.AttackRange;
        AttackCD = EntityData.AttackCD;
        AttackDamage = EntityData.AttackDamage;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
    }

    
}
