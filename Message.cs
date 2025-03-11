
// Data model for a message stored in Firestore.





using Firebase.Firestore;

[FirestoreData]
public class Message
{
    [FirestoreProperty]
    public string senderUserId { get; set; }

    [FirestoreProperty]
    public string recipientUserId { get; set; }

    [FirestoreProperty]
    public string messageText { get; set; }

    [FirestoreProperty]
    public Timestamp timestamp { get; set; }
}



