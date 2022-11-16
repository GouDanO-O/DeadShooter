using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraControl : MonoBehaviour
{
    private Camera miniMapCamera;

    private void Awake()
    {
        miniMapCamera = GetComponent<Camera>();
    }
    public void PluseButtonClick()
    {
        miniMapCamera.orthographicSize -= 1;
    }
    public void SubstructButtonClick()
    {
        miniMapCamera.orthographicSize += 1;
    }
}
