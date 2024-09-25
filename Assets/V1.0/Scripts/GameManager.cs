using Oculus.Interaction.Unity.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

   
    private GameObject currentDrone;

   

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

   

     

    public void SpawnDrone(Transform spawnTransform)
    {
        currentDrone = Instantiate(Drone, spawnTransform.position, Quaternion.identity);
    }

   
}
