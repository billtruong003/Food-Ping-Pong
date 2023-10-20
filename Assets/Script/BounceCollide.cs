using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BounceCollide : MonoBehaviour
{
    // Each time bouncing will reduce speed by percent for example 1 will not change speed and 0 will make the speed to 0
    [SerializeField]
    [Range(0.5f, 1)]
    private float bouncinessFriction;

    [SerializeField]
    private PlayerVFXManager VFXManager;
    private Vector3 lastVel;
    private Rigidbody2D rb;

    void Start()
    {
        VFXManager = GetComponent<PlayerVFXManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        lastVel = rb.velocity;
        // Debug.Log(lastVel.normalized);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        VFXManager.BounceSpark();
        float speed = lastVel.magnitude;
        var direction = Vector3.Reflect(lastVel.normalized, collision.contacts[0].normal);
        Debug.Log(speed);
        if (speed > 1)
        {
            rb.velocity = direction * (speed * bouncinessFriction);
        }
        else
        {
            rb.velocity = direction * Mathf.Min(0, speed);
        }
        StartCoroutine(StopBall());
    }
    private IEnumerator StopBall()
    {
        yield return new WaitForSeconds(1f);

    }
}
