using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderMiniGame : MonoBehaviour
{
    public Slider slider;
    public PlayerItem playerItem;
    public float speed;
    private bool canPressSpace = true;
    private bool isMoving = true;
    public float minRange = 0.7f;
    public float maxRange = 0.9f;
    public FoodSelector foodSelector;
    public Image[] SuccessFailUI;
    



    void Update()
    {
        if(playerItem.isMiniGaming == true)
        {
            
            if(isMoving)
            {
                slider.value += speed*Time.deltaTime;

                if(slider.value >= slider.maxValue)
                {
                    slider.value = slider.minValue;
                }
            }
            if((foodSelector.FoodValue == "Toast" &&(playerItem.BreadIngredient <= 0 || playerItem.EggIngredient <= 0)) ||(foodSelector.FoodValue == "Supple" && (playerItem.IceCreamIngredient <= 0 || playerItem.EggIngredient <= 0)))
            {
                Debug.Log("재료부족");
                playerItem.mainTothird();
            }

            if (Input.GetKeyDown(KeyCode.Space) && canPressSpace)
            {
                StartCoroutine(StopAndResetSlider());
                if(foodSelector.FoodValue == "Toast")
                {
                    playerItem.BreadIngredient -= 1;
                    playerItem.EggIngredient -= 1;
                }
                if(foodSelector.FoodValue == "Supple")
                {
                    playerItem.IceCreamIngredient -= 1;
                    playerItem.EggIngredient -= 1;
                }

            }
            
            
        }
        
    }

    IEnumerator StopAndResetSlider()
    {
        canPressSpace = false;
        isMoving = false;

        if(slider.value >= minRange && slider.value <= maxRange)
        {
            Debug.Log("님 점수 좋네여:"+ slider.value);
            playerItem.SpawnCoffee();
            StartCoroutine(ActiveSuccessImage(0));
            playerItem.SuccessSound();
        }
        else
        {
            playerItem.FailSound();
            StartCoroutine(ActiveSuccessImage(1));
        }

        yield return new WaitForSeconds(0.5f);

        slider.value = slider.minValue;
        isMoving = true;
        canPressSpace = true;
    }

    IEnumerator ActiveSuccessImage(int i)
    {
        SuccessFailUI[i].gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        SuccessFailUI[i].gameObject.SetActive(false);
    }
}
