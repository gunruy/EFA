using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class PlayerCam : MonoBehaviour
//{
//    public float sensX;
//    public float sensY;

//    public Transform orientation;

//    float xRotation;
//    float yRotation;

//    private void Start()
//    {
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;
//    }

//    private void Update()
//    {
//        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
//        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

//        yRotation += mouseX;
//        xRotation -= mouseY;

//        xRotation = Mathf.Clamp(xRotation, -90, 90);


//        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
//        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
//    }


//}

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform playerBody; // Oyuncu karakterinin d�n��� i�in

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Fare hareketlerini al
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Rotasyonlar� g�ncelle
        yRotation += mouseX;
        xRotation -= mouseY;

        // Dikey rotasyonu s�n�rla (bak���n yukar�/a�a�� hareketini s�n�rlamak i�in)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kamera ve karakterin y�n�n� g�ncelle
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamera i�in sadece dikey rotasyon
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f); // Karakterin y�n� i�in sadece yatay rotasyon
    }
}

