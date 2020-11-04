using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicKCC : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public int layerCollision;

    private float crouchTimer = 0.0f;
    private float crouchAccumulatedTime = 0.0f;
    private bool jumpOnce = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (!animator.GetBool("isCycle")) 
            {
                animator.SetBool("isCycle", true);
            }
            else 
            {
                animator.SetBool("isCycle", false);
            }
        }
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        transform.Translate(input * Time.deltaTime);

        animator.SetFloat("speed", input.magnitude);

        if (animator.GetFloat("speed") < 0.1f)
        {
            // will crouch half way
            SetDownCrouchBasedOnTime(0.5f, KeyCode.Z);
            // will crouch full way
            SetDownCrouchBasedOnTime(1.0f, KeyCode.C);
            // will not crouch anymore
            SetUpCrouchBasedOnTime(0.1f, KeyCode.X);
        }
        else
        {
            crouchAccumulatedTime = animator.GetFloat("crouch");
            crouchTimer = 0.0f;
        }
        if ((Input.GetKeyDown(KeyCode.Space) && (!animator.GetBool("jump") && animator.GetFloat("speed") < 0.1f)))
        {
            animator.SetBool("jump", true);
            jumpOnce = true;
        }
    }

    private void FixedUpdate()
    {
        if (jumpOnce) 
        {
            jumpOnce = false;
            rb.AddForce(new Vector3(0, 8, 0), ForceMode.Impulse);
        }
    }

    void SetDownCrouchBasedOnTime(float value, KeyCode key) 
    {
        if (Input.GetKey(key))
        {
            if (animator.GetFloat("crouch") < value)
            {
                crouchTimer += Time.deltaTime;
                if (crouchTimer >= 0.01)
                {
                    crouchAccumulatedTime += crouchTimer;
                    crouchTimer = 0.0f;
                    animator.SetFloat("crouch", crouchAccumulatedTime);
                }
            }
        }
    }
    void SetUpCrouchBasedOnTime(float value, KeyCode key)
    {
        if (Input.GetKey(key))
        {
            if (animator.GetFloat("crouch") > value)
            {
                crouchTimer += Time.deltaTime;
                if (crouchTimer >= 0.01)
                {
                    crouchAccumulatedTime -= crouchTimer;
                    crouchTimer = 0.0f;
                    animator.SetFloat("crouch", crouchAccumulatedTime);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == layerCollision) 
        {
            animator.SetBool("jump", false);
        }
    }
}
