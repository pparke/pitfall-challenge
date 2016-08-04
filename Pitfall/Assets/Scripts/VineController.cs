using UnityEngine;
using System.Collections;

/**
 * Responsible for controlling the motion of the vine object
 */
public class VineController : MonoBehaviour {

    private Rigidbody2D rigidbody2d;

    public float amplitude = 70.0f;
    public float period = 8.5f;
    public float phaseShift = 0.0f;
    private float elapsedTime = 0.0f;
    public float y;

    void Awake ()
    {
        rigidbody2d = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
    }

	
	// Update is called once per frame
	void Update () {
        // rotate on the z axis according to a sine function
        elapsedTime = elapsedTime + Time.deltaTime;
        if (elapsedTime >= period) elapsedTime -= period;
        float B = (2 * Mathf.PI) / period;
        float C = -phaseShift / B;
        y = amplitude * Mathf.Sin((B * elapsedTime) + C);
        rigidbody2d.MoveRotation(y);
    }
   

}
