using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Provides a fade in and fade out effect for scene transitions.
 * code is a modified version of that found in the unity 4.x stealth tutorial at https://unity3d.com/learn/tutorials/projects/stealth-tutorial-4x-only/screen-fader
 * but has been modified to work with the new UI system.
 */
public class SceneFadeInOut : MonoBehaviour
{
    // Speed that the screen fades to and from black.
    public static float fadeSpeed = 4.0f;

    // Whether or not the scene is still fading in.
    public static bool fade = false;

    // reference to the image to use as a fader
    public static Image fader;

    // the type of fade transition to use
    delegate void TransitionHandler();

    private static TransitionHandler transition;

    // method to be called once the transition is complete
    public delegate void CompleteHandler();

    public static CompleteHandler onComplete;

    void Awake()
    {
        fader = (Image)GetComponent(typeof(Image));

    }


    void Update()
    {
        // If the scene is starting...
        if (fade)
        {
            // run the transition
            transition();
        }
            
    }


    static void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        fader.color = Color.Lerp(fader.color, Color.clear, fadeSpeed * Time.deltaTime);

        // If the texture is almost clear...
        if (fader.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            fader.color = Color.clear;
            fader.enabled = false;

            // we no longer need to fade
            fade = false;

            onComplete();
        }
    }


    static void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        fader.color = Color.Lerp(fader.color, Color.black, fadeSpeed * Time.deltaTime);

        // If the screen is almost black...
        if (fader.color.a >= 0.95f)
        {
            fader.color = Color.black;
            fader.enabled = false;
            fade = false;

            onComplete();
        }
    }


    public static void StartScene(CompleteHandler handler)
    {
        Init();

        // make sure the fader color is correctly set
        fader.color = Color.black;

        // Fade the texture to clear.
        transition = new TransitionHandler(FadeToClear);

        // set the callback to use once complete
        onComplete = handler;
    }

    /**
     * Transition to the specified scene
     */
    public static void EndScene(CompleteHandler handler)
    {
        Init();

        // make sure the fader color is correctly set
        fader.color = Color.clear;

        // Fade the texture to black.
        transition = new TransitionHandler(FadeToBlack);

        // set the callback to use once complete
        onComplete = handler;
    }

    /**
     * Prepare for a new transition
     */
    private static void Init ()
    {
        // re-enable the fader image if disabled
        if (!fader.enabled)
        {
            // Make sure the texture is enabled.
            fader.enabled = true;
        }

        // will be checked in the update loop
        fade = true;
    }

    
}
