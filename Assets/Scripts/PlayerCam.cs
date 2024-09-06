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

    public Transform playerBody; // Oyuncu karakterinin dönüþü için

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

        // Rotasyonlarý güncelle
        yRotation += mouseX;
        xRotation -= mouseY;

        // Dikey rotasyonu sýnýrla (bakýþýn yukarý/aþaðý hareketini sýnýrlamak için)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kamera ve karakterin yönünü güncelle
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamera için sadece dikey rotasyon
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f); // Karakterin yönü için sadece yatay rotasyon
    }
}

