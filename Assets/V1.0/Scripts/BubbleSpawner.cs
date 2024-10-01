using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private int maxBubbleAmount;

    public void Update()
    {
        if (GameManager.Instance.BubbleCount < maxBubbleAmount)
        {
            StartCoroutine(SpawnBubble());
        }
        
    }

    IEnumerator SpawnBubble()
    {
        GameManager.Instance.BubbleCount++;

        yield return new WaitForSeconds(Random.Range(.5f, 1f));

        var spawnPos = MRUK.Instance.GetCurrentRoom().GenerateRandomPositionInRoom(0, true);

        if (spawnPos.HasValue)
            Instantiate(bubble, spawnPos.Value, Quaternion.identity);
    }
}
