using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class NPCManager : MonoBehaviour
{
    public static NPCManager Instance;

    public NPCTrigger nPCTrigger;

    public bool[] isNPCInPlace = new bool[5];
    public bool isCafeFull = false;
    public Vector3[] npcWaitP = new Vector3[5]
    {
        new Vector3(-9.25f, 4.25f, 21.8f),
        new Vector3(-21.85f, 4.25f, 21f),
        new Vector3(-31.25f, 4.25f, 21.5f),
        new Vector3(-16.75f, 4.25f, 42.25f),
        new Vector3(-9.33f, 4.25f, 44f)
    };

    private int totalNpcTouchedCount = 0;
    private const int maxTouchedCount = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < isNPCInPlace.Length; i++)
        {
            isNPCInPlace[i] = false;
        }
        UpdateCafeStatus();
        UpdateTriggerState();
    }

    public void IncrementNpcTouchedCount()
    {
        totalNpcTouchedCount++;
        Debug.Log($"Total NPC touched count: {totalNpcTouchedCount}/{maxTouchedCount}");
        UpdateTriggerState();
    }

    public void ResetTouchedCount()
    {
        totalNpcTouchedCount = 0;
        Debug.Log("NPC Touched Count Reset.");
        UpdateTriggerState();
    }

    public void UpdateCafeStatus()
    {
        isCafeFull = true;

        foreach (bool spot in isNPCInPlace)
        {
            if (!spot)
            {
                isCafeFull = false;
                break;
            }
        }


        UpdateTriggerState();
    }

    public int ReserveSpot()
    {
        if (isCafeFull)
        {
            Debug.Log("No available spots. Cafe is full.");
            return -1;
        }

        for (int i = 0; i < isNPCInPlace.Length; i++)
        {
            if (!isNPCInPlace[i])
            {
                isNPCInPlace[i] = true;
                UpdateCafeStatus();
                UpdateTriggerState();

                return i;
            }
        }

        Debug.Log("No available spots. All spots are reserved.");
        return -1;
    }

    public void UpdateTriggerState()
    {
        NPCTrigger trigger = FindObjectOfType<NPCTrigger>();
        if (trigger != null)
        {
            trigger.UpdateTriggerState(trigger.NPCComeToCafe, isCafeFull || trigger.currentTouchedNPCs >= 5);
        }

    }

    public void ReleaseSpot(int index)
    {
        if (index < 0 || index >= isNPCInPlace.Length)
        {
            Debug.LogWarning($"Invalid spot index: {index}. Cannot release.");
            return;
        }

        if (isNPCInPlace[index])
        {
            isNPCInPlace[index] = false;
            UpdateCafeStatus();
            Debug.Log($"Spot {index} has been released.");
        }
        else
        {
            Debug.LogWarning($"Spot {index} is already empty.");
        }
    }
}
