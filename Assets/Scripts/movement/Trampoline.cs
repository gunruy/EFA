using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Trampoline Settings")]
    public float gravityReductionFactor = 2f; // Z�plama s�ras�nda yer�ekimi ne kadar azalacak
    public float jumpForceMultiplier = 1.5f; // Z�plama kuvvetini artt�rma oran�

    private bool isOnTrampoline = false;
    private float defaultGravity;
    private Rigidbody playerRb;

    private void Start()
    {
        defaultGravity = Physics.gravity.y; // Varsay�lan yer�ekimi de�erini al�yoruz
    }

    private void Update()
    {
        if (isOnTrampoline)
        {
            // Trambolinin �st�ndeyken yer�ekimini azalt
            Physics.gravity = new Vector3(0, defaultGravity / gravityReductionFactor, 0);
        }
        else
        {
            // Yer�ekimini yava��a eski haline d�nd�r
            Physics.gravity = new Vector3(0, Mathf.Lerp(Physics.gravity.y, defaultGravity, Time.deltaTime * 3f), 0);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // E�er karakter bu objeyle �arp�yorsa
        if (other.gameObject.CompareTag("Player")) // "Player" tag'� olan objelerle �arpacak
        {
            isOnTrampoline = true;
            playerRb = other.gameObject.GetComponent<Rigidbody>(); // Player'�n Rigidbody'sini al�yoruz
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, playerRb.linearVelocity.y * jumpForceMultiplier, playerRb.linearVelocity.z); // Z�plama kuvvetini artt�r
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // E�er karakter trambolinden ayr�l�yorsa
        if (other.gameObject.CompareTag("Player"))
        {
            isOnTrampoline = false;
        }
    }
}
