using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // disable panel
        PauseManager.Resume();
    }
    
    public void Pause()
    {
        pauseMenuUI.SetActive(true); // enable panel
        PauseManager.Pause();
    }
    
    public void LoadMenu()
    {
        // Debug.Log("Loading menu..."); // log to console
        PauseManager.Resume();
        Player.DistanceTravelled = 0f; // reset distance travelled
        SceneManager.LoadScene(0); // load menu scene
    }
    
    public void QuitGame()
    {
        // Debug.Log("Quitting game..."); // log to console
        PauseManager.Resume();
        Player.DistanceTravelled = 0f; // reset distance travelled
        Application.Quit(); // quit game
    }
}
