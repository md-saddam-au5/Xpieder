
// Manages the UI for content details and messaging. When a marker is tapped, it displays the content and a button to message the owner.







using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Content Details UI")]
    public GameObject contentDetailsPanel;
    public Text contentDetailsText;
    public Button closeDetailsButton;
    public Button messageButton;

    [Header("Messaging UI")]
    public GameObject messagePanel;
    public InputField messageInput;
    public Button sendMessageButton;
    public Text messageRecipientLabel;

    // Recipient information from the tapped AR content.
    private string currentRecipientUserId;
    private string currentRecipientUsername;

    private MessageManager messageManager;

    void Start()
    {
        if (closeDetailsButton != null)
            closeDetailsButton.onClick.AddListener(HideContentDetails);

        if (messageButton != null)
            messageButton.onClick.AddListener(OpenMessagePanel);

        if (sendMessageButton != null)
            sendMessageButton.onClick.AddListener(SendMessage);

        messageManager = FindObjectOfType<MessageManager>();
    }

    // Displays content details and stores recipient info.
    public void ShowContentDetails(string content, string recipientUserId, string recipientUsername)
    {
        if (contentDetailsText != null)
            contentDetailsText.text = content;
        contentDetailsPanel.SetActive(true);

        currentRecipientUserId = recipientUserId;
        currentRecipientUsername = recipientUsername;
    }

    public void HideContentDetails()
    {
        contentDetailsPanel.SetActive(false);
    }

    // Opens the messaging panel.
    public void OpenMessagePanel()
    {
        messagePanel.SetActive(true);
        messageRecipientLabel.text = "Message to: " + currentRecipientUsername;
    }

    // Closes the messaging panel.
    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
    }

    // Sends the message using MessageManager.
    public void SendMessage()
    {
        string messageText = messageInput.text;
        if (!string.IsNullOrEmpty(messageText) && messageManager != null)
        {
            messageManager.SendMessageToUser(currentRecipientUserId, messageText);
            messageInput.text = "";
            CloseMessagePanel();
        }
    }
}




