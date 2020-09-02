using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusOn : MonoBehaviour
{
    public Transform target;

    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
