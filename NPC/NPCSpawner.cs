using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    private GameObject npcPrefab;
    public GameObject[] npcList;
    public GameObject eventNPC;
    public int maxNPCCount = 10;
    private int currentNPCCount = 0;

    public float spawnInterval = 3f;
    private float minZ = 220f;
    private float maxZ = 250f;
    private float spawnY = 0f;
    private float spawnX = -180f;

    public UIManager uiManager;

    void Start()
    {

        StartCoroutine(SpawnNPC());
        
    }

    IEnumerator SpawnNPC()
    {
        while (true)
        {

            yield return new WaitForSeconds(spawnInterval);


            if (currentNPCCount < maxNPCCount)
            {
                SpawnRandomNPC();
            }
        }
    }

    void SpawnRandomNPC()
    {
        int npcindex = Random.Range(0, npcList.Length);

        Quaternion spawnRotation = Quaternion.Euler(0, -90, 0);
        
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, randomZ);
        GameObject npc = Instantiate(npcList[npcindex], spawnPosition, spawnRotation);



        currentNPCCount++;


        npc.GetComponent<NPCMovement>().OnNPCDestroyed += () => currentNPCCount--;
    }

    public void StopSpawningNPC()
    {
        StopCoroutine(SpawnNPC());
    }
    
    public void SpawnEventNPC()
    {   
        
        
        Quaternion spawnRotation = Quaternion.Euler(0, -90, 0);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, randomZ);
        GameObject evnetnpc = Instantiate(eventNPC, spawnPosition, spawnRotation);
        
        
    }
}
 
