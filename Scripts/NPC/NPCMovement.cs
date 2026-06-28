using System.Collections;
using System.Collections.Generic;

using UnityEngine;


/*
    public event NPCDestroyedHandler OnNPCDestroyed;
    public delegate void NPCDestroyedHandler();

    public float minSpeed = 10f;
    public float maxSpeed = 15f;
    private float npcSpeed;

    public bool NPCComeToCafe = false;
    public float xMoveSpeed = 5f;
    private float targetX = 180f;
    public delegate void NPCDestroyedHandler();

    public Rigidbody npcrigid;

    public bool[] isNPCInPlace = new bool[6];

    private bool hasCheckedXThreshold = false;

     void Awake()
    {
        npcrigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        npcSpeed = Random.Range(minSpeed, maxSpeed);


        npcrigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {

        if (!hasCheckedXThreshold && transform.position.x >= 0f)
        {
            hasCheckedXThreshold = true;
            Debug.Log("0 지나감");
            if (NPCComeToCafe)
            {
                StartCoroutine(MoveToCafe());
                Debug.Log("이동시작");
            }
        }


        if (!NPCComeToCafe || !hasCheckedXThreshold)
        {
            MoveInXAxis();
        }


        if (transform.position.x >= targetX)
        {
            Destroy(gameObject);
        }
    }

    void MoveInXAxis()
    {

        transform.Translate(npcSpeed * Time.deltaTime, 0, 0, Space.World);
    }
    /
    IEnumerator MoveToCafe()
    {
        int availableIndex = -1;


        for (int i = 0; i < NPCManager.Instance.isNPCInPlace.Length; i++)
        {
            if (!NPCManager.Instance.isNPCInPlace[i])
            {
                availableIndex = i;
                NPCManager.Instance.isNPCInPlace[i] = true;
                Debug.Log($"NPC {gameObject.name} has reserved spot {i}");
                break;
            }
        }


        if (availableIndex == -1)
        {
            Debug.Log($"NPC {gameObject.name} couldn't find an available spot, continuing to move forward.");
            StartCoroutine(ContinueMovingForward());
            yield break;
        }

        Vector3 targetPosition = NPCManager.Instance.npcWaitP[availableIndex];


        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            npcrigid.MovePosition(Vector3.MoveTowards(npcrigid.position, targetPosition, npcSpeed * Time.deltaTime));

            transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return null;
        }

        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            transform.position = targetPosition;
            yield break;
        }
        Debug.Log($"NPC {gameObject.name} arrived at spot {availableIndex}. Spot {availableIndex} is now occupied.");
    }
    /

    IEnumerator MoveToCafe()
    {
        if (NPCManager.Instance.AreAllSpotsFull())
        {
            Debug.Log($"NPC {gameObject.name} noticed that all spots are full. Continuing forward.");
            StartCoroutine(ContinueMovingForward());
            yield break;
        }

        Vector3 waypoint1 = new Vector3(-25.5f, 1f, 50f);
        Vector3 waypoint2 = new Vector3(-25.5f, 4.25f, 30f);


        yield return StartCoroutine(MoveToPosition(waypoint1));
        Debug.Log($"NPC {gameObject.name} reached waypoint 1 at {waypoint1}");


        yield return StartCoroutine(MoveToPosition(waypoint2));
        Debug.Log($"NPC {gameObject.name} reached waypoint 2 at {waypoint2}");


        int reservedIndex = NPCManager.Instance.ReserveSpot();
        if (reservedIndex == -1)
        {
            Debug.Log($"NPC {gameObject.name} couldn't find an available spot. Continuing forward.");
            StartCoroutine(ContinueMovingForward());
            yield break;
        }


        Vector3 targetPosition = NPCManager.Instance.npcWaitP[reservedIndex];
        yield return StartCoroutine(MoveToPosition(targetPosition));
        Debug.Log($"NPC {gameObject.name} arrived at spot {reservedIndex}.");
    }


    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        Debug.Log($"NPC {gameObject.name} starts moving to {targetPosition}");
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetPosition, npcSpeed * Time.deltaTime);
            npcrigid.MovePosition(nextPosition);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return null;
        }

        transform.position = targetPosition;
        Debug.Log($"NPC {gameObject.name} arrived at {targetPosition}");
    }


    IEnumerator ContinueMovingForward()
    {
        while (transform.position.x < targetX)
        {
            MoveInXAxis();
            yield return null;
        }

        if (transform.position.x >= targetX)
        {
            Debug.Log($"NPC {gameObject.name} exited the scene.");
            Destroy(gameObject);
        }
    }
    */

public class NPCMovement : MonoBehaviour
{
    public event NPCDestroyedHandler OnNPCDestroyed;
    public delegate void NPCDestroyedHandler();

    public float minSpeed = 15f;
    public float maxSpeed = 15f;
    private float npcSpeed;

    public bool isMovingToCafe = false;
    public float targetX = 180f;

    private Rigidbody npcrigid;

    private int reservedIndex = -1;


    void Awake()
    {
        npcrigid = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        npcSpeed = Random.Range(minSpeed, maxSpeed);

    }

    void Update()
    {
        if (isMovingToCafe) return;


        transform.Translate(npcSpeed * Time.deltaTime, 0, 0, Space.World);


        if (transform.position.x >= targetX)
        {
            Debug.Log($"{gameObject.name} exited the scene.");
            Destroy(gameObject);
        }
        LogMemoryUsage();
        if (this == null || gameObject == null)
        return;
    }
    public void SetReservedIndex(int index)
    {
        reservedIndex = index;
    }

    void LogMemoryUsage()
    {

    }

    public void StartCafeRoute()
    {
        if (isMovingToCafe) return;

        isMovingToCafe = true;
        StartCoroutine(MoveToAdditionalWaypointAndCafe());
    }

    public void ContinueForward()
    {
        if (isMovingToCafe) return;

        Debug.Log($"{gameObject.name} continues forward.");
        StartCoroutine(ContinueMovingForward());
    }


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("CanGo") && !isMovingToCafe)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isMovingToCafe = true;
            Debug.Log($"{gameObject.name} is moving to the cafe.");
            StartCoroutine(MoveToAdditionalWaypointAndCafe());
        }
        else if (other.CompareTag("CantGo"))
        {

        }
    }


    IEnumerator MoveToAdditionalWaypointAndCafe()
    {
        Debug.Log($"{gameObject.name} is starting cafe route.");


        Vector3 additionalWaypoint = new Vector3(-25f, 1f, 200f);
        yield return StartCoroutine(MoveToPosition(additionalWaypoint));
        Debug.Log($"{gameObject.name} reached the additional waypoint.");


        Vector3 waypoint1 = new Vector3(-25.5f, 1f, 50f);
        yield return StartCoroutine(MoveToPosition(waypoint1));


        Vector3 waypoint1_5 = new Vector3(-25.5f, 4.25f, 41f);
        yield return StartCoroutine(MoveToPosition(waypoint1_5));


        Vector3 waypoint2 = new Vector3(-25.5f, 4.25f, 30f);
        yield return StartCoroutine(MoveToPosition(waypoint2));

    

        reservedIndex = NPCManager.Instance.ReserveSpot();
        if (reservedIndex == -1)
        {
            Debug.Log($"{gameObject.name} couldn't find an available spot. Returning to forward movement.");
            ContinueForward();
            yield break;
        }


        transform.rotation = Quaternion.Euler(0, -90, 0);
        Vector3 targetPosition = NPCManager.Instance.npcWaitP[reservedIndex];
        SetReservedIndex(reservedIndex);
        yield return StartCoroutine(MoveToPosition(targetPosition));

    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, npcSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;

    }

    IEnumerator ContinueMovingForward()
    {
        while (transform.position.x < 180f)
        {
            transform.Translate(npcSpeed * Time.deltaTime, 0, 0, Space.World);
            yield return null;
        }

        Debug.Log($"{gameObject.name} exited the scene.");
        Destroy(gameObject);
    }
    private Coroutine currentCoroutine;

    void StopCurrentCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }

    void OnDestroy()
    {
        StopCurrentCoroutine();


        if (reservedIndex != -1)
        {
            NPCManager.Instance.ReleaseSpot(reservedIndex);
            reservedIndex = -1;
        }
    }
}