using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoppingSystem : MonoBehaviour
{
    public PlayerItem playerItem;
    public DeliveryBox deliveryBox;
    public DeliveryProcess deliveryProcess;
    

    public TMP_Text CoffeeAmountText;
    public TMP_Text BreadAmountText;
    public TMP_Text MilkAmountText;
    public TMP_Text EggAmountText;
    public TMP_Text IceCreamAmountText;
    public TMP_Text MelonAmountText;
    public TMP_Text CheeseAmountText;
    public TMP_Text AllCostText;
    public TMP_Text CurrentMoneyText;

    public int CoffeeAmount = 0;
    public int BreadAmount = 0;
    public int MilkAmount = 0;
    public int EggAmount = 0;
    public int IceCreamAmount = 0;
    public int MelonAmount = 0;
    public int CheeseAmount = 0;
    public int Allcost = 0;

    public Image purchaseComplete;
    public bool canPurchase = true;

    public void Start()
    {
        UpdateAmountText();
    }
    
    public void Update()
    {
        
        Allcost = CoffeeAmount*100 + BreadAmount*100 + MilkAmount*100 + EggAmount*150 + IceCreamAmount*200 + MelonAmount*150 +CheeseAmount*300;

        AllCostText.text = "가격:" + Allcost.ToString();
        CurrentMoneyText.text = "" + playerItem.CurrentMoney.ToString();
        UpdateAmountText();
    }
    public void CoffeeAmountAdd()
    {
        int totalCoffee = playerItem.CoffeeIngredient + CoffeeAmount;


        if (totalCoffee <= playerItem.ItemMax)
        {
            CoffeeAmount += 1;
            

        }
        else
        {
            Debug.Log("아이템 총량을 초과할 수 없습니다.");
        }

    }

    public void CoffeeAmountReduce()
    {
        if (CoffeeAmount > 0)
        {
            CoffeeAmount -= 1;

        }
        else
        {
            Debug.Log("커피 재료가 0 이하로 감소할 수 없습니다.");
        }
    }

    public void BreadAmountAdd()
    {
        int totalBread = playerItem.BreadIngredient + BreadAmount;


        if (totalBread <= playerItem.ItemMax)
        {
            BreadAmount += 1;
            

        }
        else
        {
            Debug.Log("아이템 총량을 초과할 수 없습니다.");
        }

    }

    public void BreadAmountReduce()
    {
        if (BreadAmount > 0)
        {
            BreadAmount -= 1;

        }
        else
        {
            Debug.Log("빵 재료가 0 이하로 감소할 수 없습니다.");
        }
    }

    public void MilkAmountAdd()
    {
        int totalMilk = playerItem.MilkIngredient + MilkAmount;


        if (totalMilk <= playerItem.ItemMax)
        {
            MilkAmount += 1;
            

        }
        else
        {
            Debug.Log("아이템 총량을 초과할 수 없습니다.");
        }

    }

    public void MilkAmountReduce()
    {
        if (MilkAmount > 0)
        {
            MilkAmount -= 1;

        }
        else
        {
            Debug.Log("빵 재료가 0 이하로 감소할 수 없습니다.");
        }
    }

    public void EggAmountAdd()
    {
        int totalEgg = playerItem.EggIngredient + EggAmount;


        if (totalEgg <= playerItem.ItemMax)
        {
            EggAmount += 1;
            

        }
        else
        {
            Debug.Log("아이템 총량을 초과할 수 없습니다.");
        }

    }

    public void EggAmountReduce()
    {
        if (EggAmount > 0)
        {
            EggAmount -= 1;

        }
        else
        {
            Debug.Log("빵 재료가 0 이하로 감소할 수 없습니다.");
        }
    }

    public void IceCreamAmountAdd()
    {
        int totalIceCream = playerItem.IceCreamIngredient + IceCreamAmount;


        if (totalIceCream <= playerItem.ItemMax)
        {
            IceCreamAmount += 1;
            

        }
        else
        {
            Debug.Log("아이템 총량을 초과할 수 없습니다.");
        }

    }

    public void IceCreamAmountReduce()
    {
        if (IceCreamAmount > 0)
        {
            IceCreamAmount -= 1;

        }
        else
        {
            Debug.Log("빵 재료가 0 이하로 감소할 수 없습니다.");
        }
    }

    public void MelonAmountAdd()
    {
        int totalMelon = playerItem.MelonIngredient + MelonAmount;


        if (totalMelon <= playerItem.ItemMax)
        {
            MelonAmount += 1;
            

        }
        else
        {
            Debug.Log("아이템 총량을 초과할 수 없습니다.");
        }

    }

    public void MelonAmountReduce()
    {
        if (MelonAmount > 0)
        {
            MelonAmount -= 1;

        }
        else
        {
            Debug.Log("빵 재료가 0 이하로 감소할 수 없습니다.");
        }
    }
    
    public void CheeseAmountAdd()
    {
        int totalCheese = playerItem.CheeseIngredient + CheeseAmount;


        if (totalCheese <= playerItem.ItemMax)
        {
            CheeseAmount += 1;
            

        }
        else
        {
            Debug.Log("아이템 총량을 초과할 수 없습니다.");
        }

    }

    public void CheeseAmountReduce()
    {
        if (CheeseAmount > 0)
        {
            CheeseAmount -= 1;

        }
        else
        {
            Debug.Log("빵 재료가 0 이하로 감소할 수 없습니다.");
        }
    }

    public void Purchase()
    {
        if(playerItem.CurrentMoney >= Allcost && canPurchase)
        {
            /*
            playerItem.CoffeeIngredient += CoffeeAmount;
            playerItem.BreadIngredient += BreadAmount;
            playerItem.MilkIngredient += MilkAmount;
            playerItem.EggIngredient += EggAmount;
            playerItem.IceCreamIngredient += IceCreamAmount;
            playerItem.MelonIngredient += MelonAmount;
            playerItem.CheeseIngredient += CheeseAmount;
            playerItem.CurrentMoney -= Allcost;
            */
            deliveryBox.boxCoffee += CoffeeAmount;
            deliveryBox.boxMilk += MilkAmount;
            deliveryBox.boxBread += BreadAmount;
            deliveryBox.boxEgg += EggAmount;
            deliveryBox.boxIceCream += IceCreamAmount;
            deliveryBox.boxMelon += MelonAmount;
            deliveryBox.boxCheese += CheeseAmount;
            playerItem.CurrentMoney -= Allcost;

            CoffeeAmount = 0;
            BreadAmount = 0;
            MilkAmount = 0;
            EggAmount = 0;
            IceCreamAmount = 0;
            MelonAmount = 0;
            CheeseAmount = 0;

            deliveryProcess.WaitAndSpawn();
            playerItem.mainToUI();
            ActivePurchase();
            Invoke("DeActivePurchase", 2f);
        }
        else if(playerItem.CurrentMoney <= Allcost && canPurchase)
        {
            Debug.Log("돈이 모자라요");
        }
        else if(playerItem.CurrentMoney >= Allcost && !canPurchase)
        {
            Debug.Log("전에 시킨 물품을 먼저 사용해 주세요");
        }
        else
        {
            Debug.Log("전에 시킨 물품을 먼저 사용해 주세요");
        }
    }

    

    void UpdateAmountText()
    {
        CoffeeAmountText.text = "" + CoffeeAmount.ToString();
        BreadAmountText.text = "" + BreadAmount.ToString();
        EggAmountText.text = "" + EggAmount.ToString();
        MilkAmountText.text = "" + MilkAmount.ToString();
        IceCreamAmountText.text = "" + IceCreamAmount.ToString();
        MelonAmountText.text = "" + MelonAmount.ToString();
        CheeseAmountText.text = "" + CheeseAmount.ToString();
    }
    public void ActivePurchase()
    {
        purchaseComplete.gameObject.SetActive(true);
    }
    public void DeActivePurchase()
    {
        purchaseComplete.gameObject.SetActive(false);
    }
}
