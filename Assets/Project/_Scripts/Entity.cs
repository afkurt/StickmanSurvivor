using UnityEngine;

public abstract class Entity : LivingEntity
{
    public EntityData EntityData;

    public LayerMask TargetLayer;

    

    protected virtual void Start()
    {
        CurrentHealth = EntityData.MaxHealth;
        TargetLayer = EntityData.TargetLayer;
    }
}
