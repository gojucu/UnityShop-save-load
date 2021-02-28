using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//Shop Data Holder
[System.Serializable]
public class CharactersShopData
{
    public List<int> purchasedCharactersIndexes = new List<int>();
    public int categoryCount;
}

//Player Data Holder
[System.Serializable]
public class PlayerData
{
    public int coins = 1000;
    public List<int> selectedCharacterIndex = new List<int>();
    public SettedItems setted;
}


public static class GameDataManager
{
    static PlayerData playerData = new PlayerData();
    static CharactersShopData charactersShopData = new CharactersShopData();

    static SettedItems settedItems;// Seçili malzemeler bunlar gibi olucak Çiçek,masa
    //static ShopItemDatabase itemDB;
    static GameDataManager()
    {
        for(int i =0; i < 2; i++)
        {
            playerData.selectedCharacterIndex.Insert(i, 0);
        }
        //LoadPlayerData();
        //LoadCharactersShopData();
    }

    //Player Data Methods -----------------------------------------------------------------------------
    public static SettedItems GetSelectedItems()
    {
        return settedItems;
    }
     
    public static void SetSelectedItem(ShopItem item, int index)
    {
        //ItemCategories cat = item.category; int yap
        switch (item.categoryID)
        {
            case 0:
                settedItems.TableChairID = index;//geçici olarak index kullan olmaz ise shop itema id ekle
                break;
            case 1:
                settedItems.PlantID = index;
                break;

            default:
                break;
        }

        playerData.selectedCharacterIndex.Insert(item.categoryID,index);
        //SavePlayerData();
    }

    public static int GetSelectedItemsIndex(int i)
    {
        return playerData.selectedCharacterIndex[i];
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
