using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("Item Info")]
    public int itemId;
    public int price;
    public string itemName = "Item";

    [Header("State")]
    public bool isPurchased = false;

    [Header("Buttons")]
    public Button purchaseButton;
    public Button equipButton;

    private TMP_Text purchaseText;
    private TMP_Text equipText;
    private Image purchaseImage;
    private Image equipImage;

    [Header("Button Colours")]
    public Color purchaseAvailableColor = new Color32(46, 204, 113, 255); // green
    public Color purchasedColor = new Color32(30, 132, 73, 255); // dark green
    public Color lockedColor = new Color32(127, 140, 141, 255); // grey
    public Color equipColor = new Color32(241, 196, 15, 255); // yellow
    public Color equippedColor = new Color32(0, 188, 212, 255); // cyan

    [Header("Text Colours")]
    public Color purchaseTextColor = new Color32(255, 184, 77, 255); // gold/orange
    public Color purchasedTextColor = new Color32(255, 213, 128, 255); // light gold
    public Color lockedTextColor = new Color32(230, 230, 230, 255); // light grey
    public Color equipTextColor = new Color32(44, 44, 44, 255); // dark text
    public Color equippedTextColor = Color.white; // white

    private void Start()
    {
        purchaseText = purchaseButton.GetComponentInChildren<TMP_Text>();
        equipText = equipButton.GetComponentInChildren<TMP_Text>();

        purchaseImage = purchaseButton.GetComponent<Image>();
        equipImage = equipButton.GetComponent<Image>();

        RefreshUI();
    }

    public void OnPurchasePressed()
    {
        if (isPurchased)
            return;

        if (!ShopManager.Instance.CanAfford(price))
            return;

        bool success = ShopManager.Instance.SpendCoins(price);

        if (success)
        {
            isPurchased = true;
            RefreshUI();
        }
    }

    public void OnEquipPressed()
    {
        if (!isPurchased)
            return;

        ShopManager.Instance.EquipItem(itemId);
    }

    public void RefreshUI()
    {
        bool isEquipped = ShopManager.Instance.equippedItemId == itemId;

        // PURCHASE BUTTON
        if (isPurchased)
        {
            purchaseText.text = itemName + " Purchased";
            purchaseText.color = purchasedTextColor;
            purchaseImage.color = purchasedColor;
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseText.text = "Purchase " + itemName;
            purchaseText.color = purchaseTextColor;
            purchaseImage.color = ShopManager.Instance.CanAfford(price) ? purchaseAvailableColor : lockedColor;
            purchaseButton.interactable = ShopManager.Instance.CanAfford(price);
        }

        // EQUIP BUTTON
        if (!isPurchased)
        {
            equipText.text = itemName + " Locked";
            equipText.color = lockedTextColor;
            equipImage.color = lockedColor;
            equipButton.interactable = false;
        }
        else if (isEquipped)
        {
            equipText.text = itemName + " Equipped";
            equipText.color = equippedTextColor;
            equipImage.color = equippedColor;
            equipButton.interactable = true;
        }
        else
        {
            equipText.text = "Equip " + itemName;
            equipText.color = equipTextColor;
            equipImage.color = equipColor;
            equipButton.interactable = true;
        }
    }
}