using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCodes")]
    [SerializeField]
    private KeyCode keyCodeRun = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode keyCodeJump = KeyCode.Space;
    /*
    [SerializeField]
    private KeyCode KeyCodeInterAction = KeyCode.E;
    */

    private RotateToMouse rotateToMouse;
    private MovementCharacterController movement;
    private Status status;
    
    public PlayerItem playerItem;
    
    private PlayerAnimatorController animator;

    /*
    GameObject nearObject;
    public GameObject ItemCoffee;
    public GameObject ItemCake;
    public int ItemCount;
    public int ItemMax; 
    */
    private void Awake() 
    {   
        ToggleCursorState();

        rotateToMouse =     GetComponent<RotateToMouse>();
        movement =          GetComponent<MovementCharacterController>();
        status =            GetComponent<Status>();
        animator =          GetComponent<PlayerAnimatorController>();
    }


    private void Update()
    {

        if(!playerItem.isMiniGaming && !playerItem.isCakeGaming && !playerItem.isShoping && !playerItem.isClickerGaming)
        {
            UpdateRotate();
            UpdateMove();
            UpdateJump();
        }
        else
        {

            movement.MoveSpeed = 0;

        }
        ToggleCursorState();
    }
    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if ( x != 0 || z != 0)
        {
            bool isRun = false;

            if (z > 0) isRun = Input.GetKey(keyCodeRun);

            movement.MoveSpeed = isRun == true ? status.RunSpeed :status.WalkSpeed;

        }
        else
        {
            movement.MoveSpeed = 0;

        }

        movement.MoveTo(new Vector3(x, 0, z));
    }

    private void UpdateJump()
    {
        if(Input.GetKeyDown(keyCodeJump))
        {
            movement.Jump();
        }
    }
    /*
    private void InterAction()
    {
        if(Input.GetKeyDown(KeyCodeInterAction) && nearObject != null)
        {
            if(nearObject.tag == "Machine" && nearObject.name == "PW_stove")
            {
                Instantiate(ItemCake, new Vector3 (1,0,1), Quaternion.identity);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Machine")
            nearObject = other.gameObject;
        
        Debug.Log(nearObject.name);
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Machine")
            nearObject = null;
    }
    */

    void ToggleCursorState()
    {
        if (playerItem.isShoping)
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
}
