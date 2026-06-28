using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UISound : MonoBehaviour
{
    public AudioClip click;
    public AudioClip Purchase;
    public void OnButtonSound()
    {
        SoundManager.instance.SFXPlay("Click", click);
    }
    public void OnPurchaseSound()
    {
        SoundManager.instance.SFXPlay("Purchase", Purchase);
    }
}
