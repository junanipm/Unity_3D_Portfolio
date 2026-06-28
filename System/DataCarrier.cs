using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarrier : MonoBehaviour
{

    private static DataCarrier instance;
    public bool D_Event01 = false;
    public bool D_Event02 = false;
    public bool D_Event03 = false;
    public int CakeCount = 0;
    public int TiramisuCount = 0;
    public int CoffeeCount = 0;
    public int CoffeeIngredient = 0;
    public int BreadIngredient = 0;
    public int MilkIngredient = 0;
    public int EggIngredient = 0;
    public int IceCreamIngredient = 0;
    public int MelonIngredient = 0;
    public int CheeseIngredient = 0;
    public int CurrentMoney = 0;
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static DataCarrier GetInstance()
    {
        return instance;
    }
    void Update()
    {
        
    }
}
