

// Enables users to create and pin new AR content with their current location.







using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;

public class PinContentManager : MonoBehaviour
{
    [Header("UI References")]
    public InputField contentInput;   // Input for content data.
    public Button pinButton;

    FirebaseFirestore db;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        pinButton.onClick.AddListener(PinContent);
    }

    // Pins new AR content by saving it to Firestore.
    public async void PinContent()
    {
        if (!LocationManager.Instance.locationReady)
        {
            Debug.LogWarning("Location not ready. Cannot pin content.");
            return;
        }
       
        string contentText = contentInput.text;
        ARContent newContent = new ARContent
        {
            content = contentText,
            latitude = LocationManager.Instance.latitude,
            longitude = LocationManager.Instance.longitude,
            isPublic = true,
            timestamp = Timestamp.GetCurrentTimestamp(),
            // In a real app, populate these with the current user's details.
            userId = FirebaseManager.Instance.Auth.CurrentUser != null ? FirebaseManager.Instance.Auth.CurrentUser.UserId : "Anonymous",
            username = FirebaseManager.Instance.Auth.CurrentUser != null ? FirebaseManager.Instance.Auth.CurrentUser.DisplayName : "Anonymous"
        };

        DocumentReference docRef = await db.Collection("arContents").AddAsync(newContent);
        Debug.Log("Content pinned with ID: " + docRef.Id);
    }
}


