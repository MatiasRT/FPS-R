﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{ 
    CharacterController controller;
    private float verticalVelocity;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;
    [SerializeField] int jumps;
    int counter;
    bool isJumping;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            counter = 0;
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;

                isJumping = true;
            }
        }
        else verticalVelocity -= gravity * Time.deltaTime;

        if (isJumping)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                jumpForce = 25;
                verticalVelocity += jumpForce;
                counter++;
                isJumping = false;
            }
            jumpForce = 20;
        }

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);
    }
}
