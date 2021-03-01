using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//Shop Data Holder
[System.Serializable]
public class CharactersShopData
{
    public List<int> purchasedCharactersIndexes = new List<int>();
    public List<int> purchasedTableIndexes = new List<int>();
    public List<int> purchasedPlantIndexes = new List<int>();

    public int categoryCount;

    public List<int> defaultBrokensIndexes = new List<int>();
}

//Player Data Holder
[System.Serializable]
public class PlayerData
{
    public int coins = 1000;
    public List<int> selectedCharacterIndex = new List<int>();
    public SelectedItems selectedItems;
    public ItemCategories[] itemCats=new ItemCategories[10];//Bu 10 u elinle manuel ayarlamak zorunda olabilirsin ****
    

}


public static class GameDataManager
{
    static PlayerData playerData = new PlayerData();
    static CharactersShopData charactersShopData = new CharactersShopData();

    //static SelectedItems selectedItems;// Seçili malzemeler bunlar gibi olucak Çiçek,masa

    //static ShopItemDatabase itemDB;
    static GameDataManager()
    {
        //LoadPlayerData();
        //LoadCharactersShopData();
    }

    //Player Data Methods -----------------------------------------------------------------------------
    public static SelectedItems GetSelectedItems()
    {
        return playerData.selectedItems;
    }

    public static int GetSelectedItemIndex(int catID)//****** BU kullanman gereken
    {
        return playerData.itemCats[catID].selectedItemID;
    }

    public static void SetSelectedItem(int itemID,int catID)
    {
        playerData.itemCats[catID].selectedItemID = itemID;
        //SavePlayerData();
    }



    //public static int GetSelectedItemsIndex()//SelectedItems mı kullanıyorum bunun yerine ?
    //{
    //    return playerData.selectedItems.TableChairID;
    //}


    public static int GetSelectedItemsCount()//Bune 
    {
        return playerData.selectedCharacterIndex.Count;
    }

    public static int GetCoins()
    {
        return playerData.coins;
    }

    public static void AddCoins(int amount)
    {
        playerData.coins += amount;
       // SavePlayerData();
    }

    public static bool CanSpendCoins(int amount)
    {
        return (playerData.coins >= amount);
    }

    public static void SpendCoins(int amount)
    {
        playerData.coins -= amount;
       // SavePlayerData();
    }

    //static void LoadPlayerData()
    //{
    //    playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
    //    UnityEngine.Debug.Log("<color=green>[PlayerData] Loaded.</color>");
    //}

    //static void SavePlayerData()
    //{
    //    BinarySerializer.Save(playerData, "player-data.txt");
    //    UnityEngine.Debug.Log("<color=magenta>[PlayerData] Saved.</color>");
    //}

    //Characters Shop Data Methods -----------------------------------------------------------------------------
    public static void AddPurchasedCharacter(int characterIndex)
    {
        charactersShopData.purchasedCharactersIndexes.Add(characterIndex);
        //SaveCharactersShoprData();
    }

    public static List<int> GetAllPurchasedCharacter()
    {
        return charactersShopData.purchasedCharactersIndexes;
    }


    public static int GetPurchasedCharacter(int index)
    {
        return charactersShopData.purchasedCharactersIndexes[index];
    }

    //static void LoadCharactersShopData()
    //{
    //    charactersShopData = BinarySerializer.Load<CharactersShopData>("characters-shop-data.txt");
    //    UnityEngine.Debug.Log("<color=green>[CharactersShopData] Loaded.</color>");
    //}

    //static void SaveCharactersShoprData()
    //{
    //    BinarySerializer.Save(charactersShopData, "characters-shop-data.txt");
    //    UnityEngine.Debug.Log("<color=magenta>[CharactersShopData] Saved.</color>");
    //}
}
