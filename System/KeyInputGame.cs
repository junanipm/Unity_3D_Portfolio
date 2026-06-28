using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class KeyInputGame : MonoBehaviour
{
    
    public Slider timeSlider;
    
    public float timeLeft = 5f;

    private float currentTime;
    private List<KeyCode> keySequence;
    private int currentInputIndex;
    public PlayerItem playerItem;

    public List<Image> keyImages;
    public Sprite upArrowSprite;
    public Sprite downArrowSprite;
    public Sprite leftArrowSprite;
    public Sprite rightArrowSprite; 

    public Color highlightColor = Color.green;
    public Color normalColor = Color.white;
    public Color pressedColor = Color.black;
    public FoodSelector foodSelector;

    public Image feedbackImage;
    public List<Sprite> feedbackSprites;

    private KeyCode[] possibleKeys = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow };

    public Image[] SuccessFailUI;

    void Start()
    {
        
        InitializeGame();
    }

    void InitializeGame()
    {
        if (playerItem.isCakeGaming == true)
        {
            currentTime = timeLeft;
            currentInputIndex = 0;
            GenerateRandomKeySequence();

            timeSlider.maxValue = timeLeft;
            timeSlider.value = timeLeft;


            ResetKeyImages();
            ResetFeedbackImage();


            UpdateFeedbackImage(0);


            UpdateKeyImages();
        }
    }

    void Update()
    {
        if (playerItem.isCakeGaming == true)
        {
            
            if ((foodSelector.DrinkValue == "Coffee" && (playerItem.CoffeeIngredient <= 0 || playerItem.MilkIngredient <= 0)) || (foodSelector.DrinkValue == "MelonSoda" && (playerItem.IceCreamIngredient <= 0 || playerItem.MelonIngredient <= 0)))
            {
                Debug.Log("재료부족");
                playerItem.mainTosecond();
            }
            else
            {
                UpdateTimer();
            }
            if (Input.anyKeyDown)
            {
                CheckKeyInput();
            }
            
            
        }
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        timeSlider.value = currentTime;

        if (currentTime <= 0)
        {
            InitializeGame();
        }
    }

    void GenerateRandomKeySequence()
    {
        keySequence = new List<KeyCode>();

        for (int i = 0; i < 4; i++)
        {
            KeyCode randomKey = possibleKeys[Random.Range(0, possibleKeys.Length)];
            keySequence.Add(randomKey);
        }

        Debug.Log("입력할 키 시퀀스: " + string.Join(",", keySequence));
    }

    void CheckKeyInput()
    {

        if (Input.GetKeyDown(playerItem.KeyCodeInterAction))
        {
            Debug.Log("E키는 무시됩니다.");
            return;
        }


        if (Input.GetKeyDown(keySequence[currentInputIndex]))
        {
            keyImages[currentInputIndex].color = pressedColor;
            currentInputIndex++;


            if (currentInputIndex >= keySequence.Count)
            {
                Debug.Log("성공!");
                playerItem.SpawnCake();
                playerItem.SuccessSound();
                StartCoroutine(ActiveSuccessImage(0));
                currentInputIndex = 0;
                UpdateFeedbackImage(0);
                InitializeGame();
            }
            else
            {
                UpdateFeedbackImage(currentInputIndex);
                UpdateKeyImages();
            }
        }
        else if (Input.anyKeyDown)
        {
            Debug.Log("잘못된 키 입력! 실패.");
            playerItem.FailSound();
            StartCoroutine(ActiveSuccessImage(1));
            InitializeGame();
        }
    }

    void UpdateKeyImages()
    {
        for (int i = 0; i < keySequence.Count; i++)
        {

            keyImages[i].sprite = GetKeySprite(keySequence[i]);


            if (i == currentInputIndex)
            {
                keyImages[i].color = highlightColor;
            }
            else if (i < currentInputIndex)
            {
                keyImages[i].color = pressedColor;
            }
            else
            {
                keyImages[i].color = normalColor;
            }
        }
    }


    void ResetFeedbackImage()
    {
        feedbackImage.sprite = feedbackSprites[0];
    }


    void UpdateFeedbackImage(int index)
    {
        if (index >= 0 && index < feedbackSprites.Count)
        {
            feedbackImage.sprite = feedbackSprites[index];
            Debug.Log("피드백 이미지가 설정되었습니다: " + feedbackSprites[index].name);
        }
        else
        {
            Debug.LogWarning("잘못된 인덱스이거나 스프라이트가 없음: " + index);
        }
    }


    void ResetKeyImages()
    {
        for (int i = 0; i < keyImages.Count; i++)
        {
            keyImages[i].sprite = null;
            keyImages[i].color = normalColor;
        }
    }


    Sprite GetKeySprite(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.UpArrow: return upArrowSprite;
            case KeyCode.DownArrow: return downArrowSprite;
            case KeyCode.LeftArrow: return leftArrowSprite;
            case KeyCode.RightArrow: return rightArrowSprite;
            default: return null;
        }
    }
    IEnumerator ActiveSuccessImage(int i)
    {
        SuccessFailUI[i].gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        SuccessFailUI[i].gameObject.SetActive(false);
    }
}
