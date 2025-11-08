using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour
{
    public delegate void OnDamageTake(int damage);
    public OnDamageTake onDamageTake;

    public delegate void OnDie();
    public OnDie onDie;


    protected int MaxHealth = 100;
    [SerializeField] public int CurrentHealth;

    
    protected virtual void OnEnable()
    {
        onDamageTake += TakeDamage;
        onDie += Die;
    }

    protected virtual void OnDisable()
    {
        onDamageTake -= TakeDamage;
        onDie -= Die;
    }
    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0 )
        {
            onDie?.Invoke();
        }
        
    }
    public virtual void Die()
    {
        
    }

    


}
