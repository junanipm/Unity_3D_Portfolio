using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventNPC : MonoBehaviour
{

    public TMP_Text EventNPC01_Text;
    public string[] evnet01_texts;
    private int clickCount = 0;

    void StartEvent01()
    {
        if(evnet01_texts.Length >0)
        {
            EventNPC01_Text.text = evnet01_texts[0];
        }
    }
    public void Event01_OnButtonClick()
    {
        clickCount++;

        if (clickCount <= evnet01_texts.Length)
        {
            EventNPC01_Text.text = evnet01_texts[clickCount - 1];
        }
        else if (clickCount == evnet01_texts.Length + 1)
        {
            
        }
    }
    

    
    }


