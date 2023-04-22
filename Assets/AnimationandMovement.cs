using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimationandMovement : MonoBehaviour
{
    PlayerControllAnimations PlayerControllAnimations;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 15f;
    float runMultiplier = 8.0f;
    float walkMultiplier = 2f;

    private void Awake()
    {
        PlayerControllAnimations = new PlayerControllAnimations();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        PlayerControllAnimations.CharacterControls.Move.started += onMovementInput;
        PlayerControllAnimations.CharacterControls.Move.canceled += onMovementInput;
        PlayerControllAnimations.CharacterControls.Move.performed += onMovementInput;
        PlayerControllAnimations.CharacterControls.Run.started += onRun;
        PlayerControllAnimations.CharacterControls.Run.canceled += onRun;

         
    }
    void onMovementInput (InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * walkMultiplier;
        currentMovement.z = currentMovementInput.y * walkMultiplier;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }
    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }
    void handleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }
    //void handleGravity()
    //{
    //    if (characterController.isGrounded)
    //    {
    //        float groundedGravity = -.05f;
    //        currentMovement.y = groundedGravity;
    //        currentRunMovement.y = groundedGravity;
    //    }
    //    else
    //    {
    //        float gravity = -9.8f;
    //        currentMovement.y += gravity;
    //        currentRunMovement.y += gravity;
    //    }
    //}
  
    void Update()
    {
        handleRotation();
        handleAnimation();

        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        


    }

    private void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if(isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if(!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        //else if((isMovementPressed && !isRunPressed) && isRunning)
        //{
        //    animator.SetBool(isWalkingHash, true);
        //}
        //else if ((!isMovementPressed && isRunPressed) && isRunning)
        //{
        //    animator.SetBool(isWalkingHash, false);
        //}
        else if ((!isMovementPressed && !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    private void OnEnable()
    {
        PlayerControllAnimations.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        PlayerControllAnimations.CharacterControls.Disable();
    }
}
