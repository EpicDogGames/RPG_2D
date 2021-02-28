using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item - General")]
    public string itemName;
    public string itemDescription;
    public int itemValue;
    public Sprite itemSprite;

    [Header("Item - Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStrength;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int characterToUseOn)
    {
        CharStats selectedCharacter = GameManager.instance.playerStats[characterToUseOn];

        if (isItem)
        {
            if (affectHP)
            {
                selectedCharacter.currentHP += amountToChange;

                if (selectedCharacter.currentHP > selectedCharacter.maxHP)
                {
                    selectedCharacter.currentHP = selectedCharacter.maxHP;
                }
            }

            if (affectMP)
            {
                selectedCharacter.currentMP += amountToChange;

                if (selectedCharacter.currentMP > selectedCharacter.maxMP)
                {
                    selectedCharacter.currentMP = selectedCharacter.maxMP;
                }
            }

            if (affectStrength)
            {
                selectedCharacter.strength += amountToChange;
            }
        }

        if (isWeapon)
        {
            if (selectedCharacter.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedCharacter.equippedWeapon);
            }

            selectedCharacter.equippedWeapon = itemName;
            selectedCharacter.weaponPower = weaponStrength;
        }

        if (isArmor)
        {
            if (selectedCharacter.equippedArmor != "")
            {
                GameManager.instance.AddItem(selectedCharacter.equippedArmor);
            }

            selectedCharacter.equippedArmor = itemName;
            selectedCharacter.armorPower = armorStrength;
        }

        GameManager.instance.RemoveItem(itemName);
    }
}
