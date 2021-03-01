using UnityEngine;

[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "Shopping/Item shop database")]
public class ShopItemDatabase : ScriptableObject
{

    public ShopItem[] items;
    public ShopItem tempItem;
    //Önceki DENEME3**
    //public TableChair[] tableChairs;
    //public Plants[] plants;
    //Önceki DENEME3**

    public SelectedItems selectedItems;
    public ItemCategories[] categories;

    //Item Counts
    public int ItemsCount
    {
        get { return items.Length; }
    }
    //public int TablesCount
    //{
    //    get { return tableChairs.Length; }
    //}
    //public int PlantCount
    //{
    //    get { return plants.Length; }
    //}

    public int CategoriesCount
    {
        get { return categories.Length; }
    }
    //GetItem
    public ShopItem GetShopItem(int index,int catID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            ShopItem shopItem = items[i];
            if (shopItem.categoryID == catID && shopItem.itemID == index)
            {
                tempItem = shopItem;
            }
        }
        return tempItem;
    }
    //public TableChair GetTableItem(int index)
    //{
    //    return tableChairs[index];
    //}
    //public Plants GetPlantItem(int index)
    //{
    //    return plants[index];
    //}



    //public SelectedItems GetSettedItems()
    //{
    //    return selectedItems;
    //}
    public int GetSelectedItemsID(int catID)// Bu ne ?
    {
        return categories[catID].selectedItemID;
    }
    public void PurchaseItem(int id, int catID)//Bunudamı her kategori için yapıcam öyle gibi unutma** Gerek yok çözüldü gibi ?
    {
        for (int i = 0; i < items.Length; i++)
        {
            ShopItem shopItem1 = items[i];
            if (shopItem1.itemID == id && shopItem1.categoryID == catID)
            {
                //shopItem1.isPurchased = true;
                items[i].isPurchased = true;
            }
        }
        //items[index].isPurchased = true;
    }

}
