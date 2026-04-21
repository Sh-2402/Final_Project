using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("Coins")]
    public int coins = 500;

    [Header("Equipped Item")]
    public int equippedItemId = -1;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanAfford(int price)
    {
        return coins >= price;
    }

    public bool SpendCoins(int price)
    {
        if (coins < price)
            return false;

        coins -= price;
        RefreshAllItems();
        return true;
    }

    public void EquipItem(int itemId)
    {
        equippedItemId = itemId;
        RefreshAllItems();
    }

    public void RefreshAllItems()
    {
        ShopItem[] allItems = FindObjectsByType<ShopItem>(FindObjectsSortMode.None);

        foreach (ShopItem item in allItems)
        {
            item.RefreshUI();
        }
    }
}