using UnityEngine;

public class SendEmail : MonoBehaviour
{
    [SerializeField]
    string email = "jmonsuarez@gmail.com";
    [SerializeField]
    string subject = "[auto] FileManagementAsset - eToile";
    [SerializeField]
    string body = "";

    public void SendAutoEmail()
    {
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }
}
