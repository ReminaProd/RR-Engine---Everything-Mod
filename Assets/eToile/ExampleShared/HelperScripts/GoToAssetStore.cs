using UnityEngine;

public class GoToAssetStore : MonoBehaviour
{
    [SerializeField] string url = "https://www.assetstore.unity3d.com/#!/content/67183";

    // Use this for initialization
    public void GoToTheAssetStore()
    {
        Application.OpenURL(url);
    }
}
