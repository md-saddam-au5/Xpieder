
// Attached to the AR marker prefab; handles user taps to display content details and trigger messaging.



using UnityEngine;
using UnityEngine.EventSystems;

public class ARContentMarker : MonoBehaviour, IPointerClickHandler
{
    // AR content data associated with this marker.
    public ARContent contentData;
   
    // Reference to the UI Manager for displaying content and messaging.
    public UIManager uiManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiManager != null && contentData != null)
        {
            // Pass the content text and the user info (ID and username) for messaging.
            uiManager.ShowContentDetails(contentData.content, contentData.userId, contentData.username);
        }
    }
}


