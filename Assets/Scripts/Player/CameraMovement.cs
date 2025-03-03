using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    float xRotation;
    float yRotation;
    public float sensX = 50f;
    public float sensY = 50f;

    //Stores the direction that object is facing.
    public Transform orientation;
    public Transform camera;


    void Start()
    {
        orientation = transform.Find("Orientation").GetComponent<Transform>();
        camera = transform.Find("Camera").GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
    }

    private void Look()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensY * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensX * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        camera.rotation = Quaternion.Euler(xRotation, yRotation, 0);


    }


}
