
// Loads AR content from Firebase Firestore, checks for proximity, and instantiates AR markers in the AR world.



using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using System.Threading.Tasks;

public class ContentManager : MonoBehaviour
{
    FirebaseFirestore db;
    public List<ARContent> arContents = new List<ARContent>();

    [Header("References")]
    // Prefab to instantiate for each AR marker.
    public GameObject arContentMarkerPrefab;
    // Reference to the UI Manager for marker interactions.
    public UIManager uiManager;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        LoadARContent();
    }

    // Loads AR content from Firestore.
    public async void LoadARContent()
    {
        QuerySnapshot snapshot = await db.Collection("arContents").GetSnapshotAsync();
        foreach (DocumentSnapshot document in snapshot.Documents)
        {
            ARContent content = document.ConvertTo<ARContent>();
            arContents.Add(content);
        }
        InstantiateNearbyContent();
    }

    // Instantiates markers for nearby content.
    void InstantiateNearbyContent()
    {
        foreach (ARContent content in arContents)
        {
            if (IsWithinProximity(content))
            {
                Vector3 worldPosition = ConvertGPSToWorld(content.latitude, content.longitude);
                GameObject marker = Instantiate(arContentMarkerPrefab, worldPosition, Quaternion.identity);

                ARContentMarker markerScript = marker.GetComponent<ARContentMarker>();
                if (markerScript != null)
                {
                    markerScript.contentData = content;
                    markerScript.uiManager = uiManager;
                }
            }
        }
    }

    // Simple proximity check (e.g., within 50 meters).
    bool IsWithinProximity(ARContent content)
    {
        float distance = CalculateDistance(
            LocationManager.Instance.latitude, LocationManager.Instance.longitude,
            content.latitude, content.longitude);
        return distance < 50f;
    }

    // Converts GPS coordinates to an approximate AR world position.
    Vector3 ConvertGPSToWorld(double lat, double lon)
    {
        // Approximate conversion using differences in latitude/longitude.
        float x = (float)(lat - LocationManager.Instance.latitude) * 111000f; // Meters per degree latitude.
        float z = (float)(lon - LocationManager.Instance.longitude) * 111000f; // Meters per degree longitude.
        return new Vector3(x, 0, z);
    }

    // A simple method to calculate distance.
    float CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        Vector3 pos1 = new Vector3((float)lat1, 0, (float)lon1);
        Vector3 pos2 = new Vector3((float)lat2, 0, (float)lon2);
        return Vector3.Distance(pos1, pos2);
    }
}


