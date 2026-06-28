using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CafeOpenObject : MonoBehaviour
{

    



    public bool test = false;
    public Image OpenUI;
    public Image CloseUI;
    public Image NPCComing;
    bool FirstActive= false;
    void Start()
    {
        NPCComing.gameObject.SetActive(false);
        OpenUI.gameObject.SetActive(false);
        CloseUI.gameObject.SetActive(true);
    }
     void Update()
    {
        if (test == true)
        {
                                       
            transform.rotation = Quaternion.Euler(new Vector3 ( 0, -90, 0 ));
            OpenUI.gameObject.SetActive(true);
            CloseUI.gameObject.SetActive(false);
            if(!FirstActive)
            {
                ActiveOnce();
            }
            
        }
        else if(test == false )
        {


            transform.rotation = Quaternion.Euler(new Vector3 ( 0, 0, 0 ));
            OpenUI.gameObject.SetActive(false);
            CloseUI.gameObject.SetActive(true);
        }
    }
    void NPCComingInActive()
    {
        NPCComing.gameObject.SetActive(false);
    }
    void ActiveOnce()
    {
        NPCComing.gameObject.SetActive(true);
        StartCoroutine(ActiveWaitting());
    }
    IEnumerator ActiveWaitting()
    {
        yield return new WaitForSeconds(2f);
        NPCComing.gameObject.SetActive(false);
        FirstActive = true;
    }
}
