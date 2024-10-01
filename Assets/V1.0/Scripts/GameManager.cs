using Oculus.Interaction.Unity.Input;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int bubbleCount;

    public int BubbleCount
    {
        get => bubbleCount;
        set => bubbleCount = value;
    }

    private GameObject currentDrone;

    private bool canMoveDrone;

    public bool CanMoveDrone
    {
        get => canMoveDrone;
        set => canMoveDrone = value;
    }

    public PlayerController playerController;

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

        
    }

   
    public GameObject Drone;


    public void Start()
    {

        //playerController.GestureMovement();
    }
     

    public void SpawnDrone(Transform spawnTransform)
    {
        currentDrone = Instantiate(Drone, spawnTransform.position, Quaternion.identity);
    }

   
}
