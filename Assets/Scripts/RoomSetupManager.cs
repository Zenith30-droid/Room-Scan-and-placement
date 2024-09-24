using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using Unity.VisualScripting;
using UnityEngine;
using Meta;
using System.Threading.Tasks;

public class RoomSetupManager : MonoBehaviour
{
    public void Start()
    {
        Task.Run(async () => await AwakeAsync());
    }
    public async Task AwakeAsync()
    {
        MRUK.Instance.ClearScene();
        await OVRScene.RequestSpaceSetup();
        await MRUK.Instance.LoadSceneFromDevice();




    }
}
