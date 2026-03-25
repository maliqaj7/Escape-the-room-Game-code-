using UnityEngine;
using System.Collections;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private AudioManager audios;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSController>())
        {
            AudioManager.Instance.PlayRandomSFXs(10);
            UIManager.instance.FinishGame();
            StartCoroutine(delay());
        }
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(1);

        audios.StopAllSounds();
        audios.enabled = false;
    }
}
