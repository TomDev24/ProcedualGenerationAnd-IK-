using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomSeed : MonoBehaviour
{
    public string randSeedStr = "string";
    public bool useStringForSeed = true;
    public int seed;
    public bool randomizeSeed = false;

    private void Awake() // this code should be called very early
    {
        if (useStringForSeed)
            seed = randSeedStr.GetHashCode(); // 32 bit

        if (randomizeSeed)
            seed = Random.Range(0, 99999);

        Random.InitState(seed); //random num generator now wil work in certain state througout the code 
    }
}
