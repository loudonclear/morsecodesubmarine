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
        setGaugeTemp(temperature);
        Debug.Log(temperature);
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
}
