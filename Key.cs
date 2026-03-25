using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FPSController>())
        {
            gameObject.active = false;
            AudioManager.Instance.PlayRandomSFXs(8);
            UIManager.instance.map2.active = false;
            UIManager.instance.map3.active = true;
        }
    }
}
