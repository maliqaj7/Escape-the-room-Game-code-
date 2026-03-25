using UnityEngine;

public class PopupSound : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance.PlayRandomSFXs(3);
    }
}
