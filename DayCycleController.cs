// 2024-07-12 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System.Collections;

public class DayCycleController : MonoBehaviour
{
    public Light directionalLight; // Drag your directional light here in the inspector
    public float transitionDuration = 10f; // Duration over which the change should occur

    void Start()
    {
        // Start the coroutine to change light properties over time
        StartCoroutine(ChangeLightProperties());
    }

    IEnumerator ChangeLightProperties()
    {
        float currentTime = 0f;

        // Initial properties of the light at 'morning'
        float initialIntensity = 0.1f;
        Color initialColor = Color.red;

        // Target properties of the light at 'evening'
        float targetIntensity = 1f;
        Color targetColor = Color.yellow;

        while (currentTime < transitionDuration)
        {
            // Calculate the percentage of the transition
            float t = currentTime / transitionDuration;

            // Smoothly interpolate the light's intensity and color
            directionalLight.intensity = Mathf.Lerp(initialIntensity, targetIntensity, t);
            directionalLight.color = Color.Lerp(initialColor, targetColor, t);

            // Increment the time
            currentTime += Time.deltaTime;

            // Wait for a frame
            yield return null;
        }

        // Ensure the target properties are set at the end of the transition
        directionalLight.intensity = targetIntensity;
        directionalLight.color = targetColor;
    }
}

