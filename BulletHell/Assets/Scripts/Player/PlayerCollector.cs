using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D collector;
    public float pullSpeed;
    Rigidbody2D rbTest;
    Vector2 direction;

    private void Start()
    {
        player = GetComponentInParent<PlayerStats>();
        collector = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        collector.radius = player.currentPickupRange;
        if (rbTest != null )
        {
            rbTest.AddForce(direction * pullSpeed);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            rbTest = rb;
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            direction = forceDirection;
            rb.AddForce(forceDirection * pullSpeed);
            collectible.Collect();
        }
    }
}
