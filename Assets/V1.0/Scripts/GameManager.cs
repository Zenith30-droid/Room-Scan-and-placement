using Oculus.Interaction.Unity.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Experimental

    private PlayerInputActions _actions;

    private InputAction move;

    private GameObject currentDrone;

    private Vector2 inputAxis;
    #endregion

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _actions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = _actions.Player.Move;
        move.Enable();
    }

    public GameObject Drone;

    public void Update()
    {
        MovementDebug();
        Move();
    }

    public void MovementDebug()
    {
        
    }

    public void SpawnDrone(Transform spawnTransform)
    {
        currentDrone = Instantiate(Drone, spawnTransform.position, Quaternion.identity);
    }

    private void Move()
    {
        Vector2 leftStickAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 rightStickAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        Vector3 moveDirection = new Vector3(0, leftStickAxis.y, 0);

        if (Mathf.Abs(leftStickAxis.y) > Mathf.Abs(leftStickAxis.x) && moveDirection.magnitude > 0.1f)
            currentDrone.transform.position += moveDirection * 2 * Time.deltaTime;
        else if (leftStickAxis.x != 0)
            currentDrone.transform.Rotate(0, leftStickAxis.y * 100f * Time.deltaTime, 0);


        moveDirection = new Vector3(rightStickAxis.x, 0, rightStickAxis.y);

        if (moveDirection.magnitude > 0.1f)
        {
            if (Mathf.Abs(rightStickAxis.y) >= Mathf.Abs(rightStickAxis.x))
            {
                currentDrone.transform.position += currentDrone.transform.forward * rightStickAxis.y * 2 * Time.deltaTime;
            }
                
            else
                currentDrone.transform.position += currentDrone.transform.right * rightStickAxis.x * 2 * Time.deltaTime;
        }
        


        //Vector3 moveDirection = new Vector3(leftStickAxis.x, 0, leftStickAxis.y);
        //if (moveDirection.magnitude > 1)
        //    moveDirection.Normalize();

        //moveDirection = currentDrone.transform.TransformDirection(moveDirection);
        //moveDirection.y = 0; // Keep the movement horizontal

        //if (moveDirection.magnitude > 0.1f)
        //{
        //    //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //    //currentDrone.transform.rotation = Quaternion.Slerp(currentDrone.transform.rotation, targetRotation, 15f * Time.deltaTime);
        //    currentDrone.transform.position += moveDirection * 2 * Time.deltaTime;
        //}

        Debug.LogError($"Moving");
    }
}
