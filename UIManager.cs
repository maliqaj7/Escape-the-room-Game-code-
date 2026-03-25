using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject objectivesPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject timerUI;

    [SerializeField] private GameObject FPSPlayer;
    [SerializeField] private GameObject defaultCam;

    public GameObject map2;
    public GameObject map3;

    private bool isObjectivesPanelToggled;

    private void Awake()
    {
        if(instance != null) { Debug.LogError("More than one UIManager"); Destroy(this); }
        else { instance = this; }
    }

    private void Start()
    {
        isObjectivesPanelToggled = false;
    }

    private void Update()
    {
        DisplayObjectivesPanel();
    }

    private void DisplayObjectivesPanel()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!isObjectivesPanelToggled) { objectivesPanel.active = true; isObjectivesPanelToggled = true; Time.timeScale = 0f; }
            else { objectivesPanel.active = false; isObjectivesPanelToggled = false; Time.timeScale = 1f; }
        }
    }

    public void StartGame()
    {
        menuPanel.active = false;
        defaultCam.active = false;
        FPSPlayer.active = true;
        timerUI.active = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        menuPanel.active = false;
        defaultCam.active = false;
        timerUI.active = false;
    }

    public void FinishGame()
    {
        winPanel.active = true;
        Destroy(FPSPlayer);
        defaultCam.active = true;
        if (Cursor.lockState == CursorLockMode.Locked) { Cursor.lockState = CursorLockMode.None; }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
