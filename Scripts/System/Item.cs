using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    public GameObject[] foodObjects;
    public GameObject plate;
    public Transform[] emptySlots;
    public GameObject[] foodPrefabs;
    public bool isHoldingBox = false;
    public bool isfirstPlateCoffee = false;
    public bool isfirstPlateToast = false;
    
    public NPCOrder npcOrder;

    [Header("가격")]
        public int coffeePrice = 800;
        public int toastPrice = 1000;
        public int melonSodaPrice = 1200;
        public int supplePrice = 1500;
        public int tiramisuPrice = 2000;
    

    public string[] platedFood = new string[4];

    private int currentSlot = 0;
    
    string servingFood;


    PlayerItem playerItem;
    float maxDistance = 7f;
    public GameObject DBox;



    void Start()
    {
        playerItem = GetComponent<PlayerItem>();
    }
    void Update()
    {   
        MouseRayCast(); 
        
        if (Input.GetKeyDown(playerItem.KeyCodeInterAction) && npcOrder != null)
        {

            CheckNPCOrder();
            CheckEventOrder();
        }
    }

    void MouseRayCast()
    {
        
        if(Input.GetMouseButtonDown(0) && playerItem.mainCamera.enabled)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit, maxDistance))
            {
                
                Debug.DrawRay(ray.origin, ray.direction*maxDistance, Color.red, 10f);
                Debug.Log(""+hit.collider.gameObject.name);

                
                if(hit.collider.gameObject.tag == "Coffee" && playerItem.isPlated)
                {
                    isfirstPlateCoffee = true;
                    Destroy(hit.collider.gameObject);
                    playerItem.CakeCount -= 1;
                    Debug.Log(playerItem.CakeCount);
                }
                else if(hit.collider.gameObject.tag == "Toast" && playerItem.isPlated)
                {
                    isfirstPlateToast = true;
                    Destroy(hit.collider.gameObject);
                    playerItem.CoffeeCount -= 1;
                }
                else if(hit.collider.gameObject.tag == "Tiramisu" && playerItem.isPlated)
                {
                    Destroy(hit.collider.gameObject);
                    playerItem.TiramisuCount -=1;
                }
                else if(hit.collider.gameObject.tag == "Supple" && playerItem.isPlated)
                {
                    Destroy(hit.collider.gameObject);
                    playerItem.CoffeeCount -=1;
                }
                else if(hit.collider.gameObject.tag == "MelonSoda" && playerItem.isPlated)
                {
                    Destroy(hit.collider.gameObject);
                    playerItem.CakeCount -= 1;
                }

                else if(hit.collider.gameObject.tag == "Box")
                {   
                    isHoldingBox = true;
                    Destroy(hit.collider.gameObject);
                    DBox.SetActive(true);
                }
                
                for (int i = 0; i < foodObjects.Length; i++)
                {
                    if (hit.collider.gameObject.tag == foodObjects[i].tag)
                    {
                        
                        Debug.Log("1차");

                        PlaceFoodOnPlate(i);

                        break;
                    }
                }
            
            }
            

        }
    }


    int FindEmptySlot()
    {
        for (int i = 0; i < platedFood.Length; i++)
        {
            if (string.IsNullOrEmpty(platedFood[i]) && emptySlots[i].childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }
    void PlaceFoodOnPlate(int foodIndex)
    {
        int emptySlot = FindEmptySlot();
        if (emptySlot == -1)
        {
            Debug.Log("빈 슬롯이 없습니다.");
            return;
        }

        GameObject foodInstance = Instantiate(foodPrefabs[foodIndex], emptySlots[emptySlot].position, Quaternion.identity);
        foodInstance.transform.SetParent(emptySlots[emptySlot]);
        foodInstance.transform.localPosition = Vector3.zero;
        foodInstance.transform.localRotation = Quaternion.identity;

        platedFood[emptySlot] = foodPrefabs[foodIndex].tag;

        Debug.Log($"음식 추가: {platedFood[emptySlot]} (슬롯 {emptySlot})");
    }
    void CheckNPCOrder()
    {
        if (npcOrder == null || string.IsNullOrEmpty(npcOrder.FoodOrder))
        {
            Debug.Log(npcOrder == null ? "주문없음" : "주문정보없음");
            return;
        }


        for (int i = 0; i < platedFood.Length; i++)
        {
            Debug.Log($"플레이트 음식 {i}: {platedFood[i]}");
            if (platedFood[i] == npcOrder.FoodOrder)
            {
                Debug.Log($"주문과 일치: {platedFood[i]}");
                npcOrder.NPCSUCCESS();
                if(platedFood[i] == "Coffee")
                {
                    playerItem.CurrentMoney += coffeePrice;
                }
                else if(platedFood[i] == "Toast")
                {
                    playerItem.CurrentMoney += toastPrice;
                }
                else if(platedFood[i] == "Supple")
                {
                    playerItem.CurrentMoney += supplePrice;
                }
                else if(platedFood[i] == "MelonSoda")
                {
                    playerItem.CurrentMoney += melonSodaPrice;
                }
                else if(platedFood[i] == "Tiramisu")
                {
                    playerItem.CurrentMoney += tiramisuPrice;
                }
                DeletePlatedFood(i);
                break;
            }
        }
        Debug.Log("주문과 일치하는 음식이 없습니다.");
    }
    
    

    void DeletePlatedFood(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= platedFood.Length)
        {
            Debug.LogError($"잘못된 슬롯 인덱스: {slotIndex}");
            return;
        }

        Transform slot = emptySlots[slotIndex];
        if (slot.childCount > 0)
        {
            foreach (Transform child in slot)
            {
                Debug.Log($"슬롯 {slotIndex}에서 {child.tag} 삭제");
                Destroy(child.gameObject);
            }
        }

        platedFood[slotIndex] = null;

        Debug.Log($"슬롯 {slotIndex}의 음식이 삭제되었습니다.");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPCOrder")
        {
            Debug.Log("일단 콜라이더 감지는 됨");
            npcOrder = other.GetComponentInParent<NPCOrder>();
            if (npcOrder != null)
            {
                Debug.Log($"NPCOrder 참조 성공: {npcOrder.FoodOrder}");
            }
            else
            {
                Debug.LogError("NPCOrder 컴포넌트를 찾을 수 없습니다.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPCOrder"))
        {
            Debug.Log("NPC가 영역을 벗어남");
            npcOrder = null;
        }
    }
    void CheckEventOrder()
    {
        if (npcOrder == null)
        {
            Debug.Log("주문 정보가 없습니다.");
            return;
        }


        if (npcOrder is SpecialNPCOrder specialOrder)
        {
            Debug.Log($"특별 NPC {specialOrder.npcID}의 주문 확인 중...");
            
            CheckSpecialOrder(specialOrder);
        }
        else
        {
            Debug.Log("특별 NPC가 아닙니다.");
        }
    }

    void CheckSpecialOrder(SpecialNPCOrder specialOrder)
    {

        if (specialOrder.isOrderComplete || specialOrder.isOrderFailed)
        {
            Debug.Log($"특별 NPC {specialOrder.npcID}: 주문 상태 - 완료({specialOrder.isOrderComplete}), 실패({specialOrder.isOrderFailed})");
            return;
        }


        string[] requiredOrders = specialOrder.specialOrderList;


        bool allOrdersComplete = true;

        foreach (string requiredFood in requiredOrders)
        {
            int requiredCount = specialOrder.GetRequiredFoodCount(requiredFood);
            int currentCount = GetPlatedFoodCount(requiredFood);

            if (currentCount < requiredCount)
            {
                Debug.Log($"필요한 {requiredFood}: {requiredCount}, 현재 {currentCount}");
                allOrdersComplete = false;
            }
        }

        if (allOrdersComplete)
        {
            Debug.Log($"특별 NPC {specialOrder.npcID}: 주문이 완료되었습니다!");
            specialOrder.OnOrderSuccess();
            ClearPlatedFoods(requiredOrders);
        }
        else
        {
            Debug.Log($"특별 NPC {specialOrder.npcID}: 주문이 아직 완료되지 않았습니다.");
        }
    }
    int GetPlatedFoodCount(string foodTag)
    {
        int count = 0;


        foreach (string plated in platedFood)
        {
            if (plated == foodTag)
            {
                count++;
            }
        }

        return count;
    }

    void ClearPlatedFoods(string[] foodsToClear)
    {
        foreach (string food in foodsToClear)
        {
            for (int i = 0; i < platedFood.Length; i++)
            {
                if (platedFood[i] == food)
                {
                    DeletePlatedFood(i);
                }
            }
        }
    }
}
