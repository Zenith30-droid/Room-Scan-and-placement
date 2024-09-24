using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject drone;

    // Start is called before the first frame update
    void Awake()
    {
        drone.SetActive(false);
        
    }

    public void OnInteraction()
    {
        GameManager.Instance.Drone = drone;
        drone.SetActive(true);
    }
}
