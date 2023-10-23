using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite PlayerSprite;
    [SerializeField] private PlayerVFXManager VFXManager;
    [SerializeField] private Animator anim;
    public Vector2 boxSize = new Vector2(1.0f, 1.0f);
    public LayerMask targetLayer;

    public void TriggerAnim(string action)
    {
        if (anim != null)
        {
            anim.SetTrigger(action);
        }
    }
    private void Update()
    {
        Vector2 boxPosition = transform.position;
        Collider2D triggerPickup = Physics2D.OverlapBox(boxPosition, boxSize, 0, targetLayer);

        if (triggerPickup)
        {
            Debug.Log("Trigger");
            PickUpItem pickUp = triggerPickup.GetComponent<PickUpItem>();
            pickUp.HandlePickUp();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 boxPosition = transform.position;
        Gizmos.DrawWireCube(boxPosition, boxSize);
        // Look at those silly Gizmos! They are so red and wirey!
    }
}