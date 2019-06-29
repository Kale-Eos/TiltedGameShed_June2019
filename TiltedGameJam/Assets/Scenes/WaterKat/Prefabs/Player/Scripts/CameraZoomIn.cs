using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomIn : MonoBehaviour
{
    public WaterKat.Scr_Camera cameraScript;
    private void OnCollisionStay(Collision collision)
    {
        cameraScript.InputTransition -= 0.1f;
    }
}