using UnityEngine;
using UnityEngine.UI;

/*
 * Generic PopUp message control.
 * The reference parent is just to attach itself to any available canvas (otherwise it will not be shown)
 * The automatic destroy can be also set in seconds.
 */

public class PopUp : MonoBehaviour
{
    Animator _anim;
    Text _msg;
    string _message = "";

	// Use this for initialization
	void Awake ()
    {
        _anim = gameObject.GetComponent<Animator>();
        _msg = transform.Find("Message").GetComponent<Text>();
	}

    /// <summary>Sets the message and shows the PopUp</summary>
    public void SetMessage(string message, Transform parent, float destroy = 0f)
    {
        if (destroy > 0f) Invoke("Close", destroy);         // Hides the message automatically.
        transform.SetParent(parent.root, false);            // Sets the parent.
        _message = message;                                 // Sets the text memory for further updates.
        _msg.text = _message;                               // Sets the text.
        _anim.Play("FadeIn");                               // Starts the animation to be shown.
    }
	
    public void Close()
    {
        _anim.Play("FadeOut");
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}
