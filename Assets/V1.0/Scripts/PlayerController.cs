using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject wings;

    private bool isMovingForward;
    private bool isMovingHorizontal;

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

        if (Mathf.Abs(rightStickAxis.y) >= Mathf.Abs(rightStickAxis.x))
        {
            isMovingForward = true;
            isMovingHorizontal = false;
            transform.position += transform.forward * rightStickAxis.y * 2 * Time.deltaTime;
        }
        else
        {
            isMovingForward = false;
            isMovingHorizontal = true;
            transform.position += transform.right * rightStickAxis.x * 2 * Time.deltaTime;
        }

        if(Mathf.Abs(rightStickAxis.y) == 0)
            isMovingForward = false;

        if (Mathf.Abs(rightStickAxis.x) == 0)
            isMovingHorizontal = false;



        PassiveRotation(
            new Joystick(leftStickAxis),
            new Joystick(rightStickAxis)
            );

        
    }

    public void PassiveRotation(Joystick LStick, Joystick RStick)
    {
        //if(RStick.yAxis != 0 && Mathf.Abs(wings.transform.eulerAngles.z) < 180f)
        //    wings.transform.Rotate(0, 0, Time.deltaTime * 100f * -Mathf.Sign(RStick.yAxis));

        //Debug.LogError($"Rotating  {Mathf.Abs(wings.transform.eulerAngles.z)}");

        // Rotate the GameObject
        if (isMovingForward)
            wings.transform.Rotate(0, 0, Time.deltaTime * 10f * Mathf.Sign(RStick.yAxis));
        
        if (isMovingHorizontal)
            wings.transform.Rotate(Time.deltaTime * 10f * Mathf.Sign(RStick.xAxis), 0, 0);
        
    }


}
public class Joystick
{
    public float xAxis;
    public float yAxis;

    public Joystick(Vector2 Coordinates)
    {
        xAxis = Coordinates.x;
        yAxis = Coordinates.y;
    }
}

