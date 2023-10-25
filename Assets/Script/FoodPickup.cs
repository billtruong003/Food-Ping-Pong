using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    [SerializeField] private ParticleSystem pickUpVFX;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PickUpItem());
        }
    }

    private IEnumerator PickUpItem()
    {
        pickUpVFX.Clear();
        pickUpVFX.Play();
        anim.SetTrigger("Pickup");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        float x = Random.Range(-3, 3);
        float y = Random.Range(-7, 7);
        transform.parent.parent.position = new Vector2(x, y);
        anim.Play("Appear");
        gameObject.SetActive(true);
    }
}
