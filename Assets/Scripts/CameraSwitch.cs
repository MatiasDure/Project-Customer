using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.ComponentModel.Design;
using Assets.Scripts;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;
    [SerializeField] private int indexCamToStart;
    
    int currentIndex;
    int min, max;

    private CinemachineVirtualCamera activeCam;

    public static CameraSwitch Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentIndex = indexCamToStart;
        SettingVirtualCam();
        min = 0;
        max = virtualCameras.Length - 1;
    }

    public void LeftCam()
    {
        if (GameManager.Manager.IsPaused) return;
        ResetCamPriority();
        currentIndex = currentIndex == 0 ? max : currentIndex - 1;
        SettingVirtualCam();
    }

    public void RightCam()
    {
        if (GameManager.Manager.IsPaused) return;
        ResetCamPriority();
        currentIndex = currentIndex == max ? min : currentIndex + 1;
        SettingVirtualCam();
    }

    void SettingVirtualCam()
    {
        virtualCameras[currentIndex].Priority = 1;
        activeCam = virtualCameras[currentIndex];
    }

    void ResetCamPriority() => virtualCameras[currentIndex].Priority = 0;

    public Transform GetCameraActive() => virtualCameras[currentIndex].transform;


}
