using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryProcess : MonoBehaviour
{
    public PlayerItem playerItem;
    public ShoppingSystem shoppingSystem;

    public GameObject box;
    public Vector3 BoxSpawnPosition;
    public int deliveryWaitting = 10;
    public Image DeliveryMassage;
    public AudioClip Delivery;

    private IEnumerator BlockPurchaseForSeconds(int seconds)
    {
        shoppingSystem.canPurchase = false;
        Debug.Log("일단");
        yield return new WaitForSeconds(seconds);
        Debug.Log("기다리긴했음");
        SoundManager.instance.SFXPlay("Delivery", Delivery);
        Instantiate(box, BoxSpawnPosition, Quaternion.identity);


    }

    public void WaitAndSpawn()
    {
        StartCoroutine(BlockPurchaseForSeconds(deliveryWaitting));
        StartCoroutine(DeliveryUIActive(deliveryWaitting));
    }

    IEnumerator DeliveryUIActive(int seconds)
    {   
        yield return new WaitForSeconds(seconds);
        DeliveryMassage.gameObject.SetActive(true);
    }

}
