using UnityEngine;

[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Item shop database")]
public class ShopItemDatabase : ScriptableObject
{

    public ShopItem[] items;
    public Table[] tables;
    public ItemCategories[] categories;

    public int ItemsCount
    {
        get { return items.Length; }
    }
    public int CategorisCount
    {
        get { return categories.Length; }
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
