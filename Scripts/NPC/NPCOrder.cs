using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCOrder : MonoBehaviour
{

    public string[] FoodList = new string [5];
    public string FoodOrder;
    public GameObject foodOrderUIPrefab;
    public Sprite[] foodImages;

    private GameObject foodOrderUI;
    private Transform mainCamera;
    public Camera mainCameraOverride;

    private StageManager stageManager;
    private NPCTrigger npcTrigger;
    public Vector3 UIOffset = new Vector3(0, 2, 0);
    
    public GameObject smokeEffect;

    public Material smile;
    public float delay = 3.0f;
    public float npcMaxTime;
    private float currentTime;
    public Slider timerSlider;
    private bool isTimerRuning = false;
    /*
    [Header("이벤트 NPC UI")]
    public GameObject Event01;
    public GameObject Event02;
    public GameObject Event03;
    */

    public string childObjectName = "Cube";
    private void Awake()
    {

        if (mainCameraOverride != null)
        {
            mainCamera = mainCameraOverride.transform;
        }
        else
        {
            var mainCameraObject = Camera.main;
            if (mainCameraObject == null)
            {
                Debug.LogWarning("Main Camera가 없으므로 새 카메라를 생성합니다.");



            }
            else
            {
                mainCamera = mainCameraObject.transform;
            }
        }
    }
    

    private void Start()
    {
        FoodList = new string[] { "Coffee", "Toast", "MelonSoda", "Supple", "Tiramisu" };
        GameObject NPCTriggerObject = GameObject.Find("NPCTrigger");
        if(NPCTriggerObject != null)
        {
            npcTrigger = NPCTriggerObject.GetComponent<NPCTrigger>();
        }
    }

    public void Initialize(StageManager manager)
    {
        stageManager = manager;
        FoodSetting();
    }

    public virtual void FoodSetting()
    {
        stageManager = FindObjectOfType<StageManager>();

        if (stageManager != null)
        {
            /*
            if(gameObject.name == "EventNPC01" ||gameObject.name == "EventNPC02" || gameObject.name == "EventNPC03" )
            {
                Event01.SetActive(true);
            }
            else
            {
                SetFoodOrder(stageManager.currentStage);
                CreateFoodOrderUI();
            }
            */
            SetFoodOrder(stageManager.currentStage);
            CreateFoodOrderUI();
                
        }
        else
        {
            Debug.LogError("StageManager를 찾을 수 없습니다.");
        }
    }
    private void Update()
    {
        if (foodOrderUI != null && mainCamera != null)
        {

            Vector3 direction = mainCamera.position - foodOrderUI.transform.position;


            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                foodOrderUI.transform.rotation = targetRotation;
            }
        }
        else
        {
            if (foodOrderUI == null)
            {

            }
            if (mainCamera == null)
            {

            }
        }
    }

    void SetFoodOrder(string currentStage)
    {

        if (currentStage == "Stage01")
        {
            int randomIndex = Random.Range(0, 2);
            FoodOrder = FoodList[randomIndex];
        }

        else if (currentStage == "Stage02")
        {
            int randomIndex = Random.Range(0, 4);
            FoodOrder = FoodList[randomIndex];
        }

        else if (currentStage == "Stage03")
        {
            int randomIndex = Random.Range(0, FoodList.Length);
            FoodOrder = FoodList[randomIndex];
        }
        else
        {
            FoodOrder = "Unknown Stage";
        }

        Debug.Log($"현재 스테이지: {currentStage}, 선택된 음식: {FoodOrder}");
    }

    protected virtual void CreateFoodOrderUI()
    {
        if (foodOrderUI != null)
        {
            Destroy(foodOrderUI);
        }

        if (foodOrderUIPrefab != null)
        {

            foodOrderUI = Instantiate(foodOrderUIPrefab, transform);

            RectTransform rectTransform = foodOrderUI.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.localPosition = UIOffset;
            }
            else
            {
                Debug.LogError("RectTransform을 찾을 수 없습니다.");
            }

            Image imageComponent = foodOrderUI.GetComponentInChildren<Image>();
            if (imageComponent != null && foodImages.Length > 0)
            {
                int index = System.Array.IndexOf(FoodList, FoodOrder);
                if (index >= 0 && index < foodImages.Length)
                {
                    imageComponent.sprite = foodImages[index];
                }
                else
                {
                    Debug.LogError($"FoodOrder '{FoodOrder}'에 매핑된 이미지가 없습니다.");
                }
            }
            else
            {
                Debug.LogError("Image 컴포넌트를 찾을 수 없습니다.");
            }

            timerSlider = foodOrderUI.GetComponentInChildren<Slider>();
            if(timerSlider != null)
            {
                timerSlider.minValue = 0;
                timerSlider.maxValue = npcMaxTime;
                timerSlider.value = npcMaxTime;
                StartTimer();
            }
        }
        else
        {
            Debug.LogError("foodOrderUIPrefab이 null입니다.");
        }
    }


    public void NPCSUCCESS()
    {
        isTimerRuning = false;
        Destroy(foodOrderUI);

        npcTrigger.currentTouchedNPCs --;
        Changematerial();
        StartCoroutine(Delay());
        
        Destroy(gameObject, 3.5f);
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        PlayEffect();
        
    }

    void Changematerial()
    {
        Transform childTransform = transform.Find(childObjectName);

        if (childTransform != null)
        {

            Renderer childRenderer = childTransform.GetComponent<Renderer>();

            if (childRenderer != null)
            {

                childRenderer.material = smile;
                Debug.Log($"{childObjectName}의 Material이 변경되었습니다!");
            }
            else
            {
                Debug.LogWarning($"'{childObjectName}'에 Renderer가 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning($"'{childObjectName}'라는 이름의 자식 오브젝트를 찾을 수 없습니다.");
        }

    }

    
    public void PlayEffect()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        GameObject PlayEffect = Instantiate(smokeEffect, position, rotation);

        ParticleSystem ps = PlayEffect.GetComponent<ParticleSystem>();

        if(ps != null)
        {
            Destroy(PlayEffect, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(PlayEffect, 5f);
        }
    }

    private void StartTimer()
    {
        if(isTimerRuning)
            return;
        isTimerRuning = true;
        currentTime = npcMaxTime;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if(timerSlider != null)
            {
                timerSlider.value = currentTime;
            }
            yield return null;
        }
        TimerEnd();
    }

    private void TimerEnd()
    {
        isTimerRuning = false;
        npcTrigger.currentTouchedNPCs --;
        Destroy(foodOrderUI);
        Destroy(gameObject);

    }

    void OnDestroy()
    {
        StopCoroutine(UpdateTimer());
    }
}
