using Photon.Pun;
using UnityEngine;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    private Dictionary<int, Transform> playerInitialSpawnPoints = new Dictionary<int, Transform>();

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

        // Assign and store initial spawn point only if it's not already stored
        if (!playerInitialSpawnPoints.ContainsKey(actorNumber))
        {
            Transform initialSpawnPoint = actorNumber % 2 == 0 ? spawnPoint1 : spawnPoint2;
            playerInitialSpawnPoints[actorNumber] = initialSpawnPoint;
        }

        // Spawn player at their initial spawn point
        Transform spawnPoint = playerInitialSpawnPoints[actorNumber];
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }

    public void RespawnAllPlayers()
    {
        foreach (var playerHealth in FindObjectsOfType<PlayerHealth>())
        {
            PhotonView photonView = playerHealth.GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                if (playerInitialSpawnPoints.TryGetValue(photonView.Owner.ActorNumber, out Transform initialSpawnPoint))
                {
                    var characterController = playerHealth.GetComponent<CharacterController>();
                    if (characterController != null)
                    {
                        characterController.enabled = false;
                        playerHealth.transform.position = initialSpawnPoint.position;
                        playerHealth.transform.rotation = initialSpawnPoint.rotation;
                        characterController.enabled = true;
                    }
                    else
                    {
                        var rigidbody = playerHealth.GetComponent<Rigidbody>();
                        if (rigidbody != null)
                        {
                            rigidbody.MovePosition(initialSpawnPoint.position);
                            rigidbody.MoveRotation(initialSpawnPoint.rotation);
                        }
                        else
                        {
                            playerHealth.transform.position = initialSpawnPoint.position;
                            playerHealth.transform.rotation = initialSpawnPoint.rotation;
                        }
                    }

                    playerHealth.ResetHealth();
                }
            }
        }
    }

}
