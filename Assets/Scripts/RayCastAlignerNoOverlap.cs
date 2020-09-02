using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastAlignerNoOverlap : MonoBehaviour
{
    public GameObject[] objsToPick;
    public float rayDistance = 50f;

    public float overlapBoxCheckSize = 1f;
    public LayerMask objLayer;

    private Collider[] existingObj = new Collider[1];

    private void Start()
    {
        PositionRaycast();
        Destroy(gameObject, 2f);
    }

    private void PositionRaycast()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            Quaternion spawnRot = Quaternion.FromToRotation(Vector3.up, hit.normal);

            var overlapCheckBox = new Vector3(overlapBoxCheckSize, overlapBoxCheckSize, overlapBoxCheckSize);
            int foundObj = Physics.OverlapBoxNonAlloc(hit.point, overlapCheckBox, existingObj, spawnRot, objLayer);

            Debug.Log("Founded obj " + foundObj);

            if (foundObj == 0)
                PickAndSpawn(hit.point, spawnRot);
            else
                Debug.Log("there is already someone");
        }
    }

    private void PickAndSpawn(Vector3 position, Quaternion rot)
    {
        int randIndex = UnityEngine.Random.Range(0, objsToPick.Length);
        GameObject clone = Instantiate(objsToPick[randIndex], position, rot) as GameObject;
    }
}
