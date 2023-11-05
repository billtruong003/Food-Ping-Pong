using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }
    [SerializeField] private Transform inventorySlot;

    [Header("Info Weapon")]
    [SerializeField] private List<Image> weaponCount;
    [SerializeField] private Image infoWeapon;
    [SerializeField] private TextMeshProUGUI countNum;

    [SerializeField] private GameOver gameOver;

    [SerializeField] private GameObject popUpText;
    [SerializeField] private Transform popUpTxtContainer;
    private readonly int uiLayer = 5;
    private bool isHoverUI;

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
    public void GameOver()
    {
        gameOver.TriggerGameOject();
    }
    public void PopUpText(string txt = "")
    {
        StartCoroutine(Cor_PopUpText(txt));
    }
    public IEnumerator Cor_PopUpText(string txt)
    {
        GameObject txtPopUp = Instantiate(popUpText, popUpTxtContainer);
        TextMeshProUGUI InfoText = txtPopUp.GetComponent<TextMeshProUGUI>();
        InfoText.text = txt;
        yield return new WaitForSeconds(1);
        Destroy(txtPopUp);
    }
    public void ShowDescriptionMat(MaterialSlot matInfo)
    {
        if (matInfo.MatName == "")
            return;
        if (!matInfo.infoObject.activeSelf)
            matInfo.infoObject.SetActive(true);
        string description = MainManager.Instance.GetDescriptionMaterial(matInfo.MatName);
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

    public void InitWeaponInfo(Sprite weapImg)
    {
        infoWeapon.sprite = weapImg;
        infoWeapon.gameObject.SetActive(true);
        foreach (Image img in weaponCount)
        {
            img.sprite = weapImg;
            img.transform.parent.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
    public void SetCountWeaponTurn(int count)
    {
        countNum.text = count.ToString();
    }
    public void DecreaseWeaponCount(int index)
    {
        if (index <= 0)
        {
            return;
        }
        weaponCount[index - 1].transform.parent.GetComponent<Animator>().SetTrigger("Disappear");
    }
    public void ResetWeaponCount()
    {
        StartCoroutine(ResetWeapCount());
    }
    public IEnumerator ResetWeapCount()
    {
        yield return new WaitForSeconds(1f);
        foreach (Image img in weaponCount)
        {
            img.transform.parent.GetComponent<Animator>().SetTrigger("Appear");
            yield return new WaitForSeconds(0.5f);
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
            if (curRaysastResult.gameObject.layer == uiLayer)
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
