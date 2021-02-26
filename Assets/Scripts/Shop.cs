using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

	public List<ShopItem> ShopItemsList;
	[SerializeField] ShopItemDatabase itemDB;

	[SerializeField] Animator NoCoinsAnim;
 

	[SerializeField] GameObject ItemTemplate;
	GameObject g;
	[SerializeField] Transform ShopScrollView;
	[SerializeField] GameObject ShopPanel;
	Button buyBtn;

	void Start ()
	{
		ListShopItems(0);
		//int len = itemDB.ItemsCount ;
		//for (int i = 0; i < len; i++) {
		//          ShopItem item = itemDB.GetShopItem(i);
		//          if (item.category == "1")
		//          {
		//		g = Instantiate(ItemTemplate, ShopScrollView);
		//		g.transform.GetChild(0).GetComponent<Image>().sprite = item.image;
		//		g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = item.price.ToString();
		//		buyBtn = g.transform.GetChild(2).GetComponent<Button>();
		//		if (item.isPurchased)
		//		{
		//			DisableBuyButton();
		//		}
		//		buyBtn.AddEventListener(i, OnShopItemBtnClicked);
		//	}
		//}
	}
	public void ListShopItems(int index)
    {
		ResetShopList();
		int len = itemDB.ItemsCount;
		for (int i = 0; i < len; i++)
		{
			ShopItem item = itemDB.GetShopItem(i);
			if (item.category == index.ToString())
			{
				g = Instantiate(ItemTemplate, ShopScrollView);
				g.transform.GetChild(0).GetComponent<Image>().sprite = item.image;
				g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = item.price.ToString();
				buyBtn = g.transform.GetChild(2).GetComponent<Button>();
				if (item.isPurchased)
				{
					DisableBuyButton();
				}
				buyBtn.AddEventListener(i, OnShopItemBtnClicked);
			}
		}
	}
	void ResetShopList()
    {
		foreach(Transform child in ShopScrollView)
        {
            Destroy(child.gameObject);
		}
    }

	void OnShopItemBtnClicked (int itemIndex)
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


			//disable the button
			buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
			DisableBuyButton();
			//change UI text: coins
			Game.Instance.UpdateAllCoinsUIText();
		}
        else
        {
			NoCoinsAnim.SetTrigger("NoCoins");
			Debug.Log("You don't have enough coins!!");
		}

	}

	void DisableBuyButton ()
	{
		buyBtn.interactable = false;
		buyBtn.transform.GetChild (0).GetComponent <Text> ().text = "PURCHASED";
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
