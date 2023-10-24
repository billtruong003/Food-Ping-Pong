using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private ParticleSystem pickUpVFX;
    [SerializeField] private SpriteRenderer matSprite;
    [SerializeField] private bool triggerPickUp;
    [SerializeField] private string materialName;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetMatName(string name)
    {
        materialName = name;
    }

    public void SetMatSprite(Sprite sprite)
    {
        matSprite.sprite = sprite;
    }

    public void HandlePickUp()
    {
        if (triggerPickUp)
            return;
        StartCoroutine(PickUp());
        triggerPickUp = true;
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
