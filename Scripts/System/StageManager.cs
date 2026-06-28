using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public PlayerItem playerItem;
    public string mainScreen = "MainPage";
    public string tutorial = "Tutorial";
    public string stage01 = "Stage01";
    public string stage02 = "Stage02";
    public string stage03 = "Stage03";
    public string currentStage = "Stage03";
    UIManager uiManager;
    public Image fadeImage;
    public float fadeDuration = 1f;
    public AudioClip SceneLoad;
    void Start()
    {
        UpdateCurrentStage();
        uiManager = FindObjectOfType<UIManager>();

        
    }
    void Update() 
    {
        GameStart();    
    }

    public void UpdateCurrentStage()
    {

        string activeSceneName = SceneManager.GetActiveScene().name;


        if (activeSceneName == stage01)
        {
            currentStage = stage01;
        }
        else if (activeSceneName == stage02)
        {
            currentStage = stage02;
        }
        else if (activeSceneName == stage03)
        {
            currentStage = stage03;
        }
        else if (activeSceneName == mainScreen)
        {
            currentStage = mainScreen;
        }

        else
        {
            currentStage = "Unknown Stage";
        }

        Debug.Log("Current Stage: " + currentStage);
    }
    public void LoadScene00()
    {
        SceneManager.LoadScene("MainPage");
    }
    public void LoadStage01()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage01");
        SoundManager.instance.SFXPlay("SceneLoad", SceneLoad);
    }
    public void LoadStage02()
    {
        uiManager.elapsedTime = 0;
        
        DataCarrier dataCarrier;
        dataCarrier = FindObjectOfType<DataCarrier>();
        if (dataCarrier == null)
        {
            GameObject dataCarrierObject = new GameObject("DataCarrier");
            dataCarrier = dataCarrierObject.AddComponent<DataCarrier>();
        }
        dataCarrier.D_Event01 = playerItem.Event01Clear;
        dataCarrier.D_Event02 = playerItem.Event02Clear;
        dataCarrier.D_Event03 = playerItem.Event03Clear;
        dataCarrier.CakeCount = playerItem.CakeCount;
        dataCarrier.TiramisuCount = playerItem.TiramisuCount;
        dataCarrier.CoffeeCount = playerItem.CoffeeCount;
        dataCarrier.CoffeeIngredient = playerItem.CoffeeIngredient;
        dataCarrier.BreadIngredient = playerItem.BreadIngredient;
        dataCarrier.MilkIngredient = playerItem.MilkIngredient;
        dataCarrier.EggIngredient = playerItem.EggIngredient;
        dataCarrier.IceCreamIngredient = playerItem.IceCreamIngredient;
        dataCarrier.MelonIngredient = playerItem.MelonIngredient;
        dataCarrier.CheeseIngredient = playerItem.CheeseIngredient;
        dataCarrier.CurrentMoney = playerItem.CurrentMoney;
        SoundManager.instance.SFXPlay("SceneLoad", SceneLoad);
        SceneManager.LoadScene("Stage02");
    }
    public void LoadStage03()
    {
        DataCarrier dataCarrier = FindObjectOfType<DataCarrier>();
        if (dataCarrier == null)
        {
            GameObject dataCarrierObject = new GameObject("DataCarrier");
            dataCarrier = dataCarrierObject.AddComponent<DataCarrier>();
        }
        dataCarrier.D_Event01 = playerItem.Event01Clear;
        dataCarrier.D_Event02 = playerItem.Event02Clear;
        dataCarrier.D_Event03 = playerItem.Event03Clear;
        dataCarrier.CakeCount = playerItem.CakeCount;
        dataCarrier.TiramisuCount = playerItem.TiramisuCount;
        dataCarrier.CoffeeCount = playerItem.CoffeeCount;
        dataCarrier.CoffeeIngredient = playerItem.CoffeeIngredient;
        dataCarrier.BreadIngredient = playerItem.BreadIngredient;
        dataCarrier.MilkIngredient = playerItem.MilkIngredient;
        dataCarrier.EggIngredient = playerItem.EggIngredient;
        dataCarrier.IceCreamIngredient = playerItem.IceCreamIngredient;
        dataCarrier.MelonIngredient = playerItem.MelonIngredient;
        dataCarrier.CheeseIngredient = playerItem.CheeseIngredient;
        dataCarrier.CurrentMoney = playerItem.CurrentMoney;
        SoundManager.instance.SFXPlay("SceneLoad", SceneLoad);
        SceneManager.LoadScene("Stage03");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene {scene.name} loaded.");
        
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        if (uiManager != null)
        {
            uiManager.UpdateAllUI();
            Debug.Log("UI업데이트 했는디");
        }
        else
        {
            Debug.Log("UIM 미싱");
        }
    }

    void GameStart()
    {
        if(currentStage == mainScreen)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                FadeOut();
                Invoke("LoadStage01",fadeDuration);
            }
        }
    }
    void FadeIn()
    {
        StartCoroutine(FadeTo(0));
    }
    void FadeOut()
    {
        StartCoroutine(FadeTo(1));
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }


        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

