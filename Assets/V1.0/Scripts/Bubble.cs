using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Bubble : MonoBehaviour
{
    [SerializeField] private GameObject Text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<MeshRenderer>().enabled = false;
            Text.SetActive(true);
        }
        
    }
}
