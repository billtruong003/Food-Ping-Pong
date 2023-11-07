using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LinePull : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Vector2 dragStartPosition;
    [SerializeField] private Vector2 currentWorldMousePoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MainManager.Instance.m_currentState == MainManager.GameState.PLAYER_STATE)
        {
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && MainManager.Instance.m_currentState == MainManager.GameState.PLAYER_STATE)
        {
            Vector2 mousePosition = Input.mousePosition;
            currentWorldMousePoint = Camera.main.ScreenToWorldPoint(mousePosition);
            SetLinePull();
        }
        if (Input.GetMouseButtonUp(0))
        {
            line.enabled = false;
        }
    }
    private void SetLinePull()
    {
        line.enabled = true;

        line.SetPosition(0, dragStartPosition);
        Vector2 newPoint = currentWorldMousePoint - dragStartPosition;
        line.SetPosition(1, dragStartPosition + newPoint);
    }
}
