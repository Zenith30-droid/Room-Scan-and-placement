using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;
using TMPro;

using UnityEngine.XR;

public class HandPointer : MonoBehaviour
{
    public OVRHand righthand;
    public GameObject CurrentTarget { get; private set; }

    [SerializeField] private bool showRaycast = true;
    [SerializeField] private Color highlightColor = Color.red;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float offset;
    [SerializeField] private TextMeshProUGUI data;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject bubble;
    [SerializeField] private int maxAmount;




    private Color _originalColor;
    private Renderer _currentRenderer;

    void Update()
    {
        //MRUK.Instance.GetCurrentRoom().GenerateRandomPositionOnSurface(MRUK.SurfaceType.)

        if (maxAmount > 0)
        {
            
            var spawnPos = MRUK.Instance.GetCurrentRoom().GenerateRandomPositionInRoom(0, true);

            if (spawnPos.HasValue)
            {
                Instantiate(bubble, spawnPos.Value, Quaternion.identity);

                maxAmount--;
            }
                


        }
        

        Debug.Log($" Hand pos {righthand.PointerPose.position.ToString()}") ;
        CheckHandPointer(righthand);

        
    }

    void CheckHandPointer(OVRHand hand)
    {
        if (Physics.Raycast(hand.PointerPose.localPosition, hand.PointerPose.forward, out RaycastHit hit, Mathf.Infinity, targetLayer))
        {
            Debug.LogError("Detected 1");

            if (CurrentTarget != hit.transform.gameObject)
            {
                CurrentTarget = hit.transform.gameObject;
                _currentRenderer = CurrentTarget.GetComponent<Renderer>();
                _originalColor = _currentRenderer.material.color;
                _currentRenderer.material.color = highlightColor;

                target.transform.position = hit.transform.position;

                target.transform.parent = hand.gameObject.transform;

                Debug.LogError("Detected");

                UpdateRayVisualization(hand.PointerPose.localPosition, hit.point, true);
            }

        }
        else
        {
            Debug.LogError($"err 1 {GameManager.Instance.CanMoveDrone}");

            if (CurrentTarget != null && Vector3.Distance(CurrentTarget.transform.position, target.transform.position) > .2f && GameManager.Instance.CanMoveDrone)
            {

                GameManager.Instance.playerController.GestureMovement(CurrentTarget, target);
                //_currentRenderer.material.color = _originalColor;
                //CurrentTarget = null;
                //CurrentTarget.transform.position = Vector3.MoveTowards(CurrentTarget.transform.position, target.transform.position, 4*Time.deltaTime);

                //CurrentTarget.transform.position = Vector3.Lerp(CurrentTarget.transform.position,
                //    target.transform.position, 4 * Time.deltaTime);

            }
            UpdateRayVisualization(hand.PointerPose.localPosition, hand.PointerPose.localPosition + hand.PointerPose.forward * 10, false);
        }

        
    }

    private void UpdateRayVisualization(Vector3 startPosition, Vector3 endPosition, bool hitSomething)
    {
        if (showRaycast && lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
            lineRenderer.material.color = hitSomething ? Color.green : Color.red;
        }
        else if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

}
