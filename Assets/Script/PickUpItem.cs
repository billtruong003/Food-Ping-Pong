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
    [SerializeField] private LayerMask wallLayer = 7;
    private Vector2 limitTopLeft = new Vector2(-7, -11);
    private Vector2 limitDownRight = new Vector2(7, 10.5f);
    private Vector2 boxSize = new Vector2(1.0f, 1.0f);
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        Vector2 halfSize = boxSize;
        bool colliders = Physics2D.OverlapBox(position, halfSize, 0, wallLayer);
        if (colliders)
        {
            ChangePosition();
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 position = transform.position;
        position.z = 0;

        Gizmos.DrawWireCube(position, new Vector3(boxSize.x, boxSize.y, 1.0f));
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
        SoundManager.Instance.PlaySFX(2);
        UIManager.Instance.CheckInventory(materialName);
        UIManager.Instance.PopPickUp(matSprite.sprite);
        StartCoroutine(PickUp());
        triggerPickUp = true;
    }

    private IEnumerator PickUp()
    {
        pickUpVFX.Stop();
        pickUpVFX.Clear();
        pickUpVFX.Play();
        anim.SetTrigger("Pickup");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }


    private void ChangePosition()
    {
        float positionX = Random.Range(limitTopLeft.x, limitDownRight.x);
        float positionY = Random.Range(limitTopLeft.y, limitDownRight.y);

        // Change the position directly without enabling/disabling
        transform.position = new Vector3(positionX, positionY);

        // Reset the scale
        transform.localScale = Vector3.one;
    }
}
