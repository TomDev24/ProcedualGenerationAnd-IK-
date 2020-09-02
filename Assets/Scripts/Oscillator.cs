using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Oscillation is the repetitive variation, typically in time, 
//of some measure about a central value (often a point of equilibrium) or between two or more different states

//An electronic oscillator is an electronic circuit that produces a periodic, oscillating electronic signal, often a sine wave or a square wave

//A sine wave or sinusoid is a mathematical curve that describes a smooth periodic oscillation.
//A sine wave is a continuous wave. It is named after the function sine, of which it is 
//the graph. It occurs often in pure and applied mathematics, as well as physics, engineering, signal processing
//and many other fields. Its most basic form as a function of time (t) is:


//https://www.youtube.com/watch?v=GqHFGMy_51c
public class Oscillator : MonoBehaviour
{
//Float - 7 digits(32 bit)
//Double-15-16 digits(64 bit)
//Decimal -28-29 significant digits(128 bit)

    public double frequency = 440.0; // setting tone
    private double increment; // distance amount which waves goes each frame its deteriment by frequency
    private double phase; //actual location on the wave
    private double sampling_frequency = 48000.0; // unity audio engine runs by default at this freq //quality of the sound

    [Range(0.1f, 0.6f)]
    public float gain; //actual power/volume of oscillator
    [Range(0.1f, 0.6f)]
    public float volume = 0.1f; //volume higher than 1.0 can kill everuthing

    public float[] frequencies;
    public int thisFreq;

    public bool playTheme = false;
    public Vector2 size;

    private void Start()
    {
        //A major Scale
        frequencies = new float[8];
        frequencies[0] = 440;
        frequencies[1] = 494;
        frequencies[2] = 554;
        frequencies[3] = 587;
        frequencies[4] = 659;
        frequencies[5] = 740;
        frequencies[6] = 831;
        frequencies[7] = 880;
    }

    private void Update()
    {
        //experiment with other waves
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gain = volume;
            frequency = frequencies[thisFreq];
            thisFreq += 1;
            thisFreq = thisFreq % frequencies.Length; // resets freq
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            gain = 0;
        }

        if (!playTheme)
            return;
        gain = volume;
        frequency = Random.Range(90, 90); // frequency = Random.Range(size.x, size.y); give cool effect going when upper limit increasing

    }

    private void OnAudioFilterRead(float[] data, int channels) //requires audioSource componenet // unityFunction
    {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency; //this our x axis step // take a look on trigonometry circle

        for (int i = 0; i < data.Length; i += channels) // chanells are speakers
        {
            phase += increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            if (channels == 2) // plays same thing from two channels
            {
                data[i + 1] = data[i];
            }

            if (phase > (Mathf.PI * 2)) // reset phase after going through circle
            {
                phase = 0.0;
            }
        }
    }

    public IEnumerator PlayNote(int index) //from 0 to 7
    {
        gain = volume;
        frequency = frequencies[index];
        yield return new WaitForSeconds(0.1f);
        gain = 0;
    }
}
