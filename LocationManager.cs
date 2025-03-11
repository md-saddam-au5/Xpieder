
// Retrieves and provides the device’s current GPS location using a singleton pattern.


using System.Collections;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance;
    public float latitude { get; private set; }
    public float longitude { get; private set; }
    public bool locationReady { get; private set; } = false;

    private void Awake()
    {
        // Implement a singleton for global access.
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("Location services are not enabled by the user.");
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("Unable to determine device location.");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        locationReady = true;
        Debug.Log("Location ready: " + latitude + ", " + longitude);
    }
}



