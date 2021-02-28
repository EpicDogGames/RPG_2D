using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen;
    public bool dialogActive;
    public bool fadingBetweenAreas;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        SortItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }

        // if (Input.GetKeyDown(KeyCode.J))
        // {
        //     AddItem("Iron Armor");
        //     AddItem("Bad Voodoo");

        //     RemoveItem("Health Potion");
        //     RemoveItem("Good Voodoo");
        // }
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i=0; i<referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while(itemAfterSpace)
        {
            itemAfterSpace = false;
            for (int i=0; i<itemsHeld.Length-1; i++)
            {
               if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i+1];
                    itemsHeld[i+1] = "";

                    numberOfItems[i] = numberOfItems[i+1];
                    numberOfItems[i+1] = 0;

                    if (itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }

        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for (int i=0; i<itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;

            for (int i=0; i<referenceItems.Length; i++)
            {
                if (referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    i = referenceItems.Length;
                }
            }

            if (itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.Log("This item is missing from the reference items: " + itemToAdd);
            }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i=0; i<itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;
                i = itemsHeld.Length;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.Log("Couldn't find the item: " + itemToRemove);
        }
    }
}
