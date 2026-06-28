using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodSelector : MonoBehaviour
{
    public GameObject drinkUIPanel;
    public GameObject foodUIPanel;




    public string FoodValue;
    public string DrinkValue;


    PlayerItem playerItem;

    public Image lackImage;
    void Start()
    {





        foodUIPanel.SetActive(false);
        drinkUIPanel.SetActive(false);
        playerItem = GetComponent<PlayerItem>();
        lackImage.gameObject.SetActive(false);
    }

    public void FoodUIActive()
    {
        foodUIPanel.SetActive(true);
    }

    public void DrinkUIActive()
    {
        drinkUIPanel.SetActive(true);
    }

    public void FoodSelected01()
    {
        if(playerItem.BreadIngredient >0 && playerItem.EggIngredient >0)
        {
            FoodValue = "Toast";
            foodUIPanel.SetActive(false);
            playerItem.isShoping = false;
            playerItem.isMiniGaming = true;
            playerItem.ToggleSlider();
        }
        else
        {
            ShowTextCoroutine();
        }
    }
    

    public void FoodSelected02()
    {
        if(playerItem.EggIngredient >0 &&playerItem.IceCreamIngredient >0)
        {
            FoodValue = "Supple";
            foodUIPanel.SetActive(false);
            playerItem.isShoping = false;
            playerItem.isMiniGaming = true;
            playerItem.ToggleSlider();
        }
        else
        {
            ShowTextCoroutine();
        }
    }


    public void DrinkSelected01()
    {
        if(playerItem.CoffeeIngredient >0 && playerItem.MilkIngredient >0)
        {
            DrinkValue = "Coffee";

            drinkUIPanel.SetActive(false);
            playerItem.isShoping = false;
            playerItem.isCakeGaming = true;
            playerItem.ToggleKeyGame();
        }
        else
        {
            ShowTextCoroutine();
        }
    }

    public void DrinkSelected02()
    {
        if(playerItem.MelonIngredient >0 && playerItem.IceCreamIngredient >0)
        {
            DrinkValue = "MelonSoda";

            drinkUIPanel.SetActive(false);
            playerItem.isShoping = false;
            playerItem.isCakeGaming = true;
            playerItem.ToggleKeyGame();
        }
        else
        {
            ShowTextCoroutine();
        }
    }

    

    private IEnumerator ShowTextCoroutine()
    {
        lackImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        lackImage.gameObject.SetActive(false);
    }

}
