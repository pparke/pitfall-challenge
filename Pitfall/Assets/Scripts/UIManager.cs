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
    private static Image livesImage;
    private static Canvas quitMenu;

    private static bool menuActive = false;

    public static float lifeWidth = 12.0f;


    void Awake()
    {
        scoreText = (Text)GameObject.Find("Canvas/Score").GetComponent(typeof(Text));
        timeText = (Text)GameObject.Find("Canvas/Time").GetComponent(typeof(Text));
        livesImage = (Image)GameObject.Find("Canvas/Lives").GetComponent(typeof(Image));
        quitMenu = (Canvas)GameObject.Find("QuitMenu").GetComponent(typeof(Canvas));
        Debug.Log(quitMenu);
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

    public static void SetLives (int amt)
    {
        livesImage.rectTransform.sizeDelta = new Vector2(amt * lifeWidth, livesImage.rectTransform.sizeDelta.y);
    }

    public static void ToggleExitPanel ()
    {
        menuActive = !menuActive;
        quitMenu.enabled = menuActive;
    }
}
