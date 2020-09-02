using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    public Material[] materialToPick;
    public Renderer thisRenderer;

    private void Start()
    {
        thisRenderer.material = materialToPick[Random.Range(0, materialToPick.Length)];
    }
}
