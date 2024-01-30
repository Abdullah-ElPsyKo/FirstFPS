using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class Pause : MonoBehaviourPunCallbacks
{
    public GameObject pausePanel;
    public GameObject optionsPanel;

    private bool isPaused = false;

    public InputManager inputManager;
    public Weapon weapon;

    private LeaveSession leaveSession;

    void Start()
    {
        leaveSession = FindObjectOfType<LeaveSession>();
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    void Update()
    {
        if (!photonView.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
        TogglePlayerControl(false);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        TogglePlayerControl(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Loading()
    {
        leaveSession.StartLeavingRoom();
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    private void TogglePlayerControl(bool enable)
    {
        if (inputManager != null)
            inputManager.enabled = enable;
        if (weapon != null)
            weapon.enabled = enable;
    }
}
