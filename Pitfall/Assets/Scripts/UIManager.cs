using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Manages the displayed UI information and provides methods
 * for setting their values
 */
public class UIManager : MonoBehaviour {

    private static Text scoreText;
    private static Text timeText;
    private static Text livesText;

    void Awake ()
    {
        scoreText = (Text)GameObject.Find("Canvas/Score").GetComponent(typeof(Text));
        timeText = (Text)GameObject.Find("Canvas/Time").GetComponent(typeof(Text));
    }

    /**
     * Set the score text
     */
    public static void SetScore (int amt)
    {
        scoreText.text = amt.ToString();
    }

    /**
     * Set the time text
     */
    public static void SetTime (int minutes, int seconds)
    {
        timeText.text = string.Format("{0}:{1}", minutes.ToString("D2"), seconds.ToString("D2"));
    }
}
