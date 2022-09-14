using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    [SerializeField] private int indexCamToStart;
    
    int currentIndex;
    int min, max;

    void Start()
    {
        currentIndex = indexCamToStart;
        SettingVirtualCam();
        min = 0;
        max = virtualCameras.Length - 1;
    }

    public void LeftCam()
    {
        ResetCamPriority();
        currentIndex = currentIndex == 0 ? max : currentIndex - 1;
        SettingVirtualCam();
    }

    public void RightCam()
    {
        ResetCamPriority();
        currentIndex = currentIndex == max ? min : currentIndex + 1;
        SettingVirtualCam();
    }

    void SettingVirtualCam() => virtualCameras[currentIndex].Priority = 1;

    void ResetCamPriority() => virtualCameras[currentIndex].Priority = 0;

}
