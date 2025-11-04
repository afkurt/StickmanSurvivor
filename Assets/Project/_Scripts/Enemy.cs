using UnityEngine;

public class Enemy : Entity
{
    [Header("Detection Settings")]
    public float detectionRadius = 5f;
    public float moveSpeed = 3f;

    private Transform target;

    void Update()
    {
        DetectTarget();
        MoveTowardsTarget();
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
            // Hedef yoksa idle kal
            return;
        }

        // Hedefe doðru yönel
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Ýstersen bakýþ yönünü de hedefe çevir
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    void OnDrawGizmosSelected()
    {
        // Editörde detection alanýný görselleþtirmek için
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
