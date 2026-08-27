using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //public InputAction MoveAction;
    
    private float movementSpeed = 5.0f;
    private Rigidbody2D pcRigidBody;
    private Vector2 moveInput;

    private float verticalVelocity;
    private float horizontalVelocity;
    public float baseVelocity = 5.0f;
    public float baseVerticalMult = 0.4f;
    public float baseExponential = 1.05f;
    public float baseHorizontalMult = 0.4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MoveAction.Enable();
        pcRigidBody = GetComponent<Rigidbody2D>();
        verticalVelocity = baseVelocity + baseVerticalMult;
        horizontalVelocity = verticalVelocity * baseHorizontalMult;

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

        CalcVerticalVelocity();
        CalcHorizontalVelocity();
        pcRigidBody.linearVelocityY = verticalVelocity;
        pcRigidBody.linearVelocityX = moveInput.x * horizontalVelocity;
        
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

    private void CalcVerticalVelocity()
    {
        // design formula starting point 
        // velocity = baseVelocity + baseVerticalMult X t^(baseExponential)
        verticalVelocity = baseVelocity + baseVerticalMult * Mathf.Pow(1.0f, baseExponential);
        verticalVelocity = 1.0f;
    }

    private void CalcHorizontalVelocity()
    {
        horizontalVelocity = verticalVelocity * baseHorizontalMult;
    }
}
