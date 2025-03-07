using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    public Light directionalLight;
    public Transform target; // Hedef nesne (�rne�in, oyuncu)
    public float rotationSpeed = 1.0f; // I����n d�n�� h�z�
    public float intensityMultiplier = 1.0f; // I����n yo�unluk �arpan�

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
            // I����n hedefe do�ru d�nmesini sa�la
            Vector3 direction = target.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // I����n yo�unlu�unu g�ncelle
            float distance = Vector3.Distance(transform.position, target.position);
            directionalLight.intensity = Mathf.Clamp(1.0f / (distance * intensityMultiplier), 0.1f, 1.0f);
        }
    }
}
