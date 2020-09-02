using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public Text textToChange;
    // Start is called before the first frame update
    void Start()
    {
        string msg = "This happen a long time ago   \nWorld was undivided  \n";
        StartCoroutine(LetterByletter(msg));
    }

    private IEnumerator LetterByletter(string msg)
    {
        string buff = "";
        textToChange.text = "";
        for (int i = 0; i < msg.Length; i++)
        {
            buff += msg[i];
            textToChange.text += msg[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
