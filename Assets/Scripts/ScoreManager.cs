using UnityEngine;
using TMPro; // TextMeshPro namespace
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;


public class ScoreManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerOneNameText;
    public TextMeshProUGUI playerTwoNameText;
    public TextMeshProUGUI score;
    public TextMeshProUGUI killFeed;

    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    private string playerOneName = "";
    private string playerTwoName = "";

    void Start()
    {
        UpdatePlayerNames();
        UpdateScoreboardUI();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerNames();
        UpdateScoreboardUI();
    }

    void UpdatePlayerNames()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == 1)
            {
                playerOneName = player.NickName;
            }
            else if (player.ActorNumber == 2)
            {
                playerTwoName = player.NickName;
            }
        }
    }

    public void PlayerScored(int playerNumber)
    {
        if (playerNumber == 1)
        {
            playerOneScore++;
        }
        else if (playerNumber == 2)
        {
            playerTwoScore++;
        }

        UpdateScoreboardUI();
        UpdateScoresOnNetwork();
    }

    void UpdateScoreboardUI()
    {
        if (playerOneNameText != null)
        {
            playerOneNameText.text = $"{playerOneName}";
        }
        if (playerTwoNameText != null)
        {
            playerTwoNameText.text = $"{playerTwoName}";
        }
        if (score != null)
        {
            score.text = $"{playerOneScore} - {playerTwoScore}";
        }
    }

    void UpdateScoresOnNetwork()
    {
        ExitGames.Client.Photon.Hashtable scores = new ExitGames.Client.Photon.Hashtable
        {
            { "PlayerOneScore", playerOneScore },
            { "PlayerTwoScore", playerTwoScore }
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(scores);
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("PlayerOneScore"))
        {
            playerOneScore = (int)propertiesThatChanged["PlayerOneScore"];
        }
        if (propertiesThatChanged.ContainsKey("PlayerTwoScore"))
        {
            playerTwoScore = (int)propertiesThatChanged["PlayerTwoScore"];
        }

        UpdateScoreboardUI();
    }

    // Adjusted AddKillFeed method to start a coroutine
    public void AddKillFeed(string killFeedText)
    {
        killFeed.text = killFeedText;
        // delay 3 sec without coroutine
        Invoke("emptyKillFeed", 3f);        
    }

    // Coroutine that displays the message, waits for 3 seconds, then clears it
    public void emptyKillFeed()
    {
        killFeed.text = "";
    }

}
