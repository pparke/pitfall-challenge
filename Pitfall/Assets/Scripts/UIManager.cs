using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    private static Text scoreText;
    private static Text timeText;

    void Awake ()
    {
        scoreText = (Text)GameObject.Find("Canvas/Score").GetComponent(typeof(Text));
        timeText = (Text)GameObject.Find("Canvas/Time").GetComponent(typeof(Text));
    }

    public static void SetScore (int amt)
    {
        scoreText.text = amt.ToString();
    }

    public static void SetTime (int minutes, int seconds)
    {
        timeText.text = string.Format("{0}:{1}", minutes.ToString("D2"), seconds.ToString("D2"));
    }
}
