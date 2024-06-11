using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float sensitivity = 1.5f;
    public float  smoothing = 1.5f;
    private float xMousePos;
    private float smoothMousePose;

    private float currentLookingPos;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
    }

    void ModifyInput()
    {
        xMousePos *= sensitivity * smoothing;
        smoothMousePose = Mathf.Lerp(smoothMousePose,  xMousePos, 1f/smoothing);
    }

    void MovePlayer()
    {
        currentLookingPos += smoothMousePose;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }
}