using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveSession : MonoBehaviourPunCallbacks
{
    // Call this to start the process of leaving the room and disconnecting
    public void StartLeavingRoom()
    {
        Debug.Log("Leaving room");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left room, now disconnecting...");
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected, loading scene...");
        SceneManager.LoadScene("Loading");
    }

}
