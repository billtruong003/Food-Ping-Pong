using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragShoot : MonoBehaviour
{

    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    private Vector3 playerPosition;
    private Vector3 mousePosition;
    private Rigidbody2D rb;
    private bool isDragging = false;
    [Header("Drag n Shoot")]
    [SerializeField] private float maxDragDistance = 5.0f;
    [SerializeField] private float shootForce = 10.0f;
    [SerializeField] private dragState state = dragState.NONE;
    [Header("Rotate")]
    [SerializeField] private GameObject directionPointer;
    [SerializeField] private Vector3 rotateLook;
    [SerializeField] private Vector3 rotateToLookAt;
    [Header("Draw")]
    [SerializeField] private LineRenderer line;
    enum dragState
    {
        NONE,
        CAN_DRAG,
        CANT_DRAG,
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = dragState.CAN_DRAG;
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DragAndShoot();
        RotateToRightDir();
    }
    private void RotateToRightDir()
    {
        if (Input.GetMouseButton(0) & state == dragState.CAN_DRAG)
        {
            // Nếu pointer ko active trong hierachie thì mới chạy
            if (!directionPointer.activeInHierarchy)
                directionPointer.SetActive(true);

            // Lấy vị trí của chuột trên màn hình
            mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z - Camera.main.transform.position.z;

            // Chuyển đổi vị trí chuột sang vị trí trong không gian thế giới
            rotateLook = Camera.main.ScreenToWorldPoint(mousePosition);

            // Tính toán góc quay để nhìn về vị trí chuột
            Vector3 direction = rotateLook - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Đặt góc quay cho đối tượng
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            // Vẽ line
            line.enabled = true;

            // Vẽ đuôi cho vật
            float lengthLine = Vector3.Distance(transform.position, rotateLook);
            
            
            // newPoint *= (maxDragDistance / 2);
            line.SetPosition(0, transform.position);
            if (lengthLine > maxDragDistance)
            {
                Vector3 dir = (rotateLook - transform.position).normalized;
                Vector3 newPoint = (transform.position + dir * maxDragDistance);
                line.SetPosition(1, newPoint);
            }
            else
            {
                line.SetPosition(1, rotateLook);
            }

        }
        else
        {
            if (directionPointer.activeInHierarchy)
                directionPointer.SetActive(false);
        }
    }
    private void DragAndShoot()
    {
        
        
        if (state != dragState.CAN_DRAG)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            // Bắt đầu kéo
            isDragging = true;
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            line.enabled = false;

            // Kết thúc kéo và bắn
            isDragging = false;
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerPosition = transform.position;
            // Tính toán hướng bắn
            Vector2 shootDirection = (playerPosition - dragEndPosition).normalized;

            // Tính toán lực bắn
            float dragDistance = Vector2.Distance(dragStartPosition, dragEndPosition);
            float clampedDragDistance = Mathf.Clamp(dragDistance, 0, maxDragDistance);
            float force = clampedDragDistance * shootForce;

            // Áp dụng lực bắn
            rb.velocity = shootDirection * force;

            state = dragState.CANT_DRAG;
            Debug.Log(clampedDragDistance);
            StartCoroutine(Ready());
        }
    }
    private IEnumerator Ready()
    {
        yield return new WaitUntil(() => (rb.velocity.magnitude < 1.5f));

        while (rb.velocity.magnitude > 0.4f) // Kiểm tra nếu tốc độ còn đủ lớn để giảm dần
        {
            rb.velocity = rb.velocity * 0.5f; // Giảm dần tốc độ
            yield return new WaitForSeconds(0.3f);
            yield return null;
        }
        rb.velocity = Vector3.zero;

        Debug.Log("Stop");

        state = dragState.CAN_DRAG;
    }

}
