﻿using UnityEngine;
using UnityEngine.UI;
//using TMPro;
using UnityEngine.Events;

public class ItemUI : MonoBehaviour
{
	[SerializeField] Color itemNotSelectedColor;
	[SerializeField] Color itemSelectedColor;

	[Space(20f)]
	[SerializeField] int itemID;
	[SerializeField] Sprite itemImage;
	[SerializeField] Button itemPurchaseButton;
	[SerializeField] Text ItemPriceText;

	[Space(20f)]
	[SerializeField] Button itemButton;
	//[SerializeField] Image itemImage;
	[SerializeField] Outline itemOutline;

	public void SetItemImage(Sprite sprite)
	{
		itemImage = sprite;
	}
	public void SetItemID(int id)
	{
		itemID = id;
	}
	public int GetItemID()
	{
		return itemID;
	}
	public void SetItemPrice(int price)
	{
		ItemPriceText.text = price.ToString();
	}

	public void SetItemAsPurchased()
	{
		itemPurchaseButton.interactable = false;
		itemPurchaseButton.transform.GetChild(0).GetComponent<Text>().text = "OWNED";
		itemButton.interactable = true;

		//itemImage = itemNotSelectedColor;
	}
	public void OnItemPurchase(int itemIndex,int catID, UnityAction<int,int> action)
	{
		itemPurchaseButton.onClick.RemoveAllListeners();
		itemPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex, catID));
	}

	public void OnItemSelect(int itemIndex,int catID, UnityAction<int,int> action)
	{
		itemButton.interactable = true;

		itemButton.onClick.RemoveAllListeners();
		itemButton.onClick.AddListener(() => action.Invoke(itemIndex,catID));
	}

	public void SelectItem()
	{
		itemOutline.enabled = true;
		//itemImage.color = itemSelectedColor;
		itemButton.interactable = false;
	}

	public void DeselectItem()
	{
		itemOutline.enabled = false;
		//itemImage.color = itemNotSelectedColor;
		itemButton.interactable = true;
	}
}
