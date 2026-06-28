using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class NPCSeat : MonoBehaviour
{

    Animator animator;
    NPCOrder npcOrder;

    


    void Awake( )
    {
        animator = GetComponentInChildren<Animator>();
        npcOrder = GetComponent<NPCOrder>();
        
    }


    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("ChildCollider"))
        {
            
            Debug.Log("ss");
            return;
        }

        else if (other.CompareTag("Seat"))
        {

            animator.SetBool("isSeat", true);
            if (npcOrder is SpecialNPCOrder specialOrder)
            {
                HandleSpecialNPC(specialOrder);
            }
            else
            {
                npcOrder.FoodSetting();
            }
        }
        
    }
    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Seat"))
        {
            animator.SetBool("isSeat", false);
        }
    }
    void HandleSpecialNPC(SpecialNPCOrder specialOrder)
    {

        Debug.Log($"특별 NPC {specialOrder.npcID} 주문 준비 중...");
        
        /*
        specialOrder.CreateTimerUI();
        specialOrder.StartTimer();


        specialOrder.FoodSetting();
        */



        
        specialOrder.StartEvent01();
    }
}
