using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //public InputAction MoveAction;
    //private float movementSpeed = 5.0f;
    [SerializeField] private InputActionAsset voiceIA;
    [SerializeField] private InputActionAsset buttonIA;

    private Rigidbody2D pcRigidBody;
    private PlayerInput playerInput;
    public GameTimer gameTimer;
    
    private bool b_voiceToggle = false;
    
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
    private bool b_sliding = false;
    private bool b_jumping = false;
    private float iceBoost = 2.5f;
    private bool b_wipeout = false;
    private bool b_activeBoost = false;

    // Alpine Ski Recreation vars below
    /*
    public float alpineSkiVelocity_y = 5.0f;
    public float alpineSkiVelocity_x = 2.5f;
    public float alpineSkiBoost = 2.5f;
    public float alpineBoostDuration = 1.0f;
    */


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MoveAction.Enable();
        pcRigidBody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        verticalVelocity = baseVelocity + baseVerticalMult;
        horizontalVelocity = verticalVelocity * baseHorizontalMult;
        timerMultiplier = gameTimer.Get_CurrentTime();
        b_wipeout = false;

        if(b_voiceToggle)
        {
            playerInput.actions = voiceIA;
        }
        else
        {
            playerInput.actions = buttonIA;
        }

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
        timerMultiplier = gameTimer.Get_CurrentTime();
        CalcVerticalVelocity();
        CalcHorizontalVelocity();
        pcRigidBody.linearVelocityY = verticalVelocity;
        pcRigidBody.linearVelocityX = moveInput.x * horizontalVelocity;

        // Alpine Ski Recreation code below  
        //pcRigidBody.linearVelocityY = alpineSkiVelocity_y;
        //pcRigidBody.linearVelocityX = alpineSkiVelocity_x * moveInput.x;
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
        Debug.Log("Jump");

        if(!b_jumping)
        {
            b_jumping = true;
        }

    }

    // No check to stop player from infinite boost
    public void YipBoost(InputAction.CallbackContext context)
    {
        yipBtnInput = context.ReadValue<float>();
        Debug.Log("YipBoost");

        if(!b_activeBoost)
        {
            b_activeBoost = true;
            
            //BoostDelay(gameTimer.Get_CurrentTime());
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
        float boostEnd = boostStart + 1;

        do
        {
            b_activeBoost = true;
        } while ((gameTimer.Get_CurrentTime() <= boostEnd));

        b_activeBoost = false;
        //alpineSkiVelocity_y = baseVelocity;
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
        //alpineSkiVelocity_y += iceBoost;

    }

    // Needs to be called on collision with obstacles
    private void Wipeout()
    {
        pcRigidBody.linearVelocityX = 0;
        pcRigidBody.linearVelocityY = 0;
        //alpineSkiVelocity_y = 0;
        //alpineSkiVelocity_x = 0;
        Debug.Log("Wipeout");
        b_wipeout = true;

        // Call Game End
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
        else if(other.gameObject.CompareTag("Rock"))
        {
            if(b_jumping)
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                Destroy(rb);
                BoxCollider2D boxColl = other.GetComponent<BoxCollider2D>();
                Destroy(boxColl);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("IceCollider"))
        {
            //alpineSkiVelocity_y -= iceBoost;
            b_sliding = false;
        }
    }

    public bool get_ActiveBoost()
    {
        return b_activeBoost;
    }

    public void set_ActiveBoost(bool p_boost)
    {
        b_activeBoost = p_boost;
    }

    public bool get_VoiceToggle()
    {
        return b_voiceToggle;
    }

    public void set_VoiceToggle(bool p_toggle)
    {
        b_voiceToggle = p_toggle;
    }

    /*
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
    */

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

    public bool get_Jumping()
    {
        return b_jumping;
    }

    public void set_jumping(bool p_jump)
    {
        b_jumping = p_jump;
    }
}
