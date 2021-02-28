using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class Shop : MonoBehaviour
{
	#region Singlton:Shop

	public static Shop Instance;

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}

	#endregion

	//[System.Serializable] public class ShopItem
	//{
	//	public Sprite Image;
	//	public int Price;
	//	public bool IsPurchased = false;
	//}

	//[SerializeField] Transform ShopItemsContainer;

	//public List<ShopItem> ShopItemsList;//Silinicek ?
	[SerializeField] ShopItemDatabase itemDB;

	[SerializeField] Animator NoCoinsAnim;
 

	[SerializeField] GameObject ItemTemplate;
	//GameObject g;
	[SerializeField] Transform ShopScrollView;
	[SerializeField] GameObject ShopPanel;
    //Silinicek ?1
    //Button buyBtn;
    //Button itemBtn;
    //Outline itemOutline;
    //1
    //int newSelectedItemIndex = 0;
    //int previousSelectedItemIndex = 0;

    void Start ()
	{
		//Fill the shop's UI list with items
		ListShopItems(0);



		//Set selected character in the playerDataManager .
	

		//SelectItemUI(GameDataManager.GetSelectedItemsIndex());


	}
	public void ListShopItems(int index)
    {
		ResetShopList();

		int len = itemDB.ItemsCount;
		for (int i = 0; i < len; i++)
		{
			ShopItem item = itemDB.GetShopItem(i);
			
			if (item.categoryID == index)
			{
				if (item.isDefault == true)
				{
					GameDataManager.SetDefaults(item);
					//if (GameDataManager.GetSelectedItemsCount() == 0)
					//{
					//	Debug.Log(GameDataManager.GetSelectedItemsCount());
					//	int dd = GameDataManager.GetSelectedItemsIndex(index);
					//	ShopItem sItem = itemDB.GetShopItem(dd);
					//	if (sItem.isDefault != false)
					//	{
					//if(GameDataManager.GetDefaultValues(item.categoryID)!=true)
							SetSelectedItems(item.itemID);
					//	}
					//}
				}

				ItemUI itemUI = Instantiate(ItemTemplate, ShopScrollView).GetComponent<ItemUI>();
				itemUI.SetItemImage(item.image);
				itemUI.SetItemPrice(item.price);
				itemUI.SetItemID(item.itemID);

				if (item.isPurchased)
                {
					itemUI.SetItemAsPurchased();
					itemUI.OnItemSelect(item.itemID, OnItemSelected);
                }
                else
                {
					itemUI.OnItemPurchase(item.itemID, OnItemPurchased);
                }
			}
		}
		//Select UI item
		int categoriesSelectedItem = GameDataManager.GetSelectedItemsIndex(index);
		Debug.Log(categoriesSelectedItem+"Hata burada");
		SetSelectedItems(categoriesSelectedItem);
		SelectItemUI(categoriesSelectedItem);
	}

	void SetSelectedItems(int index)
    {
        //Get saved index
        //int index = GameDataManager.GetSelectedItemsIndex();

        //Set selected character
        GameDataManager.SetSelectedItem(itemDB.GetShopItem(index), index);
    }

    private void MakeDefaultItemFalse(int index)
    {
        ShopItem item = itemDB.GetShopItem(index);
        if (item.isDefault != true)
        {
			int len = itemDB.ItemsCount;
			for (int i = 0; i < len; i++)
			{
                if (item.categoryID == i)
				{
					//GameDataManager.SetDefaults(item);
					int defaultIndex=GameDataManager.GetDefaultValues(item.categoryID);
					itemDB.BreakDefault(defaultIndex);
					//item.isDefault = false;
				}
			}
		}
    }

    void OnItemSelected(int i)
    {
		//Select item in the UI
		SelectItemUI(i);

		//Remove Default
		MakeDefaultItemFalse(i);

		//Save Data
		GameDataManager.SetSelectedItem(itemDB.GetShopItem(i), i);
		Debug.Log("Selected"+i);
	}
	void SelectItemUI(int itemIndex)
	{
		//ShopItem item = itemDB.GetShopItem(itemIndex);
		var d = FindObjectsOfType<ItemUI>();
		foreach (ItemUI itemUI in d)
        {
			itemUI.DeselectItem();
		}

        ItemUI newUiItem = null;
        foreach (ItemUI itemUI in from ItemUI itemUI in d
                               where itemUI.GetComponent<ItemUI>().GetItemID() == itemDB.items[itemIndex].itemID
                               select itemUI)
        {
            newUiItem = itemUI;
            newUiItem.SelectItem();
        }
	}
	//ItemUI GetItemUI(int index)//Buraya item ın id ini yollamaya çalış
	//{
	//	return ShopScrollView.GetChild(index).GetComponent<ItemUI>();
	//}
	void ResetShopList()
    {
		foreach(Transform child in ShopScrollView)
        {
            Destroy(child.gameObject);
		}
    }

	void OnItemPurchased (int itemIndex)
	{
		ShopItem item = itemDB.GetShopItem(itemIndex);
        if (GameDataManager.CanSpendCoins(item.price))
        {
			//Proceed with the purchase operation
			GameDataManager.SpendCoins(item.price);

			//Update DB's Data
			itemDB.PurchaseItem(itemIndex);

			//change itemUI
			//ListShopItems(item.categoryID);// bu hoşuma gitmedi aşağıdakinin proplemi ne çöz
			var d = FindObjectsOfType<ItemUI>();
            foreach (ItemUI itemUI in from ItemUI itemUI in d
                                   where itemUI.GetItemID() == itemIndex
                                   select itemUI)
            {
                itemUI.SetItemAsPurchased();
                itemUI.OnItemSelect(item.itemID, OnItemSelected);
            }

            //Add purchased item to Shop Data
            GameDataManager.AddPurchasedCharacter(itemIndex);

			//change UI text: coins
			Game.Instance.UpdateAllCoinsUIText();



		}
        else
        {
			NoCoinsAnim.SetTrigger("NoCoins");
			Debug.Log("You don't have enough coins!!");
		}

	}

	/*---------------------Open & Close shop--------------------------*/ 
	public void OpenShop ()
	{
		ShopPanel.SetActive (true);
	}

	public void CloseShop ()
	{
		ShopPanel.SetActive (false);
	}

}
