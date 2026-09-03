using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //public InputAction MoveAction;
    //private float movementSpeed = 5.0f;
    private Rigidbody2D pcRigidBody;
    public GameTimer gameTimer;
    private Vector2 moveInput;
    private float jumpBtnInput;
    private float yipBtnInput;
    private float debugInputSB;
    private float verticalVelocity;
    private float horizontalVelocity;
    public float baseVelocity = 5.0f;
    public float baseVerticalMult = 0.4f;
    public float baseExponential = 1.05f;
    public float baseHorizontalMult = 0.4f;
    public float timerMultiplier = 0.01f;
    private bool b_activeBoost = false;
    //public float fakeTimer = 1.0f;

    // Alpine Ski Recreation vars below
    
    //public float alpineSkiVelocity_y = 5.0f;
    //public float alpineSkiVelocity_x = 2.5f;
    //public float alpineSkiBoost = 2.5f;
    //public float alpineBoostDuration = 1.0f;
    private bool b_sliding = false;
    private float iceBoost = 2.5f;
    private bool b_wipeout = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MoveAction.Enable();
        pcRigidBody = GetComponent<Rigidbody2D>();
        verticalVelocity = baseVelocity + baseVerticalMult;
        horizontalVelocity = verticalVelocity * baseHorizontalMult;
        timerMultiplier = gameTimer.Get_CurrentTime();

    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        //Vector2 position = (Vector2)transform.position + move * 0.01f;
        //transform.position = position;
        //moveInput.y = 0;
        //pcRigidBody.linearVelocity = moveInput * movementSpeed;
        //pcRigidBody.linearVelocityY = 5;
        //timerMultiplier = gameTimer.Get_CurrentTime();
        CalcVerticalVelocity();
        CalcHorizontalVelocity();
        pcRigidBody.linearVelocityY = verticalVelocity;
        pcRigidBody.linearVelocityX = moveInput.x * horizontalVelocity;
        timerMultiplier = gameTimer.Get_CurrentTime();

        // Alpine Ski Recreation code below  
        //pcRigidBody.linearVelocityY = alpineSkiVelocity_y;
        //pcRigidBody.linearVelocityX = alpineSkiVelocity_x * moveInput.x;
        Debug.Log("Vertical Velocity: " + verticalVelocity);
    }

    

    public float get_HorizontalVelocity()
    {
        return horizontalVelocity;
    }

    public void set_HorizontalVelocity(float p_vel)
    {
        horizontalVelocity = p_vel;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        jumpBtnInput = context.ReadValue<float>();
        Debug.Log("Jump - Needs to do something");
    }

    // No check to stop player from infinite boost
    public void YipBoost(InputAction.CallbackContext context)
    {
        yipBtnInput = context.ReadValue<float>();
        Debug.Log("YipBoost");

        if(!b_activeBoost)
        {
            
        }
        
    }

    public void DebugSpaceBar(InputAction.CallbackContext context)
    {
        debugInputSB = context.ReadValue<float>();
        Debug.Log("Spacebar debug");
        Debug.Log("Vertical Velocity: " + verticalVelocity);
        Debug.Log("Horizontal Velocity: " + horizontalVelocity);
    }

    private void CalcVerticalVelocity()
    {
        Debug.Log("CalcVel");
        // design formula starting point 
        // velocity = baseVelocity + baseVerticalMult X t^(baseExponential)
        float tempExponent = Mathf.Pow(timerMultiplier, baseExponential);
        Debug.Log("tempEx: " + tempExponent);    
        float tempVel = baseVelocity + (baseVerticalMult * tempExponent);
        Debug.Log("TempVel" + tempVel);
        set_VerticalVelocity(tempVel);
    }

    private void CalcHorizontalVelocity()
    {
        horizontalVelocity = verticalVelocity * baseHorizontalMult;
    }

    private void BoostDelay(float p_time)
    {
        
    }

    // Needs to be called on collision with ice patch
    private void IceSlide()
    {
        if(!b_sliding)
        {
            b_sliding = true;
            IceSlideBoost();
        }
    }

    // Should pass the ice patch that collision occured with
    private void IceSlideBoost()
    {
        Debug.Log("IceSlideBoost()");
        

    }

    // Needs to be called on collision with obstacles
    private void Wipeout()
    {
        pcRigidBody.linearVelocityX = 0;
        pcRigidBody.linearVelocityY = 0;
        
        Debug.Log("Wipeout");
    }

    // 2D physical collision check
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Rock"))
        {
            Wipeout();
        }
        else if(other.gameObject.CompareTag("Tree"))
        {
            Wipeout();
        }
    }

    // Collision trigger check
    // OnCollisionTriggerEnter2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("IceCollider"))
        {
            Debug.Log("Ice Collision");
            IceSlide();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("IceCollider"))
        {
           
            b_sliding = false;
        }
    }

    public float get_VerticalVelocity()
    {
        return verticalVelocity;
    }

    public void set_VerticalVelocity(float p_vel)
    {
        verticalVelocity = p_vel;
    }

    public bool get_ActiveBoost()
    {
        return b_activeBoost;
    }

    public void set_ActiveBoost(bool p_boost)
    {
        b_activeBoost = p_boost;
    }

    

    public float get_IceBoost()
    {
        return iceBoost;
    }

    public void set_IceBoost(float p_boost)
    {
        iceBoost = p_boost;
    }

    public bool get_Sliding()
    {
        return b_sliding;
    }

    public void set_Sliding(bool p_slip)
    {
        b_sliding = p_slip;
    }
}
