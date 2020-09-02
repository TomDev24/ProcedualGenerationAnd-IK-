using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeRotation : MonoBehaviour
{
    public Vector3 randomizeByAxis = Vector3.zero;

    private void Start()
    {
            RandomizeByAxis();
    }

    private void RandomizeByAxis()
    {
        Quaternion randomRot = Quaternion.Euler(
            transform.rotation.eulerAngles.x + Random.Range(-randomizeByAxis.x, randomizeByAxis.x),
            transform.rotation.eulerAngles.y + Random.Range(-randomizeByAxis.y, randomizeByAxis.y),
            transform.rotation.eulerAngles.z + Random.Range(-randomizeByAxis.z, randomizeByAxis.z)
            );
        transform.rotation = randomRot;
    }

    //private void RandomYRotation()
    //{
    //    Quaternion randYRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    //    transform.rotation = randYRotation;
    //}
}
//public Vector3 localPosition;
//Description
//Position of the transform relative to the parent transform.

//If the transform has no parent, it is the same as Transform.position.