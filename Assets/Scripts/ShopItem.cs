using UnityEngine;

[System.Serializable]
public struct ShopItem
{
    public string category;
    public Sprite image;
    public string name;
    public int price;

    public bool isPurchased;

}
