using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class WeapInfo : MonoBehaviour
{
    [Header("AssignComponent")]
    [SerializeField] private Image weapSprite;
    [SerializeField] private TextMeshProUGUI weapName;
    [SerializeField] private TextMeshProUGUI rare;
    [SerializeField] private TextMeshProUGUI priceTxt;


    [Header("Button")]
    [SerializeField] private Button buy;
    [SerializeField] private Button use;
    [SerializeField] private TextMeshProUGUI inUse;

    [Header("Transform")]
    [SerializeField] private Transform textContainer;
    [SerializeField] private GameObject textPopUp;

    [Header("Info")]
    [SerializeField] private WeapID idWeap;
    [SerializeField] private Sprite weapIcon;
    [SerializeField] private string weaponName;
    [SerializeField] private string rarity;
    [SerializeField] private string price;
    [SerializeField] private bool unlocked;
    private ShopController shopController;
    public void SetInfoWeap()
    {
        weapSprite.sprite = weapIcon;
        weapName.text = weaponName;
        rare.text = rarity;
        priceTxt.text = price;
    }

    public void SetShopController(ShopController script)
    {
        shopController = script;
    }
    public void SetInfo(Sprite icon, string weaponName, string rarity, string price, WeapID idWeap)
    {
        weapIcon = icon;
        this.weaponName = weaponName;
        this.rarity = rarity;
        this.price = price;
        this.idWeap = idWeap;
        SetInfoWeap();
        CheckUnlocked();
        gameObject.SetActive(true);
    }

    public void SetRareColor(List<Color> cols)
    {
        if (rarity == "UNIQUE")
        {
            SetUniqueColor(cols);
        }
        else
        {
            SetOtherColor(cols);
        }
    }
    public void SetOtherColor(List<Color> rarityCol)
    {
        TMP_TextInfo textInfo = rare.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int vertexIndex = textInfo.characterInfo[i].vertexIndex + j;
                textInfo.meshInfo[0].colors32[vertexIndex] = rarityCol[0];
            }
        }
    }
    public void SetUniqueColor(List<Color> rarityCol)
    {
        TMP_TextInfo textInfo = rare.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int vertexIndex = textInfo.characterInfo[i].vertexIndex + j;
                textInfo.meshInfo[0].colors32[vertexIndex] = rarityCol[j];
            }
        }
    }

    public void Unlocked()
    {
        buy.gameObject.SetActive(false);
        use.gameObject.SetActive(true);
        priceTxt.text = "Owned";
        priceTxt.color = Color.gray;
        inUse.transform.parent.gameObject.SetActive(false);
    }

    public void WeapInUse()
    {
        buy.gameObject.SetActive(false);
        use.gameObject.SetActive(false);
        inUse.transform.parent.gameObject.SetActive(true);
    }
    public void ChangePickItem()
    {
        shopController.ChangePickedItem(idWeap, this);
    }
    public void NotUnLocked()
    {
        buy.gameObject.SetActive(true);
        use.gameObject.SetActive(false);
        inUse.transform.parent.gameObject.SetActive(false);
    }

    public void PopUpText(string txt = "")
    {
        StartCoroutine(Cor_PopUpText(txt));
    }

    public void CheckInUse()
    {
        StartCoroutine(Cor_CheckWeapInUse());
    }
    public void BuyItem()
    {
        StartCoroutine(Cor_BuyItem());
    }

    public void CheckUnlocked()
    {
        StartCoroutine(Cor_CheckUnlocked());
    }

    private IEnumerator Cor_BuyItem()
    {
        yield return new WaitUntil(() => PlayerData.Instance != null);
        if (PlayerData.Instance.GetMoneyInt() <= int.Parse(price))
        {
            PopUpText();
            yield break;
        }
        PlayerData.Instance.DecreaseMoney(int.Parse(price));
        PlayerData.Instance.unlockItem(idWeap);
        Unlocked();
    }

    private IEnumerator Cor_CheckUnlocked()
    {
        yield return new WaitUntil(() => PlayerData.Instance != null);
        if (PlayerData.Instance.GetWeapUnlocked().Contains(idWeap))
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }

        if (unlocked)
        {
            Unlocked();
            CheckInUse();
            yield break;
        }
        NotUnLocked();

    }

    private IEnumerator Cor_CheckWeapInUse()
    {
        yield return new WaitUntil(() => PlayerData.Instance != null);
        if (PlayerData.Instance.GetWeapInUse() == idWeap)
        {
            WeapInUse();
        }
    }

    public IEnumerator Cor_PopUpText(string txt)
    {
        GameObject txtPopUp = Instantiate(textPopUp, textContainer);
        // List<string> notice = new List<string>()
        // {
        //     "You're broken, get some money bitch!~",
        //     "You're out of money",
        //     "You broken bitch, go get some money!",
        //     "Stop spaming, you're broken, get some money now!",
        // };
        // int rndnum = Random.Range(0, notice.Count);
        // TextMeshProUGUI tmpText = txtPopUp.GetComponent<TextMeshProUGUI>();
        // tmpText.text = notice[rndnum];
        yield return new WaitForSeconds(1.2f);
        Destroy(txtPopUp);
    }
}
