using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationtrigger : MonoBehaviour
{
     Animator animator;
    int isWalkingHash;
    int isRunningHash;
    public float speed;

    void Start()
    {
        animator= GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);




        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
             
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (!isRunning && (runPressed && forwardPressed))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!runPressed || !forwardPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
