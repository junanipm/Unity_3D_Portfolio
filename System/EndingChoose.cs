using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndingChoose : MonoBehaviour
{

    public VideoPlayer GoodEndingPlayer;
    public VideoPlayer HiddenEndingPlayer;
    public RawImage GoodEndingImage;
    public RawImage HiddenEndingImage;
    public GameObject GoodDoor;
    public GameObject HiddenDoor;

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GoodEnding")
        {
            Debug.Log("굿엔딩");
            GoodEndingPlayer.gameObject.SetActive(true);
            GoodEndingImage.gameObject.SetActive(true);
            HiddenDoor.SetActive(false);
            WaitFor31Sec();
        }
        else if(other.tag == "HiddenEnding")
        {
            HiddenEndingPlayer.gameObject.SetActive(true);
            HiddenEndingImage.gameObject.SetActive(true);
            GoodDoor.SetActive(false);
            WaitFor49Sec();
        }
    }
    IEnumerator WaitFor49Sec()
    {
        yield return new WaitForSeconds(49.5f);
        SceneManager.LoadScene(0);
    }
    IEnumerator WaitFor31Sec()
    {
        yield return new WaitForSeconds(31.5f);
        SceneManager.LoadScene(0);
    }
    
}
