using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject objectivePanel;

    void Start()
    {
        // Ensure the correct panels are visible at the start
        mainMenuPanel.SetActive(true);
        objectivePanel.SetActive(false);
    }

    // NEW: This function runs every frame
    private void Update()
    {
        // First, check if the objective panel is currently visible
        if (objectivePanel.activeInHierarchy)
        {
            // Then, check if the player presses the "Enter" key (Return) or the Numpad Enter key
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                // If they do, call the same function that the "Continue" button uses
                StartGame();
            }
        }
    }

    public void ShowObjectiveScreen()
    {
        // This function is called by the "Play" button
        mainMenuPanel.SetActive(false);
        objectivePanel.SetActive(true);
    }

    public void StartGame()
    {
        // This function is called by the "Continue" button and now by the Enter key
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}