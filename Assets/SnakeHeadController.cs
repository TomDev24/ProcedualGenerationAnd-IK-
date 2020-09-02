using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{
    public SnakeController snakeController;

    public Transform cam;
    public float speedSlower = 0.4f;

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput !=0)
        {
            movement.x = horInput * Time.deltaTime * 3f;
            movement.z = vertInput * Time.deltaTime * 3f;

            Quaternion tmp = cam.rotation;

            cam.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);
            movement = cam.TransformDirection(movement);
            cam.rotation = tmp;

            transform.rotation = Quaternion.LookRotation(movement);
            //snakeController.MoveBody(vertInput);
            transform.Translate(movement, Space.World);
        }
    }

    public void HorizontalMove(float input, float speed)
    {
        input = -input; // inverting for local position
        transform.Translate(new Vector3(input * speed * speedSlower,0,0), Space.Self);
    }
}
