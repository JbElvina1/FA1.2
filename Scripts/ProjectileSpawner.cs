using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform target;
    public float interval = 1.5f;
    public float speed = 8f;
    public float lifetime = 6f;

    void Start() => InvokeRepeating(nameof(Fire), 1f, interval);

    void Fire()
    {
        if (projectilePrefab == null || target == null) return;

        var p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector3 dir = (target.position - transform.position).normalized;

        var rb = p.GetComponent<Rigidbody>();
        if (rb != null) rb.linearVelocity = dir * speed;

        Destroy(p, lifetime);
    }
}