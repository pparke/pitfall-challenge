using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/**
 * Provides methods used by the main menu for starting
 * and exiting the game
 */
public class MenuManager : MonoBehaviour {

    /**
     * Method used by the start game button
     */
    public void StartGameButton ()
    {
        SceneFadeInOut.EndScene(new SceneFadeInOut.CompleteHandler(StartGame));
    }

    /**
     * Method used by the exit button
     */
    public void ExitGameButton ()
    {
        SceneFadeInOut.EndScene(new SceneFadeInOut.CompleteHandler(ExitGame));
    }

    /**
     * Start the main gameplay scene
     */
    void StartGame()
    {
        SceneManager.LoadScene("Play");
    }

    /**
     * Quit the game
     */
    void ExitGame()
    {
        Application.Quit();
    }
}
