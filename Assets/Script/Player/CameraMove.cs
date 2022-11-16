using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float mouseSensitvity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

     private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*mouseSensitvity*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*mouseSensitvity*Time.deltaTime;

        xRotation -= mouseY;
        yRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, -40f, 40f);
        transform.localRotation = Quaternion.Euler(xRotation, -yRotation, 0f);        
    }
}
