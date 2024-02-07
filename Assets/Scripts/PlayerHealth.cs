using Photon.Pun;
using UnityEngine;

public class PlayerHealth : MonoBehaviourPun
{
    public int maxHealth = 100;
    private int currentHealth;
    private GameObject scoreManager;

    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreboardScript"); // Find the object to disable
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount, PhotonView attacker)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            // Check if the local player made the kill
            if (attacker != null && attacker.IsMine)
            {
                // Notify the ScoreManager in the object with the tag "Scoreboard"
                scoreManager.GetComponent<ScoreManager>().PlayerScored(attacker.Owner.ActorNumber);

                // Construct the kill feed message
                string killMessage = $"{attacker.Owner.NickName} eliminated {photonView.Owner.NickName}";
                scoreManager.GetComponent<ScoreManager>().AddKillFeed(killMessage);
            }

            photonView.RPC("RespawnPlayers", RpcTarget.All);
        }
    }


    [PunRPC]
    public void RespawnPlayers()
    {
        FindObjectOfType<PlayerSpawner>().RespawnAllPlayers();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        // Add any other reset logic here
    }

}
