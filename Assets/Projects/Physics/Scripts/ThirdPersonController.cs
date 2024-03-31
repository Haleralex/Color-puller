using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    //input fields
    private ThirdPersonActionsAsset playerActionsAsset;
    private InputAction move;

    //movement fields
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;
    //private Animator animator;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonActionsAsset();
        //animator = this.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += DoJump;
        playerActionsAsset.Player.Attack.started += DoAttack;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= DoJump;
        playerActionsAsset.Player.Attack.started -= DoAttack;
        playerActionsAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        var moveReadValue = move.ReadValue<Vector2>();
        forceDirection += moveReadValue.x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += moveReadValue.y * GetCameraForward(playerCamera) * movementForce;
        var cachedDirection = forceDirection;
        if (forceDirection == Vector3.zero)
        {
            /* var vel = new Vector3(-rb.velocity.x, 0, -rb.velocity.z); */
            rb.AddForce(-rb.velocity * 0.3f, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;
        }


        if (!IsGrounded())
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt(cachedDirection);
    }

    private void LookAt(Vector3 moveValue)
    {
        Vector3 direction = moveValue;
        Debug.Log(direction);
        direction.y = 0f;
        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        {
            var e = direction * 100f + transform.position;
            e.y = 0f;
            /* Debug.Log(e);
            transform.LookAt(e, Vector3.up); */

            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
            /*  Debug.Log(this.rb.rotation); */
        }
        else
        {
            rb.angularVelocity = Vector3.zero;

        }
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        return transform.position.y <= 5;
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        //animator.SetTrigger("attack");
    }
}