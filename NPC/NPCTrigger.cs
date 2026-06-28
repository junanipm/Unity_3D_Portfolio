using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public int currentTouchedNPCs = 0;
    public bool NPCComeToCafe = false;
    public int maxNPCsAllowed = 5;

    void Start()
    {
        Debug.Log("Starting Trigger...");
        UpdateTriggerState(NPCComeToCafe, currentTouchedNPCs >= maxNPCsAllowed);
    }

    public void SetNPCComeToCafe(bool value)
    {
        Debug.Log($"Setting NPCComeToCafe to {value}");
        NPCComeToCafe = value;
        UpdateTriggerState(NPCComeToCafe, currentTouchedNPCs >= maxNPCsAllowed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {

            if(gameObject.tag == "CanGo")
            {
            currentTouchedNPCs++;
            
            Debug.Log($"{other.name} entered trigger. Total NPCs passed: {currentTouchedNPCs}");
            UpdateTriggerState(NPCComeToCafe, currentTouchedNPCs >= maxNPCsAllowed);
            }
        }
    }

    public void UpdateTriggerState(bool npcComeToCafe, bool cantGo)
    {


        if (cantGo)
        {
            gameObject.tag = "CantGo";

        }
        else if (npcComeToCafe)
        {
            gameObject.tag = "CanGo";
            Debug.Log("Tag updated to CanGo for NPCTrigger");
        }
        else
        {
            gameObject.tag = "CantGo";
            Debug.Log("Tag updated to CantGo for NPCTrigger");
        }


    }
}