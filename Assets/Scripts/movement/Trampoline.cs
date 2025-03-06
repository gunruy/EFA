using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Trampoline Settings")]
    public float gravityReductionFactor = 2f; // Zýplama sýrasýnda yerçekimi ne kadar azalacak
    public float jumpForceMultiplier = 1.5f; // Zýplama kuvvetini arttýrma oraný

    private bool isOnTrampoline = false;
    private float defaultGravity;
    private Rigidbody playerRb;

    private void Start()
    {
        defaultGravity = Physics.gravity.y; // Varsayýlan yerçekimi deðerini alýyoruz
    }

    private void Update()
    {
        if (isOnTrampoline)
        {
            // Trambolinin üstündeyken yerçekimini azalt
            Physics.gravity = new Vector3(0, defaultGravity / gravityReductionFactor, 0);
        }
        else
        {
            // Yerçekimini yavaþça eski haline döndür
            Physics.gravity = new Vector3(0, Mathf.Lerp(Physics.gravity.y, defaultGravity, Time.deltaTime * 3f), 0);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Eðer karakter bu objeyle çarpýyorsa
        if (other.gameObject.CompareTag("Player")) // "Player" tag'ý olan objelerle çarpacak
        {
            isOnTrampoline = true;
            playerRb = other.gameObject.GetComponent<Rigidbody>(); // Player'ýn Rigidbody'sini alýyoruz
            if (playerRb != null)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, playerRb.velocity.y * jumpForceMultiplier, playerRb.velocity.z); // Zýplama kuvvetini arttýr
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // Eðer karakter trambolinden ayrýlýyorsa
        if (other.gameObject.CompareTag("Player"))
        {
            isOnTrampoline = false;
        }
    }
}
