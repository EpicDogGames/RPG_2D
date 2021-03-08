using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;

    public GameObject gameMenu;
    public GameObject[] windows;

    private CharStats[] playerStats;

    public Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] charStatHolder;

    public GameObject[] playerStatusButtons;

    public Text statusName, statusHP, statusMP, statusStrength, statusDefense, statusWeaponEquipped, statusWeaponPower, statusArmorEquipped, statusArmorPower, statusExp;
    public Image statusImage;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;

    public GameObject itemCharacterChoiceMenu;
    public Text[] itemCharacterChoiceName;

    public Text goldText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("Fire2"))
       {
           if (gameMenu.activeInHierarchy)
           {
            //    gameMenu.SetActive(false);
            //    GameManager.instance.gameMenuOpen = false;
                CloseMenu();
           }
           else
           {
                gameMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
           }
       } 
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for (int i=0; i<playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                lvlText[i].text = "Level: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                charImage[i].sprite = playerStats[i].charImage;
            }
            else
            {
                charStatHolder[i].SetActive(false);
            }
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i=0; i<windows.Length; i++)
        {
            if (i == windowNumber)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }

        itemCharacterChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for (int i=0; i<windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        gameMenu.SetActive(false);

        GameManager.instance.gameMenuOpen = false;

        itemCharacterChoiceMenu.SetActive(false);
    }

    public void OpenStatusWindow()
    {
        UpdateMainStats();

        // update the information to be shown
        StatusCharacter(0);
        
        for (int i=0; i<playerStatusButtons.Length; i++)
        {
            playerStatusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            playerStatusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }
    }

    public void StatusCharacter(int selectedCharacter)
    {
        statusName.text = playerStats[selectedCharacter].charName;
        statusHP.text = "" + playerStats[selectedCharacter].currentHP + "/" + playerStats[selectedCharacter].maxHP;
        statusMP.text = "" + playerStats[selectedCharacter].currentMP + "/" + playerStats[selectedCharacter].maxMP;
        statusStrength.text = playerStats[selectedCharacter].strength.ToString();
        statusDefense.text = playerStats[selectedCharacter].defense.ToString();
        if (playerStats[selectedCharacter].equippedWeapon != "")
        {
            statusWeaponEquipped.text = playerStats[selectedCharacter].equippedWeapon;
        }
        statusWeaponPower.text = playerStats[selectedCharacter].weaponPower.ToString();
        if (playerStats[selectedCharacter].equippedArmor != "")
        {
            statusArmorEquipped.text = playerStats[selectedCharacter].equippedArmor;
        }
        statusArmorPower.text = playerStats[selectedCharacter].armorPower.ToString();
        statusExp.text = (playerStats[selectedCharacter].expToNextLevel[playerStats[selectedCharacter].playerLevel] - playerStats[selectedCharacter].currentEXP).ToString();
        statusImage.sprite = playerStats[selectedCharacter].charImage;
    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();

        for (int i=0; i<itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].numberOfItemsText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].numberOfItemsText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        if (activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.itemDescription;
    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void OpenItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(true);

        for(int i=0; i<itemCharacterChoiceName.Length; i++)
        {
            itemCharacterChoiceName[i].text = GameManager.instance.playerStats[i].charName;
            itemCharacterChoiceName[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }
    }

    public void CloseItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectedCharacter)
    {
        activeItem.Use(selectedCharacter);
        CloseItemCharacterChoice();
    }

    public void SaveGame()
    {
        GameManager.instance.SaveData();
        QuestManager.instance.SaveQuestData();
    }
}
