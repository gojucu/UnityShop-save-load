using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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
	GameObject g;
	[SerializeField] Transform ShopScrollView;
	[SerializeField] GameObject ShopPanel;
	//Silinicek ?1
	//Button buyBtn;
	//Button itemBtn;
	//Outline itemOutline;
	//1
	int newSelectedItemIndex = 0;
	int previousSelectedItemIndex = 0;

	void Start ()
	{
		//Fill the shop's UI list with items
		ListShopItems(0);

		//Set selected character in the playerDataManager .
	

		//Select UI item
		for (int i = 0; i < itemDB.CategorisCount; i++)
		{
			int categoriesSelectedItem = GameDataManager.GetSelectedItemsIndex(i);
			SetSelectedItems(categoriesSelectedItem);
			SelectItemUI(categoriesSelectedItem);
		}
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
				ItemUI itemUI = Instantiate(ItemTemplate, ShopScrollView).GetComponent<ItemUI>();
				itemUI.SetItemImage(item.image);
				itemUI.SetItemPrice(item.price);

				if (item.isPurchased)
                {
					itemUI.SetItemAsPurchased();
					itemUI.OnItemSelect(i, OnItemSelected);
                }
                else
                {
					itemUI.OnItemPurchase(i, OnItemPurchased);
                }
			}
		}
	}

	void SetSelectedItems(int index)
	{
		//Get saved index
		//int index = GameDataManager.GetSelectedItemsIndex();

		//Set selected character
		GameDataManager.SetSelectedItem(itemDB.GetShopItem(index), index);
	}

	void OnItemSelected(int i)
    {
		//Select item in the UI

		SelectItemUI(i);

		//Save Data
		GameDataManager.SetSelectedItem(itemDB.GetShopItem(i), i);
		Debug.Log("Selected"+i);
	}
	void SelectItemUI(int itemIndex)
	{
		ShopItem item = itemDB.GetShopItem(itemIndex);
		for(int i = 0; i< itemDB.ItemsCount; i++)
        {
			if (item.categoryID == itemDB.items[i].categoryID)
			{
				ItemUI itemUIs = GetItemUI(i);

				itemUIs.DeselectItem();
			}
		}

		//previousSelectedItemIndex = newSelectedItemIndex;
		newSelectedItemIndex = itemIndex;

		//ItemUI prevUiItem = GetItemUI(previousSelectedItemIndex);
		ItemUI newUiItem = GetItemUI(newSelectedItemIndex);

		newUiItem.SelectItem();

	}
	ItemUI GetItemUI(int index)
    {
		return ShopScrollView.GetChild(index).GetComponent<ItemUI>();
    }
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
