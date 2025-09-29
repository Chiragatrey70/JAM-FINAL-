using UnityEngine;
using TMPro;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 2f;
    public LayerMask interactionLayer;
    public KeyCode interactionKey = KeyCode.E;

    [Header("UI References")]
    public TextMeshProUGUI promptText;

    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        if (promptText != null) promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, interactionDistance, interactionLayer))
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (promptText != null)
                {
                    promptText.gameObject.SetActive(true);
                    promptText.text = interactable.InteractionPrompt;
                }

                // Check if the player presses E AND an interaction is NOT already open
                if (Input.GetKeyDown(interactionKey) && !UIManager.isInteracting)
                {
                    interactable.Interact(this);
                }
                return;
            }
        }

        if (promptText != null) promptText.gameObject.SetActive(false);
    }
}