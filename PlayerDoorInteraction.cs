using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerDoorInteraction : MonoBehaviour
{
    public List<DoorController> doors = new List<DoorController>(); // drag 5 doors here
    public KeyCode interactKey = KeyCode.E;
    public LayerMask playerMask;

    [Header("UI")]
    public GameObject interactHintPopup;     // A UI panel or Text "Press E to Open"
    public GameObject doorUnlockPopup;

    private DoorController currentDoor;

    private void Start()
    {
        interactHintPopup.SetActive(false);
    }

    private void Update()
    {
        if (currentDoor != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (currentDoor.hasUnlocked)
                {
                    currentDoor.OpenDoor();
                }
                
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (currentDoor.isUnlockCodeType)
                {
                    AudioManager.Instance.PlayRandomSFXs(1);
                    if (currentDoor.keyboardInputUI.gameObject.GetComponentInChildren<Text>().text == currentDoor.answerKey.ToString())
                    {
                        AudioManager.Instance.PlayRandomSFXs(2);
                        currentDoor.hasUnlocked = true;
                        currentDoor.canKeyboarduiToggle = false;
                    }
                } 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (DoorController door in doors)
        {
            if (other.gameObject == door.gameObject || other.transform.IsChildOf(door.transform))
            {
                currentDoor = door;
                currentDoor.DetectPlayerSide(playerMask);
                
                if (!currentDoor.hasUnlocked) { interactHintPopup.SetActive(true);}
                else { interactHintPopup.active = false; doorUnlockPopup.active = true;}

                interactHintPopup.GetComponentInChildren<Text>().text = currentDoor.unlockHintTag.ToString();
                if (currentDoor.canKeyboarduiToggle) { currentDoor.keyboardInputUI.active = true; }

                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (DoorController door in doors)
        {
            if (other.gameObject == door.gameObject || other.transform.IsChildOf(door.transform))
            {
                if (currentDoor == door)
                {
                    door.CloseDoor();

                    if (currentDoor.canKeyboarduiToggle) { currentDoor.keyboardInputUI.active = false; }

                    currentDoor = null;

                    if (interactHintPopup.active) { interactHintPopup.SetActive(false); }
                    if (doorUnlockPopup.active) { doorUnlockPopup.SetActive(false); }
                }
                break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        foreach (DoorController door in doors)
        {
            if (other.gameObject == door.gameObject || other.transform.IsChildOf(door.transform))
            {
                currentDoor = door;
                //currentDoor.DetectPlayerSide(playerMask);

                if (!currentDoor.hasUnlocked) { interactHintPopup.SetActive(true); }
                else { interactHintPopup.active = false; doorUnlockPopup.active = true; }

                if (!currentDoor.canKeyboarduiToggle) { currentDoor.keyboardInputUI.active = false; }

                break;
            }
        }
    }
}
