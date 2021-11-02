using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 8f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject defaultBullet;
   // public PlayerInputActions playerInputActions;
    Vector2 movement;
    public Vector2 facing;
    bool strafing = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //input
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        //movement = playerInputActions.Player.Movement.ReadValue<Vector2>();
        if (movement.sqrMagnitude > 0.01 && !strafing)
        {
            facing = movement;
        }
        animator.SetFloat("Horizontal", facing.x);
        animator.SetFloat("Vertical", facing.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        //physics movement
        if (movement.magnitude > 1)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }


    Vector2 RotateVec2D(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            GameObject newbullet = Instantiate(defaultBullet);
            float angle = Mathf.Acos(Vector2.Dot(facing.normalized, new Vector2(1, 0)));
            if (facing.y < 0)
            {
                angle = Mathf.PI * 2 - angle;
            }
            angle = Mathf.Rad2Deg*angle;
            Debug.Log(angle);
            int clamps = (int)((angle / 45.0f)+.5f);
            angle = clamps * 45.0f;
            Vector2 clampedDir = RotateVec2D(new Vector2(1, 0), angle * Mathf.Deg2Rad); 
            newbullet.transform.position = gameObject.transform.position + new Vector3(clampedDir.x, clampedDir.y, 0).normalized*.6f;
            newbullet.GetComponent<BulletScript>().dir = clampedDir;
        }
    }
    public void Strafe(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            strafing = true;
        }
        if (context.canceled)
        {
            strafing = false;
        }
    }
    public void MovementInput(InputAction.CallbackContext context)
    {
        //if (context.performed)
        //{
        movement = context.ReadValue<Vector2>();
        if (movement.sqrMagnitude > 0.01 && !strafing)
        {
            facing = movement;
        }
        animator.SetFloat("Horizontal", facing.x);
        animator.SetFloat("Vertical", facing.y);
        animator.SetFloat("Speed", movement.magnitude);
        //}
    }
}
