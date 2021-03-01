using UnityEngine;

[System.Serializable]
public struct ShopItem
{

    public int itemID;
    public int categoryID;
    public Sprite image;
    public string name;
    public int price;

    public bool isDefault;
    public bool isPurchased;

}


[System.Serializable]
public struct SelectedItems// Bu hala lazımmı ***????
{
    public int TableChairID;
    public int PlantID;
}

