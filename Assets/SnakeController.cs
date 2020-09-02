using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public SnakeHeadController snakeHead;
    public float speed = 6.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        //if (vertInput != 0)
        //{
        //    movement.z = vertInput * speed;
        //    transform.Translate(movement, Space.World);
        //}

        //if (horInput != 0)
        //{
        //    snakeHead.HorizontalMove(horInput, speed);
        //}
     //------------------------------
        //var rotation = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        //transform.rotation = Quaternion.Euler(rotation);
    }

    public void MoveBody(float zDir)
    {
        Vector3 movement = new Vector3(0, 0, -zDir * speed);
        transform.Translate(movement, Space.Self);
    }
}
