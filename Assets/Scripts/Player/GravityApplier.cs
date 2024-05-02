using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityApplier : MonoBehaviour
{
    [SerializeField] float gravity;
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject groundCheckObject;
    [SerializeField] float groundCheckRange;
    [SerializeField] LayerMask groundLayer;
    public bool isGrounded;

    private void Update()
    {
        isGrounded = controller.isGrounded;
        //isGrounded = Physics.CheckSphere(groundCheckObject.transform.position, groundCheckRange, groundLayer);
    }

    public void ChangeGroundCheckYLevel(int change)
    {
        if(change == 0)
        {
            groundCheckObject.transform.localPosition = new Vector3(0, -0.85f, 0); //default
        }
        else if(change == 1)
        {
            groundCheckObject.transform.localPosition = new Vector3(0, -0.1f, 0); //crouched
        }
        else if (change == 2)
        {
            //crawl
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(groundCheckObject.transform.position, groundCheckRange);
    //}

    private void FixedUpdate()
    {
        if(isGrounded) return;

        controller.Move(Vector3.down * gravity);
    }
}