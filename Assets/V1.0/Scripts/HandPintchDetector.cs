using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPintchDetector : MonoBehaviour
{
    [SerializeField] private HandPointer handPointer;
    [SerializeField] private AudioClip pinchSound;
    [SerializeField] private AudioClip releaseSound;

    private bool _hasPinched;
    private bool _isIndexFingerPinching;
    private float _pinchStrength;
    private OVRHand.TrackingConfidence _confidence;

    void Update() => CheckPinch(handPointer.righthand);

    void CheckPinch(OVRHand hand)
    {
        _pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        _isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        _confidence = hand.GetFingerConfidence(OVRHand.HandFinger.Index);

        if (handPointer.CurrentTarget)
        {
            Material currentMaterial = handPointer.CurrentTarget.GetComponent<Renderer>().material;
            currentMaterial.SetFloat("_Metallic", _pinchStrength);
        }

        if (!_hasPinched && _isIndexFingerPinching && _confidence == OVRHand.TrackingConfidence.High && handPointer.CurrentTarget)
        {
            _hasPinched = true;
            handPointer.CurrentTarget.GetComponent<AudioSource>().PlayOneShot(pinchSound);
        }
        else if (_hasPinched && !_isIndexFingerPinching)
        {
            _hasPinched = false;
            handPointer.CurrentTarget.GetComponent<AudioSource>().PlayOneShot(releaseSound);
        }

    }
}
