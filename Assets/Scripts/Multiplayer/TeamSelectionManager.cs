//using UnityEngine;
//using Photon.Pun;
//using ExitGames.Client.Photon;
//using UnityEngine.UI;
//using System.Collections.Generic;


//public enum Team
//{
//    Red,
//    Blue
//}

//public class TeamSelectionManager : MonoBehaviourPunCallbacks
//{
//    // UI Elements for team selection
//    public Button redTeamButton;
//    public Button blueTeamButton;

//    // Spawn points for each team
//    public Transform redTeamSpawnPoint;
//    public Transform blueTeamSpawnPoint;

//    // Player prefab to instantiate
//    public GameObject playerPrefab;

//    void Start()
//    {
//        // Add listeners to the buttons
//        redTeamButton.onClick.AddListener(OnRedTeamSelected);
//        blueTeamButton.onClick.AddListener(OnBlueTeamSelected);
//    }

//    // Called when Red Team is selected
//    public void OnRedTeamSelected()
//    {
//        SetPlayerTeam(PhotonNetwork.LocalPlayer, Team.Red);
//        SpawnPlayer();
//    }

//    // Called when Blue Team is selected
//    public void OnBlueTeamSelected()
//    {
//        SetPlayerTeam(PhotonNetwork.LocalPlayer, Team.Blue);
//        SpawnPlayer();
//    }

//    // Assign team to a player using Custom Properties
//    public void SetPlayerTeam(Player player, Team team)
//    {
//        Hashtable props = new Hashtable
//        {
//            { "team", team }
//        };
//        player.SetCustomProperties(props);
//    }

//    // Retrieve the player's team
//    public Team GetPlayerTeam(Player player)
//    {
//        if (player.CustomProperties.TryGetValue("team", out object team))
//        {
//            return (Team)team;
//        }
//        return Team.Red; // Default team
//    }

//    // Spawn player at the respective team spawn point
//    public void SpawnPlayer()
//    {
//        Team team = GetPlayerTeam(PhotonNetwork.LocalPlayer);

//        Vector3 spawnPosition = team == Team.Red ? redTeamSpawnPoint.position : blueTeamSpawnPoint.position;
//        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
//    }

//    // Example of how to get all players in each team
//    public void GetPlayersInTeams()
//    {
//        List<Player> redTeamPlayers = new List<Player>();
//        List<Player> blueTeamPlayers = new List<Player>();

//        foreach (Player player in PhotonNetwork.PlayerList)
//        {
//            Team team = GetPlayerTeam(player);
//            if (team == Team.Red)
//            {
//                redTeamPlayers.Add(player);
//            }
//            else
//            {
//                blueTeamPlayers.Add(player);
//            }
//        }

//        // You can use the lists of players to display or manage teams
//        Debug.Log("Red Team Players: " + redTeamPlayers.Count);
//        Debug.Log("Blue Team Players: " + blueTeamPlayers.Count);
//    }
//}
