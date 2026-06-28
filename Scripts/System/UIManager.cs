using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{

    public TMP_Text BreadText;
    public TMP_Text CoffeeText;
    public TMP_Text MilkText;
    public TMP_Text EggText;
    public TMP_Text IceCreamText;
    public TMP_Text MelonText;
    public TMP_Text CheeseText;

    public TMP_Text MoneyText;
    public PlayerItem playerItem;

    public Material newSkybox;
    public float elapsedTime;
    public float gameDuration = 600f;
    private int currentHour = 9;
    private int currentMinute = 0;

    private float hourInterval = 60f;
    private float halfHourInterval = 30f;
    public TMP_Text timeText; 
    public TMP_Text ampmText;
    bool hascalled = false;
    public SpecialNPCOrder specialNPC;
    NPCSpawner npcSpawner;
    public GameObject EventUI;
    public bool isPaused;
    public bool EventBegin;

    [Header("이벤트 UI")]
    public GameObject NPCtalkUI;
    public TMP_Text EventNPC_Text;
    public string[] evnet01_texts;
    private int clickCount = 0;
    public GameObject NPC_FeedBack;
    
    public TMP_Text NPC_FeedBackText;
    public string[] eventfeedbackText;



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateAllUI();
        if (BreadText == null)
            BreadText = GameObject.Find("BreadText").GetComponent<TMP_Text>();
        if (CoffeeText == null)
            CoffeeText = GameObject.Find("CoffeeText").GetComponent<TMP_Text>();
        if (MilkText == null)
            MilkText = GameObject.Find("MilkText").GetComponent<TMP_Text>();
        if (EggText == null)
            EggText = GameObject.Find("EggText").GetComponent<TMP_Text>();
        if (IceCreamText == null)
            IceCreamText = GameObject.Find("IceCreamText").GetComponent<TMP_Text>();
        if (MelonText == null)
            MelonText = GameObject.Find("MelonText").GetComponent<TMP_Text>();
        if (CheeseText == null)
            CheeseText = GameObject.Find("CheeseText").GetComponent<TMP_Text>();
        if (MoneyText == null)
            MoneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();
    }
    void Start()
    {
        clickCount = 0;
        
        npcSpawner = FindObjectOfType<NPCSpawner>();
        gameDuration = 600f;
        playerItem = FindObjectOfType<PlayerItem>();
        if (playerItem == null)
        {
            Debug.LogError("PlayerItem이 할당되지 않았습니다!");
        }
        UpdateAllUI();

        

        if (BreadText == null) Debug.LogWarning("BreadText가 연결되지 않았습니다!");
        if (CoffeeText == null) Debug.LogWarning("CoffeeText가 연결되지 않았습니다!");
        
    }

    void Update()
    {
        if(playerItem != null)
        {
            BreadText.text = playerItem.BreadIngredient.ToString();
            CoffeeText.text =  playerItem.CoffeeIngredient.ToString();
            MilkText.text = playerItem.MilkIngredient.ToString();
            EggText.text =  playerItem.EggIngredient.ToString();
            IceCreamText.text =  playerItem.IceCreamIngredient.ToString();
            MelonText.text =  playerItem.MelonIngredient.ToString();
            CheeseText.text = playerItem.CheeseIngredient.ToString();
        }

        MoneyText.text = playerItem.CurrentMoney.ToString();
        
        UpdateSkyBox();

        if(specialNPC != null)
        {
            if(EventBegin == true)
            {
                
                EventUI.SetActive(true);
                
            }
            else if(EventBegin == false)
            {
                
                EventUI.SetActive(false);
            }
        }
    }
    public void UpdateAllUI()
    {
        playerItem = FindObjectOfType<PlayerItem>();
        if(playerItem != null)
        {
            BreadText.text = playerItem.BreadIngredient.ToString();
            CoffeeText.text =  playerItem.CoffeeIngredient.ToString();
            MilkText.text = playerItem.MilkIngredient.ToString();
            EggText.text =  playerItem.EggIngredient.ToString();
            IceCreamText.text =  playerItem.IceCreamIngredient.ToString();
            MelonText.text =  playerItem.MelonIngredient.ToString();
            CheeseText.text = playerItem.CheeseIngredient.ToString();
        }

        MoneyText.text = playerItem.CurrentMoney.ToString();
        
        UpdateSkyBox();

        UpdateTimeUI();
        
        
    }

    public void StartGameTime()
    {
        StartCoroutine(UpdateGameTime());
    }
    public IEnumerator UpdateGameTime()
    {
        while (elapsedTime < gameDuration)
        {
            if(!isPaused)
            {
                yield return new WaitForSeconds(0.1f);
                elapsedTime += 0.1f;


                int totalHalfHoursPassed = Mathf.FloorToInt(elapsedTime / halfHourInterval);
                currentHour = 10 + (totalHalfHoursPassed / 2);
                currentMinute = (totalHalfHoursPassed % 2) * 30;


                UpdateTimeUI();
            }
            else
            {
                yield return null;
            }
            
        }

        Debug.Log("게임이 종료되었습니다.");
    }

    void UpdateTimeUI()
    {

        timeText.text = $"{currentHour:D2}:{currentMinute:D2}";
        
    }
    void UpdateSkyBox()
    {

        
        if(currentHour == 20)
        {
            npcSpawner.maxNPCCount = 0;
            StartCoroutine(Waitfor30Seconds());
            if (newSkybox != null)
            {
                RenderSettings.skybox = newSkybox;
                Debug.Log("스카이박스가 변경되었습니다.");
            }
            
            
        }
        
    }
    IEnumerator Waitfor30Seconds()
    {
        yield return new WaitForSeconds(30f);
        timeText.text = $"{20:D2}:{30:D2}";
        if(!hascalled)
        {
            
            npcSpawner.SpawnEventNPC();
            specialNPC = FindObjectOfType<SpecialNPCOrder>();
            hascalled = true;
        }
    }

    public void Event01_OnButtonClick()
    {
        clickCount++;

        if (clickCount <= evnet01_texts.Length)
        {
            EventNPC_Text.text = evnet01_texts[clickCount - 1];
        }
        else if (clickCount == evnet01_texts.Length + 1)
        {
            NPCtalkUI.gameObject.SetActive(false);
            playerItem.isShoping = false;
            Destroy(EventNPC_Text);
            specialNPC.OredrStart();
            EventBegin = false;
        }
        
    }
    public void NPCFeedBack(bool success)
    {
        if(success == true)
        {
            NPC_FeedBackText.text = eventfeedbackText[0];
        }
        else if(success == false)
        {
            NPC_FeedBackText.text = eventfeedbackText[1];
        }
    }
}
