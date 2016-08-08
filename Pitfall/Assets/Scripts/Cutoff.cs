using UnityEngine;
using System.Collections;

/**
 * This script is used to apply a cutoff effect to the attached material
 * with the CutoffEffect shader.  This will result in any part of the sprite
 * below a given y position being fully transparent.
 */
public class Cutoff : MonoBehaviour {

    public Material cutoffMat;

    /**
     * Set the cutoff point for the material, anything below this point will have full transparency
     */
    public void SetCutoff (float y)
    {
        cutoffMat.SetFloat("_Cutoff", y);
    }

    /**
     * Apply post processing effects to the texture
     */
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, cutoffMat);
    }
}
