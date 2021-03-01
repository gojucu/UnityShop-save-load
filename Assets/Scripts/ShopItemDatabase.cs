using UnityEngine;

[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Item shop database")]
public class ShopItemDatabase : ScriptableObject
{

    public ShopItem[] items;
    public TableChair[] tableChairs;
    public Plants[] plants;
    public SelectedItems selectedItems;
    public ItemCategories[] categories;

    //Item Counts
    public int ItemsCount
    {
        get { return items.Length; }
    }
    public int TablesCount
    {
        get { return tableChairs.Length; }
    }
    public int PlantCount
    {
        get { return plants.Length; }
    }

    public int CategoriesCount
    {
        get { return categories.Length; }
    }
    //GetItem
    public ShopItem GetShopItem(int index)
    {
        return items[index];
    }
    public TableChair GetTableItem(int index)
    {
        return tableChairs[index];
    }
    public Plants GetPlantItem(int index)
    {
        return plants[index];
    }



    public SelectedItems GetSettedItems()
    {
        return selectedItems;
    }
    public void PurchaseItem(int index)//Bunudamı her kategori için yapıcam öyle gibi unutma
    {
        items[index].isPurchased = true;
    }

}
