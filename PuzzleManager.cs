using UnityEngine;
using System.Collections;
public class PuzzleManager: MonoBehaviour
{
    [SerializeField] private GameObject toggleCodeUI;
    [SerializeField] private GameObject accessCompHintPopup;

    [SerializeField] private bool isDoor1, isDoor2, isDoor3, isDoor4;
    [SerializeField] private DoorController[] _doors;

    private Coroutine _routine;
    private float _timer;
    private bool canTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (isDoor1)
        {
            if (other.GetComponent<FPSController>() && Input.GetKey(KeyCode.E))
            {
                if (_routine != null) { return; }
                _routine = StartCoroutine(ActivateCodeRoutine());
            }
        }
        else if (isDoor2)
        {
            if (other.GetComponent<FPSController>() && Input.GetKey(KeyCode.E) && !canTrigger)
            {
                StopGasLeakingRoutine(3);
            }
            else { _timer = 0f; AudioManager.Instance.sfxSources[5].Stop(); }
        }

        else if (isDoor3)
        {
            if (other.GetComponent<FPSController>() && Input.GetKey(KeyCode.E) && !canTrigger)
            {
                ActivateCryopod(4);
            }
            else { _timer = 0f; AudioManager.Instance.sfxSources[6].Stop(); }
        }
        
        else if (isDoor4)
        {
            if (other.GetComponent<FPSController>() && !canTrigger)
            {
                CollectBattery();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSController>())
        {
            canTrigger = false;
            accessCompHintPopup.active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _routine = null;
        accessCompHintPopup.active = false;
        toggleCodeUI.active = false;
    }

    private IEnumerator ActivateCodeRoutine()
    {
        AudioManager.Instance.PlayRandomSFXs(4);
        yield return new WaitForSeconds(3f);
        toggleCodeUI.active = true;
    }
    
    private void StopGasLeakingRoutine(float _time)
    {
        _timer += Time.fixedDeltaTime;
        Debug.Log(_timer);

        if (!AudioManager.Instance.sfxSources[5].isPlaying) { AudioManager.Instance.PlayRandomSFXs(5); }

        if(_timer >= _time)
        {
            _timer = 0;
            canTrigger = true;
            AudioManager.Instance.sfxSources[5].Stop();

            if (_doors[0].hasUnlocked) { _doors[1].hasUnlocked = true; this.transform.GetChild(0).gameObject.active = false; }

            StartCoroutine(SmallDelay(1, true, 2));
            
        }
    }

    private void ActivateCryopod(float _time)
    {
        _timer += Time.fixedDeltaTime;
        Debug.Log(_timer);

        if (!AudioManager.Instance.sfxSources[6].isPlaying) { AudioManager.Instance.PlayRandomSFXs(6); }

        if (_timer >= _time)
        {
            _timer = 0;
            canTrigger = true;
            AudioManager.Instance.sfxSources[6].Stop();

            if (_doors[0].hasUnlocked && _doors[1].hasUnlocked) 
            { 
                _doors[2].hasUnlocked = true;
                this.GetComponentInChildren<Animator>().SetBool("IsActivated", true);
            }

            StartCoroutine(SmallDelay(1, true, 2));

        }
    }

    private void CollectBattery()
    {
        if(_doors[3].hasUnlocked) { return; }

        if (_doors[0].hasUnlocked && _doors[1].hasUnlocked && _doors[2].hasUnlocked)
        {
            _doors[3].hasUnlocked = true;

            FindAnyObjectByType<FPSController>().transform.Find("FPSHand").GetComponent<Animator>().SetTrigger("TakeAction");
            AudioManager.Instance.PlayRandomSFXs(8);

            canTrigger = true;
            gameObject.GetComponentInChildren<MeshRenderer>().enabled= false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            accessCompHintPopup.active = false;

            Destroy(gameObject, 2f);
        }
    }

    private IEnumerator SmallDelay(float _waitTime, bool _canPlaySFX, int _sfxIndex)
    {
        yield return new WaitForSeconds(_waitTime);
        if (_canPlaySFX) { AudioManager.Instance.PlayRandomSFXs(_sfxIndex); }
        FindAnyObjectByType<FPSController>().transform.Find("FPSHand").GetComponent<Animator>().SetTrigger("TakeAction");
    }
}
