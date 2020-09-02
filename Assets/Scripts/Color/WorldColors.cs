using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldColors : MonoBehaviour
{
    public ColorPallet colorPallete;
    public Camera myCam;
    public float fogMin = 0.05f;
    public float fogMax = .04f;

    // Start is called before the first frame update
    void Start()
    {
        Color skyFogColor = colorPallete.outputColors[Random.Range(0, colorPallete.outputColors.Count)];
        myCam.backgroundColor = skyFogColor;
        RenderSettings.fogColor = skyFogColor; // in URP should be diffrent (probably);
        RenderSettings.fogDensity = Random.Range(fogMin, fogMax);

        //You can use color pallet in postProcessing
    }

}
