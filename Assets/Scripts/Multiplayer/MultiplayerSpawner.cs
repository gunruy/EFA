using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MultiplayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    // Spawn noktalarýný ayarla
    public Transform redTeamSpawnPoint;
    public Transform blueTeamSpawnPoint;

    // UI butonlarý
    public Button redButton;
    public Button blueButton;

    // Oyuncunun takýmýný tutmak için bir deðiþken
    private string selectedTeam;
    private bool playerStatus = false;


    [SerializeField] private CanvasGroup myGroup;

    [SerializeField] private bool fadeOut = false;
    public void HideUI()
    {
        fadeOut = true;
    }


    void Start()
    {
        // Butonlara týklama olaylarýný ekle
        redButton.onClick.AddListener(() => SelectTeam("Red"));
        blueButton.onClick.AddListener(() => SelectTeam("Blue"));
    }

    private void Update()
    {
        Fade();
        //CursorZort();

        if (playerStatus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Takým seçimini yap
    public void SelectTeam(string team)
    {
        selectedTeam = team;

        // Seçilen takýma göre oyuncuyu spawn et
        SpawnPlayer();
    }

    // Oyuncuyu takýmýna göre doðru yerde spawn et
    void SpawnPlayer()
    {
        Vector3 spawnPosition = Vector3.zero;

        // Takýma göre spawn pozisyonunu belirle
        if (selectedTeam == "Red")
        {
            spawnPosition = redTeamSpawnPoint.position;
        }
        else if (selectedTeam == "Blue")
        {
            spawnPosition = blueTeamSpawnPoint.position;
        }

        // PhotonNetwork.Instantiate ile player prefabýný doðru yerde spawn et
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);

        // UI butonlarýný gizleyin veya devre dýþý býrakýn
        redButton.gameObject.SetActive(false);
        blueButton.gameObject.SetActive(false);

        playerStatus = true;

        
    }

    private void CursorZort()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }

    private void Fade()
    {
        if (fadeOut)
        {
            if (myGroup.alpha >= 0)
            {
                myGroup.alpha -= Time.deltaTime;
                if (myGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
