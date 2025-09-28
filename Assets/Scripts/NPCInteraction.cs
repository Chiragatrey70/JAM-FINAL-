using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    // This enum creates a dropdown menu in the Inspector
    public enum InteractionType { TextOnly, ImageOnly, TextAndImage }
    [Header("Interaction Settings")]
    public InteractionType interactionType;

    [Header("UI References")]
    public GameObject interactionPanel;
    public TMP_Text dialogueText;
    public Image displayImage;

    [Header("NPC Content")]
    [TextArea(3, 10)]
    public string npcDialogue;
    public Sprite imageToShow;

    [Header("Interaction Prompt")]
    public GameObject interactionPrompt;

    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionPrompt != null) interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionPrompt != null) interactionPrompt.SetActive(false);
            EndInteraction();
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!interactionPanel.activeInHierarchy) StartInteraction();
        }

        if (interactionPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            EndInteraction();
        }
    }

    public void StartInteraction()
    {
        switch (interactionType)
        {
            case InteractionType.TextOnly:
                if (dialogueText != null)
                {
                    displayImage.gameObject.SetActive(false);
                    dialogueText.gameObject.SetActive(true);
                    dialogueText.text = npcDialogue;
                }
                break;

            case InteractionType.ImageOnly:
                if (displayImage != null)
                {
                    dialogueText.gameObject.SetActive(false);
                    displayImage.gameObject.SetActive(true);
                    displayImage.sprite = imageToShow;
                }
                break;

            case InteractionType.TextAndImage:
                if (dialogueText != null && displayImage != null)
                {
                    dialogueText.gameObject.SetActive(true);
                    displayImage.gameObject.SetActive(true);
                    dialogueText.text = npcDialogue;
                    displayImage.sprite = imageToShow;
                }
                break;
        }

        interactionPanel.SetActive(true);
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
    }

    public void EndInteraction()
    {
        interactionPanel.SetActive(false);
    }
}