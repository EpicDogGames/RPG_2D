﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public static ShopMenu instance;

    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public string[] itemsForSale;
    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectedItem;
    public Text buyItemName, buyItemDescription, buyItemValue;
    public Text sellItemName, sellItemDescription, sellItemValue;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !shopMenu.activeInHierarchy)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();
        
        GameManager.instance.shopActive = true;
        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyItemButtons[0].Press();

        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].numberOfItemsText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].numberOfItemsText.text = "";
            }
        }
    }

    public void OpenSellMenu()
    {
        sellItemButtons[0].Press();

        sellMenu.SetActive(true);
        buyMenu.SetActive(false);

        ShowSellItems();
    }

    private void ShowSellItems()
    {
        GameManager.instance.SortItems();

        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButtons[i].numberOfItemsText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemButtons[i].numberOfItemsText.text = "";
            }
        }        
    }

    public void SelectBuyItem(Item buyItem)
    {
        selectedItem = buyItem;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = "Value: " + selectedItem.itemValue + "g";
    }

    public void SelectSellItem(Item sellItem)
    {
        selectedItem = sellItem;
        sellItemName.text = selectedItem.itemName;
        sellItemDescription.text = selectedItem.itemDescription;
        sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.itemValue * 0.5f).ToString() + "g";
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.currentGold >= selectedItem.itemValue)
            {
                GameManager.instance.currentGold -= selectedItem.itemValue;
                GameManager.instance.AddItem(selectedItem.itemName);
            }

            goldText.text = GameManager.instance.currentGold.ToString() + "g";
        }
    }

    public void SellItem()
    {
        if (selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.itemValue * 0.5f);
            GameManager.instance.RemoveItem(selectedItem.itemName);
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";

        ShowSellItems();
    }
}
