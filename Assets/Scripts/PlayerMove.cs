using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float playerSpeed = 10f;

    public float  momentumDamping = 5f;
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

        cameraAnim.SetBool("isWalking", isWalking);
        cameraAnim.SetFloat("animcamSpeed", animcamSpeed);
    }

    void GetInput()
    {
        // si on est en train de bouger
        if(Input.GetKey(KeyCode.Z) || 
           Input.GetKey(KeyCode.Q) || 
           Input.GetKey(KeyCode.S) || 
           Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();
            inputVector =  transform.TransformDirection(inputVector);

            isWalking = true;
        } 
        // si on s'arrête de bouger
        else 
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, momentumDamping * Time.deltaTime);
            isWalking = false;
        }
            
        movementVector = (inputVector * playerSpeed) + (Vector3.up * gravity);
    }

    void MovePlayer()
    {
        cc.Move(movementVector * Time.deltaTime);
    }
}
