using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject droneSpawnPoint;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    public void OnInteraction()
    {
        GameManager.Instance.SpawnDrone(droneSpawnPoint.transform);
    }
}
