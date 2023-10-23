using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private ParticleSystem pickUpVFX;
    [SerializeField] private bool TriggerPickup;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void HandlePickUp()
    {
        if (TriggerPickup)
            return;
        StartCoroutine(PickUp());
        TriggerPickup = true;
    }
    private IEnumerator PickUp()
    {
        LevelManager.Instance.DecreasePickUp();
        pickUpVFX.Clear();
        pickUpVFX.Play();
        anim.SetTrigger("Pickup");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
