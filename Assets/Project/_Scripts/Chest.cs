using UnityEngine;

public class Chest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == null) return;
        UIManager.Instance.onChestOpen?.Invoke();
        Destroy(gameObject);
    }
}
