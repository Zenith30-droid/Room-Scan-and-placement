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
        inputAxis = move.ReadValue<Vector2>();

        Debug.LogError($"Input Data {inputAxis.x}");
    }

    public void SpawnDrone(Transform spawnTransform)
    {
        currentDrone = Instantiate(Drone, spawnTransform.position, spawnTransform.rotation);
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(inputAxis.x, 0, inputAxis.y);
        if (moveDirection.magnitude > 1)
            moveDirection.Normalize();

        moveDirection = currentDrone.transform.TransformDirection(moveDirection);
        moveDirection.y = 0; // Keep the movement horizontal

        if (moveDirection.magnitude > 0.1f)
        {
            //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            //currentDrone.transform.rotation = Quaternion.Slerp(currentDrone.transform.rotation, targetRotation, 15f * Time.deltaTime);
            currentDrone.transform.position += moveDirection * 15 * Time.deltaTime;
        }

        Debug.LogError($"Moving");
    }
}
