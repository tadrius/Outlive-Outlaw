using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static Weapon;
using UnityEngine.Windows;
using System.Resources;
using StarterAssets;

public class FlashlightPower : MonoBehaviour
{
    [SerializeField] float maxIntensity = 15f;
    [SerializeField] float maxInnerSpot = 17.5f;

    [SerializeField] float maxPower = 100f;
    [SerializeField] float powerDecayRate = 1.0f;

    float power;
    Light flashlight;
    StarterAssetsInputs input;
    bool isOn;

    private enum FlashlightState { On, Off }

    private void Awake()
    {
        input = GetComponentInParent<StarterAssetsInputs>();
        flashlight = GetComponent<Light>();
        power = maxPower;
    }

    private void Start()
    {
        isOn = false;
    }
    void ProcessInput()
    {
        if (input.toggleFlashlight)
        {
            isOn = !isOn;
            input.toggleFlashlight = false;
        }
    }

    private void Update()
    {
        ProcessInput();
        switch (isOn) {
            case true:
                UpdateLightValues(power);
                power = Mathf.Max(0f, power - (powerDecayRate * Time.deltaTime));
                break;
            case false:
                UpdateLightValues(0f);
                break;
        }
    }

    private void UpdateLightValues(float power)
    {
        float powerPercentage = power / maxPower;

        flashlight.intensity = powerPercentage * maxIntensity;
        flashlight.innerSpotAngle = maxInnerSpot * powerPercentage;
    }
 
}
