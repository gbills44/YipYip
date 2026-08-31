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
    //public float fakeTimer = 1.0f;

    // Alpine Ski Recreation vars below
    private bool b_activeBoost = false;
    public float alpineSkiVelocity_y = 5.0f;
    public float alpineSkiVelocity_x = 2.5f;
    public float alpineSkiBoost = 2.5f;
    public float alpineBoostDuration = 1.0f;
    private bool b_sliding = false;
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
        //CalcVerticalVelocity();
        //CalcHorizontalVelocity();
        //pcRigidBody.linearVelocityY = verticalVelocity;
        //pcRigidBody.linearVelocityX = moveInput.x * horizontalVelocity;

        // Alpine Ski Recreation code below  
        pcRigidBody.linearVelocityY = alpineSkiVelocity_y;
        pcRigidBody.linearVelocityX = alpineSkiVelocity_x * moveInput.x;
    }

    public float get_VerticalVelocity()
    {
        return verticalVelocity;
    }

    public void set_VerticalVelocity(float p_vel)
    {
        verticalVelocity = p_vel;
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
            b_activeBoost = true;
            alpineSkiVelocity_y = alpineSkiVelocity_y + alpineSkiBoost;
            BoostDelay(gameTimer.Get_CurrentTime());
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
        // design formula starting point 
        // velocity = baseVelocity + baseVerticalMult X t^(baseExponential)
        verticalVelocity = baseVelocity + baseVerticalMult * Mathf.Pow(timerMultiplier, baseExponential);
    }

    private void CalcHorizontalVelocity()
    {
        horizontalVelocity = verticalVelocity * baseHorizontalMult;
    }

    private void BoostDelay(float p_time)
    {
        float boostStart = p_time;
        float boostEnd = boostStart + alpineBoostDuration;

        do
        {
            b_activeBoost = true;
        } while ((gameTimer.Get_CurrentTime() <= boostEnd));

        b_activeBoost = false;
        alpineSkiVelocity_y = baseVelocity;
    }

    // Needs to be called on collision with ice patch
    private void IceSlide()
    {
        if(!b_sliding)
        {
            b_sliding = true;
            //IceSlideBoost(gameTimer.Get_CurrentTime());
        }
    }

    // Needs to be called on collision with obstacles
    private void Wipeout()
    {
        pcRigidBody.linearVelocityX = 0;
        pcRigidBody.linearVelocityY = 0;
        alpineSkiVelocity_y = 0;
        alpineSkiVelocity_x = 0;
    }

    public bool get_ActiveBoost()
    {
        return b_activeBoost;
    }

    public void set_ActiveBoost(bool p_boost)
    {
        b_activeBoost = p_boost;
    }

    public float get_AlpineSkiVelocityY()
    {
        return alpineSkiVelocity_y;
    }

    public void set_AlpineSkiVelocityY(float p_vel)
    {
        alpineSkiVelocity_y = p_vel;
    }
    public float get_AlpineSkiVelocityX()
    {
        return alpineSkiVelocity_x;
    }

    public void set_AlpineSkiVelocityX(float p_vel)
    {
        alpineSkiVelocity_x = p_vel;
    }
    public float get_AlpineSkiBoost()
    {
        return alpineSkiBoost;
    }

    public void set_AlpineSkiBoost(float p_vel)
    {
        alpineSkiBoost = p_vel;
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
