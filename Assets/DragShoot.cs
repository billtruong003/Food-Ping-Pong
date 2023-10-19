using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragShoot : MonoBehaviour
{
    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private Rigidbody2D rb;
    private bool isDragging = false;

    public float maxDragDistance = 5.0f;
    public float shootForce = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            // Bắt đầu kéo
            isDragging = true;
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // Kết thúc kéo và bắn
            isDragging = false;
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Tính toán hướng bắn
            Vector2 shootDirection = (dragStartPosition - dragEndPosition).normalized;

            // Tính toán lực bắn
            float dragDistance = Vector2.Distance(dragStartPosition, dragEndPosition);
            float clampedDragDistance = Mathf.Clamp(dragDistance, 0, maxDragDistance);
            float force = clampedDragDistance * shootForce;

            // Áp dụng lực bắn
            rb.velocity = shootDirection * force;

        }
    }
}
