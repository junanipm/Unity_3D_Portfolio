using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : MonoBehaviour
{

    public PlayerItem playerItem;
    public ShoppingSystem shoppingSystem;

    public GameObject box;
    public Vector3 BoxSpawnPosition;

    public int boxCoffee;
    public int boxMilk;
    public int boxEgg;
    public int boxBread;
    public int boxIceCream;
    public int boxMelon;
    public int boxCheese;
    Item item;
    DeliveryProcess deliveryProcess;
    public void Start()
    {
        item = FindObjectOfType<Item>();
        deliveryProcess = FindObjectOfType<DeliveryProcess>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        
        if(other.tag == "Delivery")
        {
            playerItem.CoffeeIngredient += boxCoffee;
            playerItem.BreadIngredient += boxBread;
            playerItem.MilkIngredient += boxMilk;
            playerItem.EggIngredient += boxEgg;
            playerItem.IceCreamIngredient += boxIceCream;
            playerItem.MelonIngredient += boxMelon;
            playerItem.CheeseIngredient += boxCheese;
            

            boxCoffee = 0;
            boxBread = 0;
            boxMilk = 0;
            boxEgg = 0;
            boxIceCream = 0;
            boxMelon = 0;
            boxCheese = 0;
            
            shoppingSystem.canPurchase = true;
            
            gameObject.SetActive(false);
            item.isHoldingBox = false;
            deliveryProcess.DeliveryMassage.gameObject.SetActive(false);
        }
    }

    

    
}
