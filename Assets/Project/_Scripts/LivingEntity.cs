using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour
{
    public delegate void OnDamageTake(int damage);
    public static OnDamageTake onDamageTake;

    public delegate void OnDie();
    public static OnDie onDie;


    protected int MaxHealth = 100;
    [SerializeField] protected int CurrentHealth;

    
    private void OnEnable()
    {
        onDamageTake += TakeDamage;
        onDie += Die;
    }

    private void OnDisable()
    {
        onDamageTake -= TakeDamage;
        onDie -= Die;
    }
    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth < 0 )
        {
            onDie?.Invoke();
        }
        
    }
    public virtual void Die()
    {
        
    }

    


}
