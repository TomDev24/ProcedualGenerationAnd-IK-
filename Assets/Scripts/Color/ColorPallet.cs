using System.Collections.Generic;
using UnityEngine;

//picking in range between two hues
[CreateAssetMenu(menuName = "ColorToy/Multi Color Pallete")]
public class ColorPallet : ScriptableObject
{
    public ProceduralColor[] inputs;

    public bool addInvertedColor;
    public bool addGoldenRatioColor;

    public List<Color> outputColors;

    public bool pickReducedSublistOfColors;
    public int sublistLength = 16;

    public void Generate()
    {
        outputColors.Clear();
        for (int i = 0; i < inputs.Length; i++)
        {
            BuildListOfColorVariations(inputs[i]);
        }

        if (pickReducedSublistOfColors)
        {
            GenerateRandomSublist(sublistLength);
        }
    }

    public void GenerateRandomSublist(int colorsToPick)
    {
        List<Color> tempColorList = new List<Color>();

        for (int i = 0; i < colorsToPick; i++)
        {
            tempColorList.Add(outputColors[Random.Range(0, outputColors.Count)]);
        }

        outputColors = tempColorList;
    }

    private void BuildListOfColorVariations(ProceduralColor inputColor)
    {
        List<Color> tempColorList = new List<Color>();

        tempColorList = GenerateSaturationVariations(inputColor);
        outputColors.AddRange(tempColorList);

        tempColorList = GenerateValueVariations(inputColor);
        outputColors.AddRange(tempColorList);

        if (addInvertedColor)
        {
            outputColors.Add(InvertColor(tempColorList[Random.Range(0, tempColorList.Count)]));
        }
        if (addGoldenRatioColor)
        {
            outputColors.Add(GoldenRatioColor(inputColor.color));
        }
    }

    private List<Color> GenerateSaturationVariations(ProceduralColor inputColor)
    {
        List<Color> generatedColorList = new List<Color>();
        float saturationIncrement = Random.Range(inputColor.minSaturation, inputColor.maxSaturation) / inputColor.variationsToGenerate;
        for (int i = 0; i < inputColor.variationsToGenerate; i++)
        {
            generatedColorList.Add(Desaturate(inputColor.color, saturationIncrement * i));
        }

        return generatedColorList;
    }

    private List<Color> GenerateValueVariations(ProceduralColor inputColor)
    {
        List<Color> generatedColorList = new List<Color>();
        float valueIncrement = Random.Range(inputColor.minBrightness, inputColor.maxBrightness) / inputColor.variationsToGenerate;

        for (int i = 0; i < inputColor.variationsToGenerate; i++)
        {
            generatedColorList.Add(SetLevel(inputColor.color, i * valueIncrement));
        }

        return generatedColorList;
    }

    private Color InvertColor(Color color)
    {
        Color returnColor = new Color(1f - color.r, 1f - color.g, 1f - color.b);
        return returnColor;
    }

    private Color GoldenRatioColor(Color color)
    {
        float myH, myS, myV;
        Color.RGBToHSV(color, out myH, out myS, out myV);

        float goldH = myH + 0.618033988749895f; // golden ratio conjugate wiki
        goldH = (goldH % 1f);

        Color returnColor = Color.HSVToRGB(goldH, myS, myV);
        return returnColor;

    }

    private Color SetLevel(Color color, float level)
    {
        Color returnColor = new Color(level * color.r, level * color.g, level * color.b);
        return returnColor;
    }

    private Color Desaturate(Color colorRGB, float saturation)
    {
        float myH, myS, myV;
        Color.RGBToHSV(colorRGB, out myH, out myS, out myV);

        Color returnColor = Color.HSVToRGB(myH, myS * saturation, myV);
        return returnColor;
    }

    //Is not used;
    public Color RandomSaturated(Color rgbColor)
    {
        float myH, myS, myV;
        Color.RGBToHSV(rgbColor, out myH, out myS, out myV);
        Color returnColor = Color.HSVToRGB(myH, Random.Range(.5f, 1f), Random.Range(.5f, 1f));
        return returnColor;
    }
}
