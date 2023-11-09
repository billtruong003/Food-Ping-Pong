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
    private PlayerVFXManager vFXManager;
    private Vector3 lastVel;
    private Rigidbody2D rb;
    [SerializeField] private AudioClip wallCollideSound;

    void Start()
    {
        vFXManager = GetComponent<PlayerVFXManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastVel = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            ShakeCam();
            SoundManager.Instance.PlayWallCollide(wallCollideSound);
        }
        vFXManager.BounceSpark();
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
    }
    private void ShakeCam()
    {
        if (MainManager.Instance != null)
        {
            MainManager.Instance.ShakeCamera();
        }
    }
    private IEnumerator StopBall()
    {
        yield return new WaitForSeconds(1f);
    }
}