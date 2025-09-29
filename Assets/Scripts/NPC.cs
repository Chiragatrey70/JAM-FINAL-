using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Content")]
    [SerializeField] private string _interactionPrompt = "Talk";
    [TextArea(3, 10)]
    public string dialogue;

    public string InteractionPrompt => _interactionPrompt;

    private void Update()
    {
        // If the dialogue panel is open and the player presses E...
        if (dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            CloseDialogue();
        }
    }

    public bool Interact(PlayerInteractor interactor)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogue;
        UIManager.isInteracting = true;
        Debug.Log("Starting dialogue with NPC.");
        return true;
    }

    private void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        UIManager.isInteracting = false;
    }
}