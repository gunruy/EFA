using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    float xRotation;
    float yRotation;
    public float sensX = 50f;
    public float sensY = 50f;

    public Transform camera;


    void Start()
    {
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
        camera.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //sor neden rotation �al��m�yor ama localrotation �al���yor.
        //yrotationu parenttan almas� gerekiyordu ama normal rotation kullan�ld���nda sadece x ekseninde d�nerken parent�n yapt��� d�n��ten etkilenmiyordu. neden ?


    }


}
