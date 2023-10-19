using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCollide : MonoBehaviour
{
    private Vector3 lastVel;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        lastVel = rb.velocity;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVel.magnitude;
        var direction = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
        Debug.Log(speed);
        if (speed > 1)
        {
            rb.velocity = direction * (speed * 0.5f);
        }
        else
        {
            rb.velocity = direction * Mathf.Min(0, speed);
        }
    }
}
