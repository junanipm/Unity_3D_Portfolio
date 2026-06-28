using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public int Progress = 0;
    public Image[] tutorial;
    public bool isPressed = false;
    public bool isfirstShopping = true;
    public bool isfirstHolding = true;
    public bool isfisrtDelivery = false;
    bool coffeetoastComplete = false;
    bool platingComplete = false;
    bool cafefirstOpen = false;
    public float passTime = 3.5f;
    public GameObject TutCol1;
    public GameObject TutCol2;
    public DeliveryProcess deliveryProcess;
    public CafeOpenObject cafeOpen;
    Item item;
    PlayerItem playerItem;
    private void Start()
    {   isPressed =false;
        tutorial[0].gameObject.SetActive(true);
        Invoke("GameStartTo02", passTime);
        item = GetComponent<Item>();
        playerItem = GetComponent<PlayerItem>();
    }
    void Update()
    {
        FirstQDown();
        if(playerItem.isfirstCoffee && playerItem.isfirstToast &&!coffeetoastComplete) 
        {
            ActiveToggle(11);
            StartCoroutine(WaitForPassTime(12));
            coffeetoastComplete = true;
        }
        if(item.isfirstPlateCoffee && item.isfirstPlateToast &&!platingComplete)
        {
            ActiveToggle(13);
            StartCoroutine(WaitForPassTime(14));
            platingComplete = true;
        }
        if(!cafefirstOpen && cafeOpen.test)
        {
            ImageDeActive(14);
            cafefirstOpen = true;
        }
    }
    void GameStartTo02()
    {
        ImageDeActive(0);
        ImageActive(1);
    }

    void GameStartTo03()
    {
        ImageDeActive(2);
        ImageActive(2);
        StartCoroutine(WaitForPassTime(3));
        StartCoroutine(WaitForPassTime(4));
        
    }

    void FirstQDown()
    {
        if(isPressed == false)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                isPressed = true;
                ActiveToggle(5);
            }
        }
        if(item.isHoldingBox == true && isfirstHolding)
        {
            ActiveToggle(8);
            isfirstHolding = false;
            isfisrtDelivery = true;
        }
    }
    public void FirstShopping()
    {
        if(isfirstShopping == true)
        {
            ImageDeActive(5);
            StartCoroutine(WaitForDeliveryTime(6));
            isfirstShopping = false;
        }
    }
    void ImageDeActive(int i)
    {   
        for(int index = i; index >=0; index--)
        {
            tutorial[index].gameObject.SetActive(false);
        }
        
    }
    void ImageActive(int i)
    {
        tutorial[i].gameObject.SetActive(true);
    }
    void OnTriggerEnter(Collider other) 
    {   

        if(other.tag == "TutCol")
        {
            GameStartTo03();
            TutCol1.SetActive(false);
        }
        if(other.tag == "TutCol2")
        {
            ImageActive(7);
            ImageDeActive(6);
            TutCol2.SetActive(false);
        }
        if(other.tag == "Delivery")
        {
            
            if(isfisrtDelivery == true)
            {
                ActiveToggle(9);
                StartCoroutine(WaitForPassTime(10));
                isfisrtDelivery = false;
            }
            
        }
    }
    

    IEnumerator WaitForPassTime(int i)
    {
        yield return new WaitForSeconds(passTime);
        ImageDeActive(i-1);
        ImageActive(i);
    }
    IEnumerator WaitForDeliveryTime(int i)
    {
        yield return new WaitForSeconds(deliveryProcess.deliveryWaitting);
        ImageDeActive(i-1);
        ImageActive(i);
    }
    void ActiveToggle(int i)
    {
        ImageDeActive(i-1);
        ImageActive(i);
    }
}
