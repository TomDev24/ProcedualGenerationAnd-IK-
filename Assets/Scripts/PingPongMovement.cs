using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    private Vector3 aPosition;
    private Vector3 bPosition;

    public float speedOfPingRange = 4f;
    public float offsetRange = 5f;

    private void Start()
    {
        offsetRange = Random.Range(0, offsetRange);
        speedOfPingRange = Random.Range(0, speedOfPingRange);

        aPosition = transform.localPosition;
        bPosition = new Vector3(aPosition.x, aPosition.y + offsetRange, aPosition.z);
    }

    private void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speedOfPingRange, 1);
        transform.localPosition =  transform.TransformVector(Vector3.Lerp(aPosition, bPosition, pingPong)); //localPosition

        //var newPos = Vector3.Lerp(aPosition, bPosition, pingPong);
        //transform.Translate(newPos, Space.Self);  --- no solution
    }
}
