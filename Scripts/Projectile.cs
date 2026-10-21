using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;

    void OnCollisionEnter(Collision c)
    {
        var health = c.collider.GetComponentInParent<PlayerHealth>();
        if (health != null) health.TakeHit(damage);
        Destroy(gameObject);
    }
}