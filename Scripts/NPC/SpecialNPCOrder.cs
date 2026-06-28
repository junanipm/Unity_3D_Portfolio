using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SpecialNPCOrder : NPCOrder
{
    public int npcID;

    public float timeLimit;
    private float npccurrentTime;

    public Slider NPCtimerSlider;
    public GameObject timerUIPrefab;
    private GameObject timerUIInstance;

    public bool isOrderComplete = false;
    public bool isOrderFailed = false;

    private Item playerItem;
    public string[] specialOrderList;
    public int[] orderCounts;
    public int NPCMoney = 10000;

    [Header("NPC 대사UI")]
    PlayerItem ppitem;

    
    UIManager uIManager;

    public override void FoodSetting()
    {
        Debug.Log("특별 NPC 주문 설정 중...");

    }

    protected override void CreateFoodOrderUI()
    {

        Debug.Log("특별 NPC는 이미지 생성을 건너뜁니다.");
    }
    void Start()
    {
        playerItem = FindObjectOfType<Item>();
        ppitem = FindObjectOfType<PlayerItem>();
        uIManager =  FindObjectOfType<UIManager>();

        if (playerItem == null)
        {
            Debug.LogError("PlayerItem을 찾을 수 없습니다.");
            return;
        }


        GameObject NPCTalkUI = GameObject.Find("EventCanvas"); 
        GameObject eventNPCText = GameObject.Find("NPCText");

    }

    public int GetRequiredFoodCount(string foodTag)
    {
        if (specialOrderList == null || orderCounts == null)
        {
            Debug.LogError("specialOrderList 또는 orderCounts가 초기화되지 않았습니다.");
            return 0;
        }

        for (int i = 0; i < specialOrderList.Length; i++)
        {
            if (specialOrderList[i] == foodTag)
            {
                return orderCounts[i];
            }
        }

        return 0;
    }

    public void OnOrderSuccess()
    {
        if (isOrderComplete)
            return;

        isOrderComplete = true;
        HandleOrderEnd(true);
        Debug.Log("특별 NPC의 주문이 성공적으로 완료되었습니다!");
    }

    public void CreateTimerUI()
    {
        if (timerUIPrefab != null)
        {
            timerUIInstance = Instantiate(timerUIPrefab, transform);
            timerSlider = timerUIInstance.GetComponentInChildren<Slider>();
            if (timerSlider != null)
            {
                timerSlider.minValue = 0;
                timerSlider.maxValue = timeLimit;
                timerSlider.value = timeLimit;
            }
        }
        else
        {
            Debug.LogError("타이머 UI 프리팹이 설정되지 않았습니다.");
        }
    }

    public void StartTimer()
    {
        npccurrentTime = timeLimit;
        StartCoroutine(UpdateTimer());
        
    }

    IEnumerator UpdateTimer()
    {
        while (npccurrentTime > 0)
        {
            npccurrentTime -= Time.deltaTime;
            if (timerSlider != null)
            {
                timerSlider.value = npccurrentTime;
            }
            yield return null;
        }
        OnOrderFailed();
    }

    void OnOrderFailed()
    {
        if (isOrderComplete || isOrderFailed)
            return;

        isOrderFailed = true;
        Debug.Log($"NPC {npcID} 주문 실패!");
        HandleOrderEnd(false);
    }

    public void OnOrderComplete()
    {
        if (isOrderComplete || isOrderFailed)
            return;

        isOrderComplete = true;
        Debug.Log($"NPC {npcID} 주문 성공!");
        HandleOrderEnd(true);
        
    }

    void HandleOrderEnd(bool success)
    {
        StopAllCoroutines();

        if (timerUIInstance != null)
        {
            Destroy(timerUIInstance);
        }

        if (success)
        {
                if(npcID == 1)
            {
                ppitem.Event01Clear = true;
            }
            else if (npcID == 2)
            {
                ppitem.Event02Clear = true;
            }
            else if(npcID == 3)
            {
                ppitem.Event03Clear = true;
            }
            Debug.Log($"NPC {npcID}의 주문이 성공적으로 처리되었습니다!");
            ppitem.CurrentMoney += NPCMoney;
            uIManager.NPC_FeedBack.gameObject.SetActive(true);
            uIManager.NPCFeedBack(true);

            Destroy(gameObject, 3f);
            Destroy(uIManager.NPC_FeedBack, 3f);
            
        }
        else
        {
            Debug.Log($"NPC {npcID}의 주문 처리에 실패했습니다.");
            uIManager.NPC_FeedBack.gameObject.SetActive(true);
            uIManager.NPCFeedBack(false);

            Destroy(gameObject, 3f);
            Destroy(uIManager.NPC_FeedBack, 3f);
            
        }


    }


    public void CheckOrderConditions()
    {
        if (specialOrderList == null || orderCounts == null || specialOrderList.Length != orderCounts.Length)
        {
            Debug.LogError("주문 목록 또는 개수 설정이 잘못되었습니다.");
            return;
        }

        bool allOrdersComplete = true;

        for (int i = 0; i < specialOrderList.Length; i++)
        {
            string food = specialOrderList[i];
            int requiredCount = orderCounts[i];
            int currentCount = GetPlatedFoodCount(food);

            if (currentCount < requiredCount)
            {
                Debug.Log($"{food} 필요 수량: {requiredCount}, 현재 수량: {currentCount}");
                allOrdersComplete = false;
            }
        }

        if (allOrdersComplete)
        {
            OnOrderComplete();
        }
        else
        {
            Debug.Log("주문 조건이 충족되지 않았습니다.");
        }
    }

    int GetPlatedFoodCount(string foodTag)
    {
        int count = 0;


        foreach (string plated in playerItem.platedFood)
        {
            if (plated == foodTag)
            {
                count++;
            }
        }

        return count;
    }

    public void StartEvent01()
    {
        /*

        if (NPCTalkUI == null)
        {

            GameObject canvasObject = Instantiate(timerUIPrefab, transform.position, Quaternion.identity);
            canvasObject.transform.SetParent(transform, false);
            NPCTalkUI = canvasObject;
        }


        if (EventNPC_Text == null)
        {
            TMP_Text tmpText = NPCTalkUI.GetComponentInChildren<TMP_Text>();
            if (tmpText != null)
            {
                EventNPC_Text = tmpText;
            }
            else
            {
                Debug.LogError("TMP_Text 컴포넌트를 찾을 수 없습니다.");
                return;
            }
        }
        

        EventBegin = true;
        ppitem.isShoping = true;

        if (evnet01_texts.Length > 0)
        {
            EventNPC_Text.text = evnet01_texts[0];
        }
        */
        uIManager.EventNPC_Text.text = uIManager.evnet01_texts[0];
        uIManager.EventBegin = true;
        ppitem.isShoping = true;
    }
    public void OredrStart()
    {
        ppitem.isShoping = false;
        CreateTimerUI();
        StartTimer();
        FoodSetting();
    }
    void EndingDivergance()
    {
        
        Debug.Log("ㅎㅊㅇㄷㄷ");
        if(ppitem.Event01Clear && ppitem.Event02Clear && ppitem.Event03Clear && ppitem.CurrentMoney >= 15000)
        {
            SceneManager.LoadScene("EndingStage01");
        }
        else if(ppitem.Event01Clear && ppitem.Event02Clear && ppitem.Event03Clear)
        {
            SceneManager.LoadScene("EndingStage02");
        }
        else
        {
            SceneManager.LoadScene("BadEndingStage");
        }
    }
     void OnDestroy() 
    {
        if(npcID == 3)
            EndingDivergance();
    }
}