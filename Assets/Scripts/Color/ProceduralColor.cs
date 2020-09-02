using UnityEngine;

[System.Serializable]
public class ProceduralColor
{
    public Color color;
    public float maxSaturation = 1;
    public float minSaturation = 0;
    public float maxBrightness = 1;
    public float minBrightness = 0;
    public int variationsToGenerate = 8;
}