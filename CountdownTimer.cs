using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [Tooltip("Set countdown time in seconds (30 minutes = 1800 seconds)")]
    public float startTimeInSeconds = 1800f; // Exposed to Inspector
    private float currentTime;
    private bool isRunning = false;

    [Header("UI")]
    public Text timerText;               // Assign Text UI (TextMeshPro supported below)
    public GameObject gameOverPanelUI;   // UI to show when timer reaches 0

    [Header("Player Settings")]
    public GameObject player;            // Player object to disable or kill
    [SerializeField] private GameObject defaultCam;

    void OnEnable()
    {
        // Reset timer and start automatically when this component becomes active
        currentTime = startTimeInSeconds;
        isRunning = true;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isRunning || !player.active) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;
            TimerFinished();
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void TimerFinished()
    {
        // Show Game Over panel
        if (gameOverPanelUI != null)
            gameOverPanelUI.SetActive(true);

        // Play hurt sound
        AudioManager.Instance.PlayRandomSFXs(9);

        // “Kill” player or disable player controls
        if (player != null)
            player.SetActive(false); // (Replace with your player death logic)

        defaultCam.active = true;

        if(Cursor.lockState == CursorLockMode.Locked) { Cursor.lockState = CursorLockMode.None; }

        Debug.Log("Timer finished — GAME OVER");
    }
}
