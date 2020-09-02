using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastItemAligner : MonoBehaviour
{
    public GameObject objPrefab;
    public float rayDistance = 50f;

    private void Start()
    {
        PositionRaycast();
        Destroy(gameObject, 2f);
    }

    private void PositionRaycast()
    {
        RaycastHit hit;
        //Vector3 down is in worldSpace
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance ))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            GameObject clone = Instantiate(objPrefab, hit.point, spawnRotation);
        }
    }
}
