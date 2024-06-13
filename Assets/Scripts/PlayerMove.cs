using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float momentumDamping = 5f;
    public float dashSpeed = 20f; 
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private CharacterController cc;
    public Animator cameraAnim;
    private bool isWalking;
    private bool isDashing;
    private float dashTime;
    private float lastDashTime;

    public float animcamSpeed = 1f;
    private Vector3 inputVector;
    private Vector3 movementVector;
    private float gravity = -10f;
    public bool isInvisible = false;


    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private IEnumerator TurnOnAndResetBoolean()
    {
        isInvisible = true;
        yield return new WaitForSeconds(6f);
        isInvisible = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TurnOnAndResetBoolean());
        }
    

        GetInput();
        MovePlayer();
        cameraAnim.SetBool("isWalking", isWalking);
        cameraAnim.SetFloat("animcamSpeed", animcamSpeed);
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime >= dashDuration)
            {
                EndDash();
            }
        }
        else
        {
            // si on est en train de bouger
            if (Input.GetKey(KeyCode.Z) || 
                Input.GetKey(KeyCode.Q) || 
                Input.GetKey(KeyCode.S) || 
                Input.GetKey(KeyCode.D))
            {
                inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                inputVector.Normalize();
                inputVector = transform.TransformDirection(inputVector);

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
    }

    void MovePlayer()
    {
        cc.Move(movementVector * Time.deltaTime);
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = 0f;
        lastDashTime = Time.time;
        movementVector = inputVector * dashSpeed; 
    }

    void EndDash()
    {
        isDashing = false;
    }
}