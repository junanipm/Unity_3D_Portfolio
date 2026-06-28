using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickerMiniGame : MonoBehaviour
{

    [SerializeField]
    private Slider timeSlider;

    [SerializeField]
    private TMP_Text ClickText;
    
    [SerializeField]
    private Image gameImage;

    [SerializeField]
    private Sprite[] imageSprites;

    public float timeLimit= 10.0f;

    public Image[] SuccessFailUI;
    public PlayerItem playerItem;
    public int targetClicks; 
    public int maxClick = 30;

    
    public float timeLeft = 10f;
    public float currentTime;

    private int currentImageIndex = 0;

    void Start()
    {
        if (gameImage != null && imageSprites.Length > 0)
        {
            gameImage.sprite = imageSprites[0];
        }
    }

    void Update()
    {


        if(playerItem.isClickerGaming)
        {
            UpdateTimer();
            KeyCountCheck();
            if(playerItem.CoffeeIngredient <= 0 || playerItem.BreadIngredient <=0 || playerItem.CheeseIngredient <=0 )
            {
                playerItem.mainTofourth();
                
                Debug.Log ("님 재료 없슴");
                playerItem.LackImageActive();
            }
        }

        
        ClickText.text = Mathf.Max(0, targetClicks).ToString();
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        timeSlider.value = currentTime;

        if (currentTime <= 0)
        {
            EndGame(false);
        }
        else if(targetClicks <= 0)
        {
            EndGame(false);
        }
    }

    

    public void GameStart()
    {
        if(playerItem.isClickerGaming)
        {
            targetClicks = maxClick;
            currentTime = timeLeft;
            

            timeSlider.maxValue = timeLeft;
            timeSlider.value = timeLeft;

            if (gameImage != null && imageSprites.Length > 0)
            {
                gameImage.sprite = imageSprites[0];
                currentImageIndex = 0;
            }
            
        }
    }

    void KeyCountCheck()
    {
        if(currentTime > 0 && targetClicks > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                targetClicks --;
                UpdateImage();

                if (targetClicks == 0)
                {
                    EndGame(true);
                }
                
            }
        }
    }
    void UpdateImage()
    {
        if (imageSprites.Length > 0)
        {
            currentImageIndex = (currentImageIndex + 1) % imageSprites.Length;
            gameImage.sprite = imageSprites[currentImageIndex];
        }
    }

    void EndGame(bool isSuccess)
    {



        if (isSuccess)
        {
            playerItem.SpawnTiramisu();
            GameStart();
            Debug.Log("성공");
            StartCoroutine(ActiveSuccessImage(0));
            playerItem.SuccessSound();
        }
        else
        {
            Debug.Log("실패");
            GameStart();
            StartCoroutine(ActiveSuccessImage(1));
            playerItem.FailSound();
        }
    }

    IEnumerator ActiveSuccessImage(int i)
    {
        SuccessFailUI[i].gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        SuccessFailUI[i].gameObject.SetActive(false);
    }
}
