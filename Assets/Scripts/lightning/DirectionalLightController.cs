using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    public Light directionalLight;
    public Transform target; // Hedef nesne (örneðin, oyuncu)
    public float rotationSpeed = 1.0f; // Iþýðýn dönüþ hýzý
    public float intensityMultiplier = 1.0f; // Iþýðýn yoðunluk çarpaný

    private void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>();
        }
    }

    private void Update()
    {
        if (directionalLight != null && target != null)
        {
            // Iþýðýn hedefe doðru dönmesini saðla
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Iþýðýn yoðunluðunu güncelle
            float distance = Vector3.Distance(transform.position, target.position);
            directionalLight.intensity = Mathf.Clamp(1.0f / (distance * intensityMultiplier), 0.1f, 1.0f);
        }
    }
}
