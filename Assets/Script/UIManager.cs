using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    [SerializeField] PickUpSpawner pickUpSpawner;
    [SerializeField] private PickUpConfig gameConfig;
    [SerializeField] private GameObject inventList;
    [SerializeField] private GameObject inventMenu;
    [SerializeField] private Transform inventorySlot;
    int UILayer = 5;
    bool isHoverUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void DecreaseMaterial(string name)
    {
        foreach (Transform child in inventorySlot)
        {
            MaterialSlot matSlot = child.GetComponent<MaterialSlot>();
            if (matSlot.MatName == name)
            {
                matSlot.MaterialDecreased();
                break;
            }
        }
    }
    public void ShowDescriptionMat(MaterialSlot matInfo)
    {
        if (matInfo.MatName == "")
            return;
        if (!matInfo.infoObject.activeSelf)
            matInfo.infoObject.SetActive(true);
        string description = GameManager.Instance.GetDescriptionMaterial(matInfo.MatName);
        matInfo.ShowInfo(description);
    }
    public void CheckInventory(string matName)
    {
        foreach (Transform slot in inventorySlot)
        {
            MaterialSlot matSlot = slot.GetComponent<MaterialSlot>();
            if (matSlot.MatName == "")
            {
                matSlot.MaterialTrigger(matName);
                return;
            }
            if (matSlot.MatName == matName)
            {
                matSlot.MaterialAdded();
                return;
            }
        }
    }
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
