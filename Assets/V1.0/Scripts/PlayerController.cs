using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public void Update()
    {
        Move();
    }


    private void Move()
    {

        //Left Stick Control
        //Horizontal X is responsible for rotation in Y axis
        //Vertical Y is responsible for movement in Y axis
        Vector2 leftStickAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (Mathf.Abs(leftStickAxis.y) >= Mathf.Abs(leftStickAxis.x) )
            transform.position += transform.up * leftStickAxis.y * 2 * Time.deltaTime;
        else 
            transform.Rotate(0, leftStickAxis.y * 100f * Time.deltaTime, 0);


        //Right Stick Control
        //Horizontal X is responsible for movement in X axis
        //Vertical Y is responsible for movement in Z axis
        Vector2 rightStickAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (Mathf.Abs(rightStickAxis.y) >= Mathf.Abs(rightStickAxis.x) && Mathf.Abs(rightStickAxis.y) !=0)
            transform.position += transform.forward * rightStickAxis.y * 2 * Time.deltaTime;
        else 
            transform.position += transform.right * rightStickAxis.x * 2 * Time.deltaTime;


        Debug.LogError($"Moving");
    }
}
