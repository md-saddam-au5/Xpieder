// Data model for AR content, including the ID of the user who pinned it and their username.





using Firebase.Firestore;

[FirestoreData]
public class ARContent
{
    [FirestoreProperty]
    public string content { get; set; }

    [FirestoreProperty]
    public double latitude { get; set; }

    [FirestoreProperty]
    public double longitude { get; set; }

    [FirestoreProperty]
    public bool isPublic { get; set; }

    [FirestoreProperty]
    public Timestamp timestamp { get; set; }
   
    // Added properties to identify the content owner.
    [FirestoreProperty]
    public string userId { get; set; }

    [FirestoreProperty]
    public string username { get; set; }
}




