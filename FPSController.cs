using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    [SerializeField] private Animator FPSAnimator;
    private Rigidbody rb;

    // -------- CAMERA ----------
    public Camera playerCamera;
    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;

    // Crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;
    private float yaw = 0f;
    private float pitch = 0f;
    private Image crosshairObject;

    // -------- CAMERA ZOOM ----------
    public bool enableZoom = true;
    public bool holdToZoom = false;
    public KeyCode zoomKey = KeyCode.Mouse1;
    public float zoomFOV = 30f;
    public float zoomStepTime = 5f;
    private bool isZoomed = false;

    // -------- MOVEMENT ----------
    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;
    private bool isWalking = false;

    // -------- SPRINT ----------
    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeed = 7f;
    public float sprintDuration = 5f;
    public float sprintCooldown = .5f;
    public float sprintFOV = 80f;
    public float sprintFOVStepTime = 10f;

    // Sprint Bar
    public bool useSprintBar = true;
    public bool hideBarWhenFull = true;
    public Image sprintBarBG;
    public Image sprintBar;

    private CanvasGroup sprintBarCG;
    private float sprintRemaining;
    private float sprintCooldownReset;
    private bool isSprinting = false;
    private bool isSprintCooldown = false;

    // -------- JUMP ----------
    public bool enableJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;
    private bool isGrounded = false;

    // -------- CROUCH ----------
    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchHeight = .75f;
    public float speedReduction = .5f;
    private bool isCrouched = false;
    private Vector3 originalScale;

    // -------- HEADBOB ----------
    public bool enableHeadBob = true;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);
    private Vector3 jointOriginalPos;
    private float timer = 0;

    // AUDIO ----------------------------
    private bool walkSFXPlaying = false;
    private bool sprintSFXPlaying = false;

    // --------------------------------------------------------------
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        crosshairObject = GetComponentInChildren<Image>();

        playerCamera.fieldOfView = fov;
        originalScale = transform.localScale;
        jointOriginalPos = joint.localPosition;

        if (!unlimitedSprint)
        {
            sprintRemaining = sprintDuration;
            sprintCooldownReset = sprintCooldown;
        }
    }

    private void Start()
    {
        // Lock cursor
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Setup crosshair
        if (crosshair)
        {
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor;
        }
        else
        {
            crosshairObject.gameObject.SetActive(false);
        }

        // Setup sprint bar
        sprintBarCG = GetComponentInChildren<CanvasGroup>();

        if (useSprintBar)
        {
            sprintBarBG.gameObject.SetActive(true);
            sprintBar.gameObject.SetActive(true);

            if (hideBarWhenFull)
                sprintBarCG.alpha = 0;
        }
        else
        {
            sprintBarBG.gameObject.SetActive(false);
            sprintBar.gameObject.SetActive(false);
        }
    }

    // --------------------------------------------------------------
    private void Update()
    {
        LockUnlockCursorState();
        HandleCameraMovement();
        HandleZoom();
        HandleSprint();
        HandleJump();
        HandleCrouch();
        CheckGround();
        if (enableHeadBob) HeadBob();
    }

    // --------------------------------------------------------------
    void FixedUpdate()
    {
        HandleMovement();
    }

    // --------------------------------------------------------------
    private void HandleCameraMovement()
    {
        if (!cameraCanMove) return;

        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

        float mouseY = Input.GetAxis("Mouse Y");
        pitch += invertCamera ? mouseY * mouseSensitivity : -mouseY * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

        transform.localEulerAngles = new Vector3(0, yaw, 0);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    // --------------------------------------------------------------
    private void HandleZoom()
    {
        if (!enableZoom) return;

        // Toggle zoom
        if (!holdToZoom)
        {
            if (Input.GetKeyDown(zoomKey) && !isSprinting)
                isZoomed = !isZoomed;
        }
        else
        {
            if (Input.GetKeyDown(zoomKey)) isZoomed = true;
            if (Input.GetKeyUp(zoomKey)) isZoomed = false;
        }

        // Smooth zoom
        float targetFOV = isZoomed && !isSprinting ? zoomFOV : fov;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, zoomStepTime * Time.deltaTime);
    }

    // --------------------------------------------------------------
    private void HandleSprint()
    {
        FPSAnimator.SetBool("IsSprinting", isSprinting);

        if (!enableSprint) return;

        if (isSprinting)
        {
            isZoomed = false;
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);

            if (!unlimitedSprint)
            {
                sprintRemaining -= Time.deltaTime;
                if (sprintRemaining <= 0)
                {
                    isSprinting = false;
                    isSprintCooldown = true;
                }
            }
        }
        else
        {
            sprintRemaining = Mathf.Clamp(sprintRemaining + Time.deltaTime, 0, sprintDuration);
        }

        // Cooldown
        if (isSprintCooldown)
        {
            sprintCooldown -= Time.deltaTime;
            if (sprintCooldown <= 0)
                isSprintCooldown = false;
        }
        else
        {
            sprintCooldown = sprintCooldownReset;
        }

        // Update bar
        if (useSprintBar && !unlimitedSprint)
        {
            float pct = sprintRemaining / sprintDuration;
            sprintBar.transform.localScale = new Vector3(pct, 1, 1);

            //Old
            //if (hideBarWhenFull && sprintRemaining == sprintDuration)
            //    sprintBarCG.alpha -= 3 * Time.deltaTime;
            //else
            //    sprintBarCG.alpha += 5 * Time.deltaTime;

            // Show bar when sprinting or when stamina is NOT full

            //New
            if (!hideBarWhenFull || sprintRemaining < sprintDuration)
            {
                sprintBarCG.alpha = Mathf.Lerp(sprintBarCG.alpha, 1f, 8f * Time.deltaTime);
            }
            else
            {
                // stamina is full → hide bar
                sprintBarCG.alpha = Mathf.Lerp(sprintBarCG.alpha, 0f, 8f * Time.deltaTime);
            }

            // Always clamp alpha
            sprintBarCG.alpha = Mathf.Clamp01(sprintBarCG.alpha);
        }

    }

    // --------------------------------------------------------------
    private void HandleJump()
    {
        if (!enableJump) return;

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;

            if (isCrouched && !holdToCrouch)
                CrouchToggle();
        }
    }

    // --------------------------------------------------------------
    private void HandleCrouch()
    {
        if (!enableCrouch) return;

        if (!holdToCrouch)
        {
            if (Input.GetKeyDown(crouchKey))
                CrouchToggle();
        }
        else
        {
            if (Input.GetKeyDown(crouchKey))
            {
                isCrouched = false;
                CrouchToggle();
            }
            else if (Input.GetKeyUp(crouchKey))
            {
                isCrouched = true;
                CrouchToggle();
            }
        }
    }

    private void CrouchToggle()
    {
        if (isCrouched)
        {
            transform.localScale = originalScale;
            walkSpeed /= speedReduction;
            isCrouched = false;
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
            walkSpeed *= speedReduction;
            isCrouched = true;
        }
    }

    // --------------------------------------------------------------
    private void HandleMovement()
    {
        if (!playerCanMove) return;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        isWalking = (input.x != 0 || input.z != 0) && isGrounded;
        FPSAnimator.SetBool("IsWalking", isWalking);

        // ---------------- FOOTSTEP AUDIO ----------------
        if (isWalking && isGrounded)
        {
            if (isSprinting)
            {
                // Sprint footstep
                if (!sprintSFXPlaying)
                {
                    sprintSFXPlaying = true;
                    walkSFXPlaying = false;

                    AudioManager.Instance.StopMovementFXs(false);
                    AudioManager.Instance.PlayMovementSFX(1, true);   // sprint SFX (loop)
                }
            }
            else
            {
                // Walk footstep
                if (!walkSFXPlaying)
                {
                    walkSFXPlaying = true;
                    sprintSFXPlaying = false;

                    AudioManager.Instance.StopMovementFXs(false);
                    AudioManager.Instance.PlayMovementSFX(0, true);   // walk SFX (loop)
                }
            }
        }
        else
        {
            // Stop SFX when not moving
            if (walkSFXPlaying || sprintSFXPlaying)
            {
                walkSFXPlaying = false;
                sprintSFXPlaying = false;

                AudioManager.Instance.StopMovementFXs(false);
            }
        }


        // Sprinting movement
        if (enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0 && !isSprintCooldown)
        {
            isSprinting = true;

            if (isCrouched) CrouchToggle();

            Vector3 target = transform.TransformDirection(input) * sprintSpeed;
            ApplyMovement(target);
        }
        else
        {
            isSprinting = false;

            Vector3 target = transform.TransformDirection(input) * walkSpeed;
            ApplyMovement(target);
        }
    }

    private void ApplyMovement(Vector3 targetVelocity)
    {
        Vector3 velocity = rb.linearVelocity;
        Vector3 change = targetVelocity - velocity;

        change.x = Mathf.Clamp(change.x, -maxVelocityChange, maxVelocityChange);
        change.z = Mathf.Clamp(change.z, -maxVelocityChange, maxVelocityChange);
        change.y = 0;

        rb.AddForce(change, ForceMode.VelocityChange);
        FPSAnimator.SetFloat("Velocity", rb.linearVelocity.magnitude);
    }

    // --------------------------------------------------------------
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        float distance = .75f;

        if (Physics.Raycast(origin, Vector3.down, distance))
            isGrounded = true;
        else
            isGrounded = false;
    }

    // --------------------------------------------------------------
    private void HeadBob()
    {
        if (isWalking)
        {
            if (isSprinting) timer += Time.deltaTime * (bobSpeed + sprintSpeed);
            else if (isCrouched) timer += Time.deltaTime * (bobSpeed * speedReduction);
            else timer += Time.deltaTime * bobSpeed;

            joint.localPosition = new Vector3(
                jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x,
                jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y,
                jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z
            );
        }
        else
        {
            timer = 0;
            joint.localPosition = Vector3.Lerp(joint.localPosition, jointOriginalPos, Time.deltaTime * bobSpeed);
        }
    }

    public bool IsPlayerMoving()
    {
        return (isWalking || isSprinting);
    }

    private void LockUnlockCursorState()
    {
        // Lock cursor
        if (lockCursor && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            lockCursor = false;
        }
        else if (!lockCursor && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            lockCursor = true;
        }
    }
}
