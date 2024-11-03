using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TubeColorChange : MonoBehaviour
{
    public TubeColorSO colorSettings;
    private Renderer tubeRenderer;

    private void Awake()
    {
        tubeRenderer = GetComponent<Renderer>();
    }
    private void OnEnable()
    {
        FlappyBirdController.jumpColor += ChangeTubeColor;
    }
    private void OnDisable()
    {
        FlappyBirdController.jumpColor -= ChangeTubeColor;
    }
    private void ChangeTubeColor()
    {
        if (colorSettings != null && colorSettings.tubeColors.Length > 0)
        {
            Color newColor = colorSettings.tubeColors[UnityEngine.Random.Range(0, colorSettings.tubeColors.Length)];
            tubeRenderer.material.color = newColor;
        }
    }
}