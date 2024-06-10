using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 20f;
    private CharacterController cc;
    public Animator cameraAnim;
    private bool isWalking;

    public float animcamSpeed = 1f;
    private Vector3 inputVector;
    private Vector3 movementVector;
    private float gravity = -10f;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
        CheckForHeadBot();

        cameraAnim.SetBool("isWalking", isWalking);
        cameraAnim.SetFloat("animcamSpeed", animcamSpeed);
    }

    void GetInput()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        inputVector =  transform.TransformDirection(inputVector);

        movementVector = (inputVector * playerSpeed) + (Vector3.up * gravity);
    }

    void MovePlayer()
    {
        cc.Move(movementVector * Time.deltaTime);
    }

    void CheckForHeadBot()
    {
        if(cc.velocity.magnitude>0.1f)
        {
            isWalking = true;
            animcamSpeed = 1f + playerSpeed / 16;

        } else
        {
            animcamSpeed = 1f;
            isWalking = false;
        }
    }
}
