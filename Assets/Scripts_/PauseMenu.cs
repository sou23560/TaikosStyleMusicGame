using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menuUI;
    [SerializeField] InputManager inputManager;

    bool isOpen = false;

    void OnEnable()
    {
        inputManager.OnPause += ToggleMenu;
    }

    void OnDisable()
    {
        inputManager.OnPause -= ToggleMenu;
    }

    [SerializeField] NotesGenerator notesGenerator;

    void ToggleMenu()
    {
        isOpen = !isOpen;
        menuUI.SetActive(isOpen);

        if (isOpen)
            notesGenerator.Pause();
        else
            notesGenerator.Resume();
    }
    public void Resume()
    {
        isOpen = false;
        menuUI.SetActive(false);
        Time.timeScale = 1f;
        notesGenerator.Resume();
    }
}