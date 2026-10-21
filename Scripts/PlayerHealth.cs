using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int Health { get; private set; }
    public int Hits { get; private set; }

    PlayerTransformController controller;

    void Awake()
    {
        Health = maxHealth;
        controller = GetComponent<PlayerTransformController>();
    }

    public void TakeHit(int damage = 1)
    {
        Hits++;
        Health = Mathf.Max(0, Health - damage);
        if (Health == 0)
        {
            if (controller != null) controller.ResetTransform();
            Health = maxHealth;
        }
    }
}