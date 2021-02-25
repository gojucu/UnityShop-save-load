using UnityEngine;

[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Item shop database")]
public class ShopItemDatabase : ScriptableObject
{

    public ShopItem[] items;

    public int ItemsCount
    {
        get { return items.Length; }
    }
    public ShopItem GetShopItem(int index)
    {
        return items[index];
    }
    public void PurchaseItem(int index)
    {
        items[index].isPurchased = true;
    }
}
