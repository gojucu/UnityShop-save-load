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
public struct TableChair
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
public struct Plants
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
public struct SelectedItems
{
    public int TableChairID;
    public int PlantID;
}

