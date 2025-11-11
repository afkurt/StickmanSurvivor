using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour
{
    public delegate void OnDamageTake(float damage);
    public OnDamageTake onDamageTake;

    public GameObject _VFX;

    public delegate void OnDie();
    public OnDie onDie;

    [SerializeField] protected SkinnedMeshRenderer _meshRenderer;
    [SerializeField] protected Color _defaultColor;


    protected int MaxHealth = 50;
    [SerializeField] public float CurrentHealth;

    protected virtual void Start()
    {
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _defaultColor = _meshRenderer.material.color;

    }
    protected virtual void OnEnable()
    {
        onDamageTake += TakeDamage;
        onDie += Die;
    }

    protected virtual void OnDisable()
    {
        onDamageTake -= TakeDamage;
        onDie -= Die;
        _meshRenderer.material.color = _defaultColor;
    }
    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        StartCoroutine(HitEffect());

        if(CurrentHealth <= 0 )
        {
            GameObject DieVFX =  ObjectPoolingManager.Instance.GetDieVFX();
            DieVFX.transform.position = transform.position;
            DieVFX.SetActive(true);
            onDie?.Invoke();
            
        }
        
    }
    public virtual void Die()
    {
        
    }
    public virtual IEnumerator HitEffect()
    {
        _meshRenderer.material.color = Color.blue;
        yield return new WaitForSeconds(0.1f);
        _meshRenderer.material.color = _defaultColor;
    }




}
