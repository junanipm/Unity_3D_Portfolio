using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{

    public delegate void ItemDestroyedHandler();
    public event ItemDestroyedHandler OnItemDestroyed;

    void OnDestroy()
    {
        
        if (OnItemDestroyed != null)
        {
            OnItemDestroyed();
        }
    }
}
