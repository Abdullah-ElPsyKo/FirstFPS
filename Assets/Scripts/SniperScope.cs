using Photon.Pun;
using UnityEngine;

public class SniperScope : MonoBehaviour
{
    public Camera sniperCamera;
    public GameObject scopeOverlay; // The UI element for the scope overlay
    public float normalFOV = 60f;
    public float zoomedFOV = 15f;
    public KeyCode zoomKey = KeyCode.Mouse1;
    public bool toggleScope = true;

    private bool isScoped = false; // To keep track of the scope state
    private PhotonView photonView;
    private GameObject scoreboard;


    private void Start()
    {
        scoreboard = GameObject.FindGameObjectWithTag("Scoreboard"); // Find the object to disable
        photonView = GetComponentInParent<PhotonView>(); // Get the PhotonView from the player
        scopeOverlay.SetActive(false);
        toggleScope = true;
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.GetKeyDown(zoomKey))
        {
            if (toggleScope)
            {
                // Toggle scope functionality
                isScoped = !isScoped;
                ToggleScope(isScoped);
            }
            else
            {
                // Hold to zoom functionality
                EnableScope(true);
            }
        }
        else if (Input.GetKeyUp(zoomKey) && !toggleScope)
        {
            // If not toggling, disable scope when key is released
            EnableScope(false);
        }
    }

    void ToggleScope(bool isOn)
    {
        sniperCamera.fieldOfView = isOn ? zoomedFOV : normalFOV;
        scopeOverlay.SetActive(isOn);

        if (scoreboard != null)
        {
            scoreboard.SetActive(!isOn);
        }
    }

    void EnableScope(bool isOn)
    {
        sniperCamera.fieldOfView = isOn ? zoomedFOV : normalFOV;
        scopeOverlay.SetActive(isOn);

        if (scoreboard != null)
        {
            scoreboard.SetActive(!isOn);
        }
    }
}
