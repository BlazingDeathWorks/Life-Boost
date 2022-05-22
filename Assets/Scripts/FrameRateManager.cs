using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    [SerializeField] private int _frameRate;

    private void Awake()
    {
        Application.targetFrameRate = _frameRate;
    }
}
