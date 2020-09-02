using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//typing improvment -- https://www.typingclub.com/sportal/program-3/116.play
public class CubeController : MonoBehaviour
{
    public bool dos;
    // Start is called before the first frame update
    void Start()
    {
        //transform.DOMoveZ(2, 1); // moving in global
        //Generic way 
        //DOTween.To();
        transform.DOMove(new Vector3(1,1,1), 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(dos)
            DOTween.RewindAll();
    }
}
