using System.Collections;
using UnityEngine;
using StarterAssets;

public class PlayerGravity : MonoBehaviour
{
    [Header("Gravity Settings")]
    [Tooltip("The object the Cinemachine camera is following.")]
    public Transform cinemachineTarget;
    public float rotationSpeed = 5f;
    public KeyCode gravityKey = KeyCode.LeftShift;

    // This is the property that was missing
    public bool IsFlipped { get; private set; }

    private bool isFlipping = false;
    private ThirdPersonController _thirdPersonController;

    void Start()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        IsFlipped = false; // Start not flipped
    }

    void Update()
    {
        if (Input.GetKeyDown(gravityKey) && !isFlipping)
        {
            FlipGravity();
        }
    }

    private void FlipGravity()
    {
        isFlipping = true;

        // Update our flipped state
        IsFlipped = !IsFlipped;

        Physics.gravity *= -1;
        _thirdPersonController.Gravity = Physics.gravity.y;
        StartCoroutine(SmoothRotate());
    }

    IEnumerator SmoothRotate()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);

        Quaternion cameraStartRotation = cinemachineTarget.rotation;
        Quaternion cameraEndRotation = cameraStartRotation * Quaternion.Euler(180, 0, 0);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            cinemachineTarget.rotation = Quaternion.Slerp(cameraStartRotation, cameraEndRotation, t);
            yield return null;
        }

        transform.rotation = endRotation;
        cinemachineTarget.rotation = cameraEndRotation;
        isFlipping = false;
    }
}