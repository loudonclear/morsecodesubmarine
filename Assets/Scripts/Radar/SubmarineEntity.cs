using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineEntity : MonoBehaviour
{
    public static float HULL_HEALTH = 1000.0f;

    public float temperature = targetTemperature;   // FAHRENHEIT
    public float currentHullHealth = HULL_HEALTH;

    // ************** Temperature stuff **********************
    public const float temperatureControlPower = 1.5f;
    public const float targetTemperature = 72.0f;
    public const float temperatureDamageThreshold = 130.0f;
    public const float temperatureDamageMultiplier = 0.1f;

    public SpinningGauge temperatureGauge;
    public const float maxGaugeTemp = 2 * targetTemperature;
    public const float maxTemp = 1.05f * maxGaugeTemp;

    public const float depthThreshold = -2 * SubmarineMovement.depthDelta;
    public const float depthDamageMultiplier = 0.5f;

    private void Start()
    {
        setGaugeTemp(temperature);
    }

    private void setGaugeTemp(float temperature) {
        temperatureGauge.percent = Mathf.Clamp(temperature / maxGaugeTemp, 0, 1);
    }

    private void LateUpdate()
    { 
        applyTemperatureControl();
        applyTemperatureDamage();
        temperature = Mathf.Clamp(temperature, -maxTemp, maxTemp);
        setGaugeTemp(temperature);
        applyDepthDamage();
    }

    private void applyTemperatureControl() {
        // Changing the temperature towards target temperature, but we can only do it so fast
        temperature += Mathf.Clamp(targetTemperature - temperature, 
                                -temperatureControlPower, 
                                temperatureControlPower) 
                                * Time.deltaTime;

        //Debug.Log("APT: " + Mathf.Clamp(targetTemperature - temperature,
        //                        -temperatureControlPower,
        //                        temperatureControlPower));
    }

    private void applyTemperatureDamage() {
        if (temperature >= temperatureDamageThreshold) {
            float hullDamage = temperatureDamageMultiplier * ((int)(temperature - temperatureDamageThreshold / 10.0f) + 1);
            currentHullHealth -= hullDamage * Time.deltaTime;
        }
    }

    private void applyDepthDamage()
    {
        float deltaFromThreshold = this.transform.position.y - depthThreshold;
        if (deltaFromThreshold < 0) {
            float hullDamage = depthDamageMultiplier * Mathf.Abs(deltaFromThreshold);
            currentHullHealth -= hullDamage * Time.deltaTime;
        }
    }
}
