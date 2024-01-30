using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public TMP_InputField createRoomInput, joinRoomInput;

    public TMP_InputField usernameInput;

    private bool confirmed;

    private void Start()
    {
        confirmed = false;
    }

    // Call this method when the player submits their username
    public void SetUsernameAndConnect()
    {
        if (!string.IsNullOrEmpty(usernameInput.text))
        {
            PhotonNetwork.NickName = usernameInput.text; // Set the username

            // Only connect if not already connected
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings(); // Then connect to Photon
            }
            confirmed = true;
        }
        else
        {
            Debug.LogError("Username is empty!");
            confirmed = false;
        }
    }


    public void CreateRoom()
    {
        if (!confirmed)
        {
            return;
        }
        PhotonNetwork.CreateRoom(createRoomInput.text);
    }

    public void JoinRoom()
    {
        if (!confirmed)
        {
            return;
        }
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    public override void OnJoinedRoom()
    {
        if (!confirmed)
        {
            return;
        }
        PhotonNetwork.LoadLevel("Game");
    }
}
