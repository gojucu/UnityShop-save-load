using UnityEngine;

[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Item shop database")]
public class ShopItemDatabase : ScriptableObject
{

    public ShopItem[] items;
    public SettedItems settedItems;
    public ItemCategories[] categories;

    public int ItemsCount
    {
        get { return items.Length; }
    }
    public int CategoriesCount
    {
        get { return categories.Length; }
    }
    public ShopItem GetShopItem(int index)
    {
        return items[index];
    }
    public SettedItems GetSettedItems()
    {
        return settedItems;
    }
    public void PurchaseItem(int index)
    {
        items[index].isPurchased = true;
    }
    public void BreakDefault(int index)
    {
        items[index].isDefault = false;
    }
}
