using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public PlayerItem playerItem;

    public bool Stage01Clear = false;
    public bool Stage01Fail = false;
    public GameObject AskingUI;

    public bool Stage02Clear = false;
    public bool Stage02Fail = false;
    
    public GameObject UpgrageUI;
    public GameObject HUD;
    
    
    public void Stage1To2()
    {
        playerItem.isShoping = false;
        UpgrageUI.SetActive(false);
        HUD.SetActive(true);
        if(playerItem.CurrentMoney >= 30000)
        {
            playerItem.CurrentMoney -= 30000;
            Stage01Clear = true;
            
            Invoke("ShowAskingUI", 0.1f);
        }
        else if(playerItem.CurrentMoney < 30000)
        {
            Stage01Fail = false;                
        }
            
    }
    public void Stage2To3()
    {
        playerItem.isShoping = false;
        UpgrageUI.SetActive(false);
        HUD.SetActive(true);
        if(playerItem.CurrentMoney >= 60000)
        {
            playerItem.CurrentMoney -= 60000;
            Stage02Clear = true;
            
            ShowAskingUI();
        }
        else if(playerItem.CurrentMoney < 30000)
        {
            Stage02Fail = false;                
        }
            
    }
    void ShowAskingUI()
    {

        playerItem.isShoping = true;
        AskingUI.SetActive(true);
    }
}
