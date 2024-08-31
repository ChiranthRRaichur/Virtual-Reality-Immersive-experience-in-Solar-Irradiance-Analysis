// 2024-07-16 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class LampController : MonoBehaviour
{
    private Light streetLampLight;

    void Start()
    {
        // Assuming 'Lamp' is the direct parent of 'Light'
        // You need to access it appropriately if the hierarchy is deeper
        streetLampLight = GetComponentInChildren<Light>();
        streetLampLight.enabled = false; // Start with the light turned off
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            streetLampLight.enabled = true; // Turn on the light
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            streetLampLight.enabled = false; // Turn off the light
        }
    }
}
