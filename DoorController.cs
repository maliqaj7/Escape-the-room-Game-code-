using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorPivot;          // The pivot panel that rotates (glass panel)
    public float openAngle = 90f;        // How much to rotate when opening
    public float speed = 3f;             // Rotation speed
    public LayerMask doorLayer;
    public int doorRotateDir = 1;
    public bool playerIsFrontSide;
    public Transform detectPlayerPoint;

    public GameObject keyboardInputUI;
    public string unlockHintTag;
    public string answerKey;
    public bool canKeyboarduiToggle;
    public bool hasUnlocked;
    public bool isUnlockCodeType;

    private bool isOpen = false;
    private Quaternion closedRot;
    private Quaternion openRot;

    private void Start()
    {
        closedRot = doorPivot.localRotation;
        openRot = closedRot * Quaternion.Euler(0, openAngle, 0);
    }

    private void Update()
    {
        if (isOpen)
        {
            //doorPivot.localRotation = Quaternion.Lerp(doorPivot.localRotation, openRot, Time.deltaTime * speed);
            openRot = closedRot * Quaternion.Euler(0, openAngle * doorRotateDir, 0);
            doorPivot.localRotation = Quaternion.Lerp(doorPivot.localRotation, openRot, Time.deltaTime * speed);
        }
        else
        {
            doorPivot.localRotation = Quaternion.Lerp(doorPivot.localRotation, closedRot, Time.deltaTime * speed);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
        AudioManager.Instance.PlayRandomSFXs(0);
    }

    public void CloseDoor()
    {
        if(doorPivot.localRotation == closedRot) { return; }
        isOpen = false;

        if (AudioManager.Instance.sfxSources[0].isPlaying) { return; } //Player has two collider, so it wont trigger two times
        AudioManager.Instance.PlayRandomSFXs(0);
    }

    public void DetectPlayerSide(LayerMask _player)
    {
        if (Physics.Raycast(detectPlayerPoint.position, transform.forward, 20f, _player))
        {
            doorRotateDir = -1;
            Debug.DrawRay(detectPlayerPoint.position, transform.forward, Color.green, 20f);
        }
        else
        {
            doorRotateDir = 1;
        }
    }
}
