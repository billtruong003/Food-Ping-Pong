using UnityEngine;
using UnityEngine.EventSystems;


public class DragAndShoot : MonoBehaviour
{
    [Header("Movement")]
    public float maxPower;
    float shootPower;
    public float gravity = 1;
    [Range(0f, 0.1f)] public float slowMotion;

    public bool shootWhileMoving = false;
    public bool forwardDraging = true;
    public bool showLineOnScreen = false;

    Transform direction;
    Rigidbody2D rb;
    LineRenderer line;
    LineRenderer screenLine;

    // Vectors // 
    Vector2 startPosition;
    Vector2 targetPosition;
    Vector2 startMousePos;
    Vector2 currentMousePos;

    bool canShoot = true;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        line = GetComponent<LineRenderer>();
        direction = transform.GetChild(0);
        screenLine = direction.GetComponent<LineRenderer>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
            MouseClick();
        }
        if (Input.GetMouseButton(0))
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
            MouseDrag();

            if (shootWhileMoving) rb.velocity /= (1 + slowMotion);

        }

        if (Input.GetMouseButtonUp(0))
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
            MouseRelease();
        }


        if (shootWhileMoving)
            return;

        if (rb.velocity.magnitude < 0.7f)
        {
            rb.velocity = new Vector2(0, 0); //ENABLE THIS IF YOU WANT THE BALL TO STOP IF ITS MOVING SO SLOW
            canShoot = true;
        }
    }

    // MOUSE INPUTS
    void MouseClick()
    {
        if (shootWhileMoving)
        {
            Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = dir * 1;

            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            if (canShoot)
            {
                Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.right = dir * 1;

                startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

    }
    void MouseDrag()
    {
        if (shootWhileMoving)
        {
            LookAtShootDirection();
            DrawLine();

            if (showLineOnScreen)
                DrawScreenLine();

            float distance = Vector2.Distance(currentMousePos, startMousePos);

            if (distance > 1)
            {
                line.enabled = true;

                if (showLineOnScreen)
                    screenLine.enabled = true;
            }
        }
        else
        {
            if (canShoot)
            {
                LookAtShootDirection();
                DrawLine();

                if (showLineOnScreen)
                    DrawScreenLine();

                float distance = Vector2.Distance(currentMousePos, startMousePos);

                if (distance > 1)
                {
                    line.enabled = true;

                    if (showLineOnScreen)
                        screenLine.enabled = true;
                }
            }
        }

    }
    void MouseRelease()
    {
        if (shootWhileMoving /*&& !EventSystem.current.IsPointerOverGameObject()*/)
        {
            Shoot();
            screenLine.enabled = false;
            line.enabled = false;
        }
        else
        {
            if (canShoot /*&& !EventSystem.current.IsPointerOverGameObject()*/)
            {
                Shoot();
                screenLine.enabled = false;
                line.enabled = false;
            }
        }

    }


    // ACTIONS  
    void LookAtShootDirection()
    {
        Vector3 dir = startMousePos - currentMousePos;

        if (forwardDraging)
        {
            transform.right = dir * -1;
        }
        else
        {
            transform.right = dir;
        }


        float dis = Vector2.Distance(startMousePos, currentMousePos);
        dis *= 4;


        if (dis < maxPower)
        {
            direction.localPosition = new Vector2(dis / 6, 0);
            shootPower = dis;
        }
        else
        {
            shootPower = maxPower;
            direction.localPosition = new Vector2(maxPower / 6, 0);
        }

    }
    public void Shoot()
    {
        canShoot = false;
        rb.velocity = transform.right * shootPower;
    }


    void DrawScreenLine()
    {



        screenLine.positionCount = 1;
        screenLine.SetPosition(0, startMousePos);


        screenLine.positionCount = 2;
        screenLine.SetPosition(1, currentMousePos);
    }

    void DrawLine()
    {

        startPosition = transform.position;

        line.positionCount = 1;
        line.SetPosition(0, startPosition);


        targetPosition = direction.transform.position;
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        line.positionCount = 2;
        line.SetPosition(1, targetPosition);
    }

    Vector3[] positions;


}
