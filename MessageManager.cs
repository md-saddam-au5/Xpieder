
// Handles sending messages between users using Firebase Firestore.



using UnityEngine;
using Firebase.Firestore;
using Firebase.Auth;

public class MessageManager : MonoBehaviour
{
    FirebaseFirestore db;
    FirebaseAuth auth;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
    }

    // Sends a message to the specified recipient.
    public async void SendMessageToUser(string recipientUserId, string messageText)
    {
        // Create a new message object.
        Message newMessage = new Message
        {
            senderUserId = auth.CurrentUser != null ? auth.CurrentUser.UserId : "Anonymous",
            recipientUserId = recipientUserId,
            messageText = messageText,
            timestamp = Timestamp.GetCurrentTimestamp()
        };

        DocumentReference docRef = await db.Collection("messages").AddAsync(newMessage);
        Debug.Log("Message sent with ID: " + docRef.Id);
    }
}




