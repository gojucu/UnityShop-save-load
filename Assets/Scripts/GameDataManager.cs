using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//Shop Data Holder
[System.Serializable]
public class CharactersShopData
{
    public List<int> purchasedCharactersIndexes = new List<int>();
}

//Player Data Holder
[System.Serializable]
public class PlayerData
{
    public int coins = 0;
    public int selectedCharacterIndex = 0;
}


public static class GameDataManager
{
    static PlayerData playerData = new PlayerData();
    static CharactersShopData charactersShopData = new CharactersShopData();

    static ShopItem selectedItem;// Seçili malzemeler bunlar gibi olucak Çiçek,masa

    static GameDataManager()
    {
        //LoadPlayerData();
        //LoadCharactersShopData();
    }

    //Player Data Methods -----------------------------------------------------------------------------
    public static ShopItem GetSelectedCharacter()
    {
        return selectedItem;
    }
     
    public static void SetSelectedCharacter(ShopItem item, int index)
    {
        selectedItem = item;
        playerData.selectedCharacterIndex = index;
        //SavePlayerData();
    }

    public static int GetSelectedCharacterIndex()
    {
        return playerData.selectedCharacterIndex;
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
