using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //public InputAction MoveAction;
    
    private float movementSpeed = 5.0f;
    private Rigidbody2D pcRigidBody;
    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MoveAction.Enable();
        pcRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        //Vector2 position = (Vector2)transform.position + move * 0.01f;
        //transform.position = position;

        pcRigidBody.linearVelocity = moveInput * movementSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
