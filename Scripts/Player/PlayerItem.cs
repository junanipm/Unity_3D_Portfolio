using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class PlayerItem : MonoBehaviour
{   
    [SerializeField]
    public KeyCode KeyCodeInterAction = KeyCode.Mouse0;
    [SerializeField]
    private KeyCode KeyCodePlate = KeyCode.Tab;
    private static PlayerItem instance;
    GameObject nearObject;
    public GameObject ItemCoffee;
    public GameObject ItemCake;
    public GameObject ItemMelonSoda;
    public GameObject ItemSupple;
    public GameObject ItemTiramisu;
    
    public GameObject Plate;
    public bool firstOpen;
    [Header("UI는 여기 참조")]
        public GameObject KeyMiniGameUI;
        public GameObject ShoppingUI;
        public GameObject UpgradeUI;
        public GameObject HUD;
        public GameObject ClickerGameUI;
        public GameObject RecipeUI;
        public GameObject Settings;
        UIManager uIManager;
        public Image LackImage;
        public Image InterActionUI;
        public TMP_Text InterActionText;
    
    [Header("미니게임")]
    public GameObject miniSlider;

    public Camera mainCamera;
    public Camera secondCamera;
    public Camera thirdCamera;
    public Camera fourthCamera;
    public Camera UICamera;
    public CafeOpenObject cafeOpen;
    public bool isMiniGaming = false;
    public bool isCakeGaming = false;
    public bool isClickerGaming = false;
    public bool isShoping = false;
    public bool UIHovering = false;
    
    public bool isfirstCoffee = false;
    public bool isfirstToast = false;

    bool settingsOn = false;
    public ClickerMiniGame clicker;

    private bool[] isCakePositionOccupied = new bool[6];
    private Vector3[] cakeSpawnPoints = new Vector3[6];

    private bool[] isCoffeePositionOccupied = new bool[6];
    private Vector3[] CoffeeSpawnPoints = new Vector3[6];

    private bool[] isTiramisuPositionOccupied = new bool[6];
    private Vector3[] TiramisuSpawnPoints = new Vector3[6];
    [Header("NPC 및 제작관련")]
    public NPCTrigger nPCTrigger;
    FoodSelector foodSelector;
    public bool Event01Clear;
    public bool Event02Clear;
    public bool Event03Clear;
    [Header("사운드")]
        public AudioClip Delivery;
        public AudioClip Success;
        public AudioClip Fail;
        public AudioClip InterAct;
    
    void Awake()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        Resources.UnloadUnusedAssets();
        /*
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        isShoping = false;
        instance = this;
       
        */
        uIManager = FindObjectOfType<UIManager>();
        
    }
        
    
    void Start()
    {
        mainCamera.enabled = true;
        secondCamera.enabled = false;
        thirdCamera.enabled = false;
        UICamera.enabled = false;

        cakeSpawnPoints[0] = new Vector3(8.5f, 7.4f, 40f);
        cakeSpawnPoints[1] = new Vector3(9.5f, 7.4f, 40f);
        cakeSpawnPoints[2] = new Vector3(8.5f, 7.4f, 39.4f);
        cakeSpawnPoints[3] = new Vector3(9.5f, 7.4f, 39.4f);
        cakeSpawnPoints[4] = new Vector3(8.5f, 7.4f, 38.8f);
        cakeSpawnPoints[5] = new Vector3(9.5f, 7.4f, 38.8f);

        for (int i = 0; i < isCakePositionOccupied.Length; i++)
        {
            isCakePositionOccupied[i] = false;
        }


        CoffeeSpawnPoints[0] = new Vector3(15.4f, 7.5f, 32.8f);
        CoffeeSpawnPoints[1] = new Vector3(15.4f, 7.5f, 32f);
        CoffeeSpawnPoints[2] = new Vector3(15.4f, 7.5f, 31.2f);
        CoffeeSpawnPoints[3] = new Vector3(14.2f, 7.5f, 32.8f);
        CoffeeSpawnPoints[4] = new Vector3(14.2f, 7.5f, 32f);
        CoffeeSpawnPoints[5] = new Vector3(14.2f, 7.5f, 31.2f);

        for (int i = 0; i < isCoffeePositionOccupied.Length; i++)
        {
            isCoffeePositionOccupied[i] = false;
        }

        TiramisuSpawnPoints[0] = new Vector3(15.4f, 7.5f, 24.88f);
        TiramisuSpawnPoints[1] = new Vector3(15.4f, 7.5f, 24.08f);
        TiramisuSpawnPoints[2] = new Vector3(15.4f, 7.5f, 23.28f);
        TiramisuSpawnPoints[3] = new Vector3(14.2f, 7.5f, 24.88f);
        TiramisuSpawnPoints[4] = new Vector3(14.2f, 7.5f, 24.08f);
        TiramisuSpawnPoints[5] = new Vector3(14.2f, 7.5f, 23.28f);

        for (int i = 0; i < isTiramisuPositionOccupied.Length; i++)
        {
            isTiramisuPositionOccupied[i] = false;
        }
        foodSelector = GetComponent<FoodSelector>();
        

        DataCarrier dataCarrier = DataCarrier.GetInstance();
        if (dataCarrier != null)
        {
            Event01Clear = dataCarrier.D_Event01;
            Event02Clear = dataCarrier.D_Event02;
            Event03Clear = dataCarrier.D_Event03;

            CakeCount = dataCarrier.CakeCount;
            TiramisuCount = dataCarrier.TiramisuCount;
            CoffeeCount = dataCarrier.CoffeeCount;
            CoffeeIngredient = dataCarrier.CoffeeIngredient;
            BreadIngredient = dataCarrier.BreadIngredient;
            MilkIngredient = dataCarrier.MilkIngredient;
            EggIngredient = dataCarrier.EggIngredient;
            IceCreamIngredient = dataCarrier.IceCreamIngredient;
            MelonIngredient = dataCarrier.MelonIngredient;
            CheeseIngredient = dataCarrier.CheeseIngredient;
            CurrentMoney = dataCarrier.CurrentMoney;
        }
        firstOpen = false;
        isEInputed = false;
    }
    
    [Header("플레이어 스텟")]
    public int ItemMax = 5;
    public int CakeCount = 0;
    public int TiramisuCount = 0;

    public int CoffeeCount = 0;
    public int CoffeeIngredient = 0;
    public int BreadIngredient = 0;
    public int MilkIngredient = 0;
    public int EggIngredient = 0;
    public int IceCreamIngredient = 0;
    public int MelonIngredient = 0;
    public int CheeseIngredient = 0;


    public int CurrentMoney = 0;
    public bool isPlated = false;
    public bool isEInputed = false;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCodeInterAction) && nearObject != null)
        {
            if (nearObject.tag == "Machine" && nearObject.name == "PW_stove")
            {
                bool isActive = foodSelector.foodUIPanel.gameObject.activeSelf;
                if(!isActive)
                {
                    
                    Debug.Log("케잌 만들기");
                    mainTothird();
                    SoundManager.instance.SFXPlay("Interact", InterAct);
                }
                else if(isActive)
                {
                    QuitFoodSelect();
                    
                }
            }
            else if (nearObject.tag == "Machine" && nearObject.name == "PW_espressomachine")
            {
                bool isActive = foodSelector.drinkUIPanel.gameObject.activeSelf;
                if(!isActive)
                {
                    
                    Debug.Log("커피 만들기");
                    mainTosecond();
                    SoundManager.instance.SFXPlay("Interact", InterAct);
                }
                else if(isActive)
                {
                    QuitDrinkSelect();
                }
                
            }
            else if (nearObject.tag == "Sign")
            {
                /*
                Debug.Log("카페열음");
                cafeOpen.test = !cafeOpen.test;
                nPCTrigger.NPCComeToCafe = !nPCTrigger.NPCComeToCafe;
                */
                NPCTrigger trigger = FindObjectOfType<NPCTrigger>();
                if (trigger != null)
                {

                    bool newState = !trigger.NPCComeToCafe;
                    trigger.SetNPCComeToCafe(newState);
                    
                    cafeOpen.test = !cafeOpen.test;

                    Debug.Log($"카페 상태 변경됨: {newState}");
                    SoundManager.instance.SFXPlay("Interact", InterAct);
                    if(firstOpen == false && uIManager != null)
                    {
                        uIManager.StartGameTime();
                        firstOpen = true;
                    }
                }
                
            }
            else if (nearObject.tag == "Machine" && nearObject.name == "PosColl")
            {
                SoundManager.instance.SFXPlay("Interact", InterAct);
                mainToUI();
                
            }
            else if (nearObject.tag == "Machine" && nearObject.name == "ClickerPlace")
            {
                mainTofourth();
            }
            else if (nearObject.tag == "Machine" && nearObject.name == "Upgrade")
            {
                Debug.Log(":sss");
                SoundManager.instance.SFXPlay("Interact", InterAct);
                mainToUpgrade();
            }
            if(isEInputed)
            {
                if(nearObject.tag == "Machine" && nearObject.name == "PW_espressomachine")
                {
                    QuitDrinkSelect();
                    isEInputed = false;
                }
                else if(nearObject.tag == "Machine" && nearObject.name == "PW_stove")
                {
                    QuitFoodSelect();
                    isEInputed = false;
                }
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!uIManager.isPaused)
            {
                isShoping = !isShoping;
                bool isActive = RecipeUI.gameObject.activeSelf;
                RecipeUI.gameObject.SetActive(!isActive);
                UIHovering = !UIHovering;
            }
            
        }
        if(UIHovering)
        {
            if(Input.GetKeyDown(KeyCodeInterAction))
            {
                isShoping = !isShoping;

                RecipeUI.gameObject.SetActive(false);
                UIHovering = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isShoping = !isShoping;
            bool isActive = Settings.gameObject.activeSelf;
            Settings.gameObject.SetActive(!isActive);
            uIManager.isPaused = !uIManager.isPaused;
            
        }
        
        
        

        PlateActivate();
    }
    /*
    private void InterAction()
    {
        if(Input.GetKeyDown(KeyCodeInterAction) && nearObject != null)
        {
            
            if(nearObject.tag == "Machine" && nearObject.name == "PW_stove" && CakeCount <= ItemMax)
            {
                CakeCountcalculate();
                Instantiate(ItemCake, new Vector3 (CakeLocationX,0.2f,CakeLocationZ), Quaternion.identity);
                CakeCount +=1;
                Debug.Log(CakeCount);
            }

            else if(nearObject.tag == "Machine" && nearObject.name == "PW_espressomachine" && CoffeeCount <= ItemMax)
            {
                CoffeeCountcalculate();
                Instantiate(ItemCoffee, new Vector3 (CoffeeLocationX,0.2f,CoffeeLocationZ), Quaternion.identity);
                CoffeeCount +=1;
                Debug.Log(CoffeeCount);
            }
        }
    }
    */
    public void SpawnCake()
    {


        for (int i = 0; i < cakeSpawnPoints.Length; i++)
        {
            if (!isCakePositionOccupied[i] && CakeCount <= ItemMax)
            {
                if(foodSelector.DrinkValue == "Coffee" && CoffeeIngredient > 0 && MilkIngredient >0)
                {

                    GameObject coffee = Instantiate(ItemCake, cakeSpawnPoints[i], Quaternion.identity);


                    isCakePositionOccupied[i] = true;


                    CakeCount++;
                    CoffeeIngredient--;
                    MilkIngredient --;
                    isfirstCoffee = true;

                    coffee.GetComponent<DestroyItem>().OnItemDestroyed += () => HandleCakeDestroyed(i);

                    Debug.Log("케이크 소환됨. 현재 개수: " + CakeCount);
                    break;
                }
                else if(foodSelector.DrinkValue == "MelonSoda" && MelonIngredient >0 && IceCreamIngredient >0)
                {
                    GameObject melonSoda = Instantiate(ItemMelonSoda, cakeSpawnPoints[i], Quaternion.identity);


                    isCakePositionOccupied[i] = true;


                    CakeCount++;
                    MelonIngredient --;
                    IceCreamIngredient --;


                    melonSoda.GetComponent<DestroyItem>().OnItemDestroyed += () => HandleCakeDestroyed(i);

                    Debug.Log("케이크 소환됨. 현재 개수: " + CakeCount);
                    break;
                }
                else
                {
                    
                }
            }
        }
    }

    void HandleCakeDestroyed(int index)
    {

        isCakePositionOccupied[index] = false;
        CakeCount--;
        Debug.Log("케이크 파괴됨. 현재 개수: " + CakeCount);
    }

    public void SpawnCoffee()
    {

        for (int i = 0; i < cakeSpawnPoints.Length; i++)
        {
            if (!isCoffeePositionOccupied[i] && CoffeeCount <= ItemMax)
            {
                if(foodSelector.FoodValue == "Toast" && BreadIngredient > 0 && EggIngredient >0)
                {

                    GameObject coffee = Instantiate(ItemCoffee, CoffeeSpawnPoints[i], Quaternion.identity);


                    isCoffeePositionOccupied[i] = true;


                    CoffeeCount++;
                    isfirstToast = true;


                    coffee.GetComponent<DestroyItem>().OnItemDestroyed += () => HandleCoffeeDestroyed(i);

                    Debug.Log("케이크 소환됨. 현재 개수: " + CoffeeCount);
                    break;
                }
                else if(foodSelector.FoodValue == "Supple" && IceCreamIngredient > 0 && EggIngredient >0)
                {

                    GameObject supple = Instantiate(ItemSupple, CoffeeSpawnPoints[i], Quaternion.identity);


                    isCoffeePositionOccupied[i] = true;


                    CoffeeCount++;
                    


                    supple.GetComponent<DestroyItem>().OnItemDestroyed += () => HandleCoffeeDestroyed(i);

                    Debug.Log("케이크 소환됨. 현재 개수: " + CoffeeCount);
                    break;
                }

            }
        }
    }

    void HandleCoffeeDestroyed(int index)
    {

        isCoffeePositionOccupied[index] = false;
        CoffeeCount--;
        Debug.Log("케이크 파괴됨. 현재 개수: " + CoffeeCount);
    }

    public void SpawnTiramisu()
    {

        for (int i = 0; i < TiramisuSpawnPoints.Length; i++)
        {
            if (!isTiramisuPositionOccupied[i] && TiramisuCount <= ItemMax && BreadIngredient > 0 && CheeseIngredient >0 && CoffeeIngredient >0)
            {

                GameObject Tiramisu = Instantiate(ItemTiramisu, TiramisuSpawnPoints[i], Quaternion.identity);


                isTiramisuPositionOccupied[i] = true;


                TiramisuCount++;
                BreadIngredient--;
                CheeseIngredient--;
                CoffeeIngredient--;
                


                Tiramisu.GetComponent<DestroyItem>().OnItemDestroyed += () => HandleTiramisuDestroyed(i);

                Debug.Log("티라미수 소환됨. 현재 개수: " + TiramisuCount);
                break;
            }
        }
    }

    void HandleTiramisuDestroyed(int index)
    {

        isTiramisuPositionOccupied[index] = false;
        TiramisuCount--;
        Debug.Log("티라미수 파괴됨. 현재 개수: " + TiramisuCount);
    }


    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Machine" || other.tag == "Sign")
        {
            nearObject = other.gameObject;
            InterActionUI.gameObject.SetActive(true);
            InterActionText.gameObject.SetActive(true);
        }

    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Machine" || other.tag == "Sign")
        {
            InterActionUI.gameObject.SetActive(false);
            InterActionText.gameObject.SetActive(false);
        
            nearObject = null;
        }
    }

    

    private void PlateActivate()
    {
        if(Input.GetKeyDown(KeyCodePlate))
        {
            if(isPlated == false)
            {
                Plate.gameObject.SetActive(true);
                isPlated = true;
            }
            else if(isPlated == true)
            {
                Plate.gameObject.SetActive(false);
                isPlated = false;
            }
        }
    }

    public void mainTosecond()
    {
        if (isCakeGaming)
        {

            isCakeGaming = false;
            mainCamera.enabled = true;
            secondCamera.enabled = false;
            ToggleKeyGame();
            
        }
        else if ((CoffeeIngredient > 0 && MilkIngredient >0) || (MelonIngredient >0 && IceCreamIngredient >0))
        {
        
            isShoping = true;
            mainCamera.enabled = false;
            secondCamera.enabled = true;

            foodSelector.DrinkUIActive();
            SoundManager.instance.SFXPlay("Interact", InterAct);
        }
        else
        {
        Debug.Log("커피 재료가 부족합니다.");
            LackTextActive();
            Invoke("LackTextUnActive", 3f);
        }

        
    }
    public void mainTothird()
    {
        if (isMiniGaming)
        {


            isMiniGaming = false;
            mainCamera.enabled = true;
            thirdCamera.enabled = false;
            ToggleSlider();
        }
        else if (( BreadIngredient > 0 && EggIngredient > 0) || (EggIngredient >0 && IceCreamIngredient >0))
        {

            isShoping = true;
            mainCamera.enabled = false;
            thirdCamera.enabled = true;
            foodSelector.FoodUIActive();
            SoundManager.instance.SFXPlay("Interact", InterAct);
        }
        else
        {
            LackTextActive();
            Invoke("LackTextUnActive", 3f);
            Debug.Log("케잌 재료가 부족합니다.");
        }

    }

    public void mainTofourth()
    {
        if (isClickerGaming)
        {

            isClickerGaming = false;
            mainCamera.enabled = true;
            fourthCamera.enabled = false;
            ToggleClicker();
        }
        else if (CoffeeIngredient > 0 && CheeseIngredient > 0 && BreadIngredient >0)
        {


            isClickerGaming = true;

            clicker.targetClicks = clicker.maxClick;
            clicker.currentTime = clicker.timeLeft;
            clicker.GameStart();
            mainCamera.enabled = false;
            fourthCamera.enabled = true;
            ToggleClicker();
            SoundManager.instance.SFXPlay("Interact", InterAct);
        }
        else
        {
            LackTextActive();
            Invoke("LackTextUnActive", 3f);
        }

    }

    public void mainToUI()
    {

        isShoping = !isShoping;
        mainCamera.enabled = !mainCamera.enabled;
        UICamera.enabled = !UICamera.enabled;
        ToggleShopingUI();
    }
    void mainToUpgrade()
    {
        isShoping = !isShoping;
        
        ToggleUpgradeUI();
    }

    public void ToggleSlider()
    {
        if (miniSlider != null)
        {
            
            bool isActive = miniSlider.gameObject.activeSelf;
            miniSlider.gameObject.SetActive(!isActive);
        }
    }
    public void ToggleKeyGame()
    {
        if (KeyMiniGameUI != null)
        {
            bool isActive = KeyMiniGameUI.gameObject.activeSelf;
            KeyMiniGameUI.gameObject.SetActive(!isActive);
        }
    }
    public void ToggleClicker()
    {
        if (ClickerGameUI != null)
        {
            bool isActive = ClickerGameUI.gameObject.activeSelf;
            ClickerGameUI.gameObject.SetActive(!isActive);
        }
    }

    public void ToggleShopingUI()
    {
        if(ShoppingUI != null)
        {
            bool isActive = ShoppingUI.gameObject.activeSelf;
            ShoppingUI.gameObject.SetActive(!isActive);
        }
        if(HUD != null)
        {
            bool isActive = HUD.gameObject.activeSelf;
            HUD.gameObject.SetActive(!isActive);
        }
    }
    public void ToggleUpgradeUI()
    {
        if(UpgradeUI != null)
        {
            bool isActive = UpgradeUI.gameObject.activeSelf;
            UpgradeUI.gameObject.SetActive(!isActive);
        }
        if(HUD != null)
        {
            bool isActive = HUD.gameObject.activeSelf;
            HUD.gameObject.SetActive(!isActive);
        }
    }
    public void ChoosingFood()
    {
        if(nearObject != null)
        {
            if (nearObject.tag == "Machine" && nearObject.name == "PW_stove")
            {

            }
            else if (nearObject.tag == "Machine" && nearObject.name == "PW_espressomachine")
            {
                
            }

        }
    }
    public void QuitDrinkSelect()
    {
        isShoping = false;
        mainCamera.enabled = true;
        secondCamera.enabled = false;
        foodSelector.drinkUIPanel.SetActive(false);
    }
    public void QuitFoodSelect()
    {
        isShoping = false;
        mainCamera.enabled = true;
        thirdCamera.enabled = false;
        foodSelector.foodUIPanel.SetActive(false);
    }

    public void LackTextActive()
    {
        LackImage.gameObject.SetActive(true);
    }
    public void LackTextUnActive()
    {
        LackImage.gameObject.SetActive(false);
    }

    IEnumerator ActiveLackImage()
    {
        LackImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        LackImage.gameObject.SetActive(false);
    }

    public void LackImageActive()
    {
        StartCoroutine(ActiveLackImage());
    }
    public void SuccessSound()
    {
        SoundManager.instance.SFXPlay("Success", Success);
    }
    public void FailSound()
    {
        SoundManager.instance.SFXPlay("Fail", Fail);
    }
}
