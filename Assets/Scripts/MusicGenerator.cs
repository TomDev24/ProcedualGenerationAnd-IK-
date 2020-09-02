using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//first i reCreated a metronome
//Basic random Music with Random.Range for note and for playing or note

public class MusicGenerator : MonoBehaviour
{
    public Oscillator noteGen;

    public float beats = 120;
    private float beatsToSecond;

    private float counter;

    private void Start()
    {
        beatsToSecond = beats / 60;
        counter = 1 / beatsToSecond;
    }
    private void Update()
    {
        counter -= Time.deltaTime;
        if (counter <=0 )
        {
            //Basic random Music with Random.Range for note and for playing or note
            if (Random.Range(0, 2) == 1)
                StartCoroutine(noteGen.PlayNote(Random.Range(0,7)));
            counter = 1 / beatsToSecond;
        }
    }
}
