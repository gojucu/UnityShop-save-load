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
		ResetShopList();//Bu hala lazımmı ?
		int len;
        if (index == 0)
        {
			len = itemDB.TablesCount;
		}else
        {
			len = itemDB.PlantCount;
		}

		for (int i = 0; i < len; i++)
		{

            switch (index)
            {
                case 0://Table&Chair
                    {
                        TableChair item = itemDB.GetTableItem(i);

                        ItemUI itemUI = Instantiate(ItemTemplate, ShopScrollView).GetComponent<ItemUI>();
                        itemUI.SetItemImage(item.image);
                        itemUI.SetItemPrice(item.price);
                        itemUI.SetItemID(item.itemID);

                        if (item.isPurchased)
                        {
                            itemUI.SetItemAsPurchased();
                            itemUI.OnItemSelect(item.itemID,index, OnItemSelected);
                        }
                        else
                        {
                            itemUI.OnItemPurchase(item.itemID,index, OnItemPurchased);
                        }
						SelectedItems selected = GameDataManager.GetSelectedItems();

						SetSelectedItems(selected, index);
						SelectItemUI(selected.TableChairID);
						break;
                    }

                default://Plant
                    {
                        Plants item = itemDB.GetPlantItem(i);
                        ItemUI itemUI = Instantiate(ItemTemplate, ShopScrollView).GetComponent<ItemUI>();
                        itemUI.SetItemImage(item.image);
                        itemUI.SetItemPrice(item.price);
                        itemUI.SetItemID(item.itemID);

                        if (item.isPurchased)
                        {
                            itemUI.SetItemAsPurchased();
                            itemUI.OnItemSelect(item.itemID,index, OnItemSelected);
                        }
                        else
                        {
                            itemUI.OnItemPurchase(item.itemID,index, OnItemPurchased);
                        }
						SelectedItems selected = GameDataManager.GetSelectedItems();

						SetSelectedItems(selected, index);
						SelectItemUI(selected.PlantID);
						break;
                    }
            }

        }

		//Select UI item
		//int categoriesSelectedItem = GameDataManager.GetSelectedItemsIndex();

	}

    void SetSelectedItems(SelectedItems selected,int category)
    {
        //Get saved index
        //int index = GameDataManager.GetSelectedItemsIndex();

			GameDataManager.SetSelectedItem(selected);

        //Set selected character
        //GameDataManager.SetSelectedItem(itemDB.GetShopItem(index), index);
    }


    void OnItemSelected(int i,int catID)
    {
		//Get selecteds
		SelectedItems selected = GameDataManager.GetSelectedItems();

		//Select item in the UI
		SelectItemUI(i);

        if (catID == 0)
        {
			selected.TableChairID = i;
        }
        else
        {
			selected.PlantID = i;
        }

		//Save Data
		GameDataManager.SetSelectedItem(selected);
		Debug.Log("Selected"+i);
	}

	void SelectItemUI(int selected)
	{
		//ShopItem item = itemDB.GetShopItem(itemIndex);
		var d = FindObjectsOfType<ItemUI>();
		foreach (ItemUI itemUI in d)
        {
			itemUI.DeselectItem();
		}

        ItemUI newUiItem = null;

		//int id;
		//if (cat == 0)//table
  //      {
		//	id = selected.TableChairID;
  //      }
  //      else//Plant
  //      {
		//	id = selected.PlantID;
		//}
        foreach (ItemUI itemUI in from ItemUI itemUI in d
                               where itemUI.GetComponent<ItemUI>().GetItemID() == selected
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

	void OnItemPurchased (int itemIndex,int catID)
	{
		int price;

        if (catID == 0)
        {
			TableChair item = itemDB.GetTableItem(itemIndex);
			price = item.price;
        }
        else
        {
			Plants item = itemDB.GetPlantItem(itemIndex);
			price = item.price;
		}

        if (GameDataManager.CanSpendCoins(price))
        {
			//Proceed with the purchase operation
			GameDataManager.SpendCoins(price);

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
