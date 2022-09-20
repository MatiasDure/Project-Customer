using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class InfoDisplayBehaviour : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (GameManager.Manager.IsPaused) return;
            LookAtActiveCamera();
        }

        void LookAtActiveCamera() => gameObject.transform.LookAt(CameraSwitch.Instance.GetCameraActive());

    }
}
