using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 1f;

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public virtual void OnInit(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        OnDespawn();
    }
}
