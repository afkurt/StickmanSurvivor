using System.Collections;
using UnityEngine;

public class DieVFX : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        StartCoroutine(ReturnAfterPlay());
    }
    private IEnumerator ReturnAfterPlay()
    {
        yield return new WaitForSeconds(_particleSystem.main.duration);
        ObjectPoolingManager.Instance.ReturnQueue(gameObject);
    }
}
