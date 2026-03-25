using UnityEngine;

public class RotateAndBob : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 90f;          // Degrees per second around Y-axis

    [Header("Vertical Movement Settings")]
    public float bobHeight = 0.5f;             // How far up/down to move
    public float bobSpeed = 2f;                // Speed of vertical movement

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        // --- Rotate on local Y-axis ---
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);

        // --- Robotic up/down movement (PingPong) ---
        float newY = startPos.y + Mathf.PingPong(Time.time * bobSpeed, bobHeight);
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
