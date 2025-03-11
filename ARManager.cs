

// Handles the AR session and places markers on screen taps.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
    [Header("AR Components")]
    public ARSessionOrigin arOrigin;
    public ARRaycastManager raycastManager;

    // Prefab for testing or temporary marker placement
    public GameObject arContentMarkerPrefab;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        // When the user taps on a plane, place a temporary marker.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Instantiate(arContentMarkerPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}


