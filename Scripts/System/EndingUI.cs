using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingUI : MonoBehaviour
{

    public Image fadeImage;
    public float fadeDuration = 1f;
    public string currentStage;
    public GameObject TalkBalloon;
    public TMP_Text EndingText;
    public string[] GoodEndingTexts;
    public string[] HiddenEndingTexts;
    public VideoPlayer GoodEndingPlayer;
    public VideoPlayer HiddenEndingPlayer;
    public RawImage GoodEndingImage;
    public RawImage HiddenEndingImage;

    private int currentIndex = 0;
    void Start()
    {
        currentStage = SceneManager.GetActiveScene().name;
        if(currentStage == "EndingStage01")
        {
            HiddenEnding();
        }
        else if(currentStage == "EndingStage02")
        {
            GoodEnding();
        }
        else if(currentStage == "BadEndingStage")
        {
            Invoke("Load0Scene", 4f);
        }
    }


    
    void GoodEnding()
    {
        StartCoroutine(ChangeText02());
    }
    void HiddenEnding()
    {
        StartCoroutine(ChangeText01());
    }
    void Load0Scene()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator ChangeText02()
    {
        while(currentIndex < GoodEndingTexts.Length)
        {
            EndingText.text = GoodEndingTexts[currentIndex];
            currentIndex ++;
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(1);
        EndingText.gameObject.SetActive(false);
        TalkBalloon.SetActive(false);
        
    }

    IEnumerator ChangeText01()
    {
        while(currentIndex < GoodEndingTexts.Length)
        {
            EndingText.text = GoodEndingTexts[currentIndex];
            currentIndex ++;
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(15);
        currentIndex = 0;
        HiddenTextPlay();
    }

    void HiddenTextPlay()
    {
        StartCoroutine(HiddenText());
    }
    IEnumerator HiddenText()
    {
        while(currentIndex < HiddenEndingTexts.Length)
        {
            EndingText.text = HiddenEndingTexts[currentIndex];
            currentIndex ++;
            yield return new WaitForSeconds(3);
        }
        yield return new WaitForSeconds(3);
        EndingText.gameObject.SetActive(false);
        TalkBalloon.SetActive(false);
    }

}
