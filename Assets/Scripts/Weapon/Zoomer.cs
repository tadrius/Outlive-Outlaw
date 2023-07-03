using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem.Controls;

public class Zoomer : MonoBehaviour
{

    [SerializeField] float zoomSpeed = 1.0f;
    [SerializeField] float fieldOfViewMultiplier = .6f;
    [SerializeField] float rotationSpeedMultiplier = 0.5f;

    float zoomAmount = 0f;
    float fieldOfView;
    float rotationSpeed;

    CinemachineVirtualCamera virtualCamera;
    StarterAssetsInputs input;
    FirstPersonController firstPersonController;

    private void Awake()
    {
        Character player = transform.root.GetComponentInChildren<Character>();

        virtualCamera = player.GetComponentInChildren<CinemachineVirtualCamera>();
        input = player.GetComponent<StarterAssetsInputs>();
        firstPersonController = player.GetComponent<FirstPersonController>();

        fieldOfView = virtualCamera.m_Lens.FieldOfView;
        rotationSpeed = firstPersonController.RotationSpeed;
    }

    private void OnDisable()
    {
        zoomAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (input.zoom)
        {
            zoomAmount = Mathf.Min(1f, zoomAmount + Time.deltaTime * zoomSpeed);
        } else
        {
            zoomAmount = Mathf.Max(0f, zoomAmount - Time.deltaTime * zoomSpeed);
        }
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fieldOfView, fieldOfView * fieldOfViewMultiplier, zoomAmount);
        firstPersonController.RotationSpeed = Mathf.Lerp(rotationSpeed, rotationSpeed * rotationSpeedMultiplier, zoomAmount);
    }
}
