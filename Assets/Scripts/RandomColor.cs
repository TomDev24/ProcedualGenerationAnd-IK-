using System.Collections.Generic;
using UnityEngine;

//not so cohesive color
public class RandomColor : MonoBehaviour
{
    public Color colorPicker;
    public MeshRenderer[] renderers;

    private void Start()
    {
        //Color newColor = Random.ColorHSV(); // complete randomness
        //Color newColor = Random.ColorHSV(0.5f, 1f); // hue min and max, which halve of the circle // bluefish half
        //Color newColor = Random.ColorHSV(0f, .5f, 0.5f, 1f); // add saturation // depressing tone // happy saturation.
        Color newColor = Random.ColorHSV(0f, .5f, 0.5f, 1f, 0f, 1f, .5f, 1f);

        // also supports value parameters - which is like brightness
        // also alpha value for tranparency

        ApplyColor(newColor);
    }

    private void ApplyColor(Color newColor)
    {
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        //mat.SetInt("_SurfaceType", 1); -- find anweser
        mat.SetColor("_BaseColor", newColor);

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = mat;
        }
    }
}
