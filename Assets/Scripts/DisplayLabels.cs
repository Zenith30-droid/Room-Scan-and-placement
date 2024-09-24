using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using Unity.Mathematics;
using Unity.VisualScripting;

public class DisplayLabels : MonoBehaviour
{
    public Transform rayStartpoint;
    public float rayLength = 5;
    public MRUKAnchor.SceneLabels labelFilter;
    public TMPro.TextMeshPro debugText;

    public void Update()
    {
        Ray ray = new Ray(rayStartpoint.position, rayStartpoint.forward);

        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        bool hasHit = room.Raycast(ray, rayLength, LabelFilter.Included(labelFilter), out RaycastHit hit, out MRUKAnchor anchor);

        if (!hasHit) return;
        Vector3 hitPoint = hit.point;
        Vector3 hitNormal = hit.normal;

        if (anchor != null)
        {
            var label = anchor.Label;

            debugText.transform.position = hitPoint;
            debugText.transform.rotation = Quaternion.LookRotation(-hitNormal);

            debugText.text = label.ToString();
        }
    }
}
