using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    private bool canOpen;

    public string[] ItemsForSale = new string[40];
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && !ShopMenu.instance.shopMenu.activeInHierarchy)
        {
            ShopMenu.instance.itemsForSale = ItemsForSale;
            ShopMenu.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            canOpen = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player") 
        {
            canOpen = false;
        }   
    }
}
