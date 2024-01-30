using UnityEngine;
using Photon.Pun;

public class DeactivateOtherCam : MonoBehaviourPun
{
    public Camera playerCamera;

    void Start()
    {
        // Check if the current photon view is not the local player
        if (!photonView.IsMine)
        {
            // If it's not the local player, disable the camera and audio listener
            if (playerCamera != null)
            {
                playerCamera.enabled = false;
            }

            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }
        }
    }
}
