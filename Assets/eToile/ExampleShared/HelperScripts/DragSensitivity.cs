using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Adjust automatically the drag sensitivity depending on the screen size.
 * This prevents the drag event from firing while pressing a "movable" button.
 */

public class DragSensitivity : MonoBehaviour {

    public float screenPercent = 1.5f;

	// Use this for initialization
	void Start ()
    {
        EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        es.pixelDragThreshold = Mathf.CeilToInt( (float)Screen.width * (screenPercent / 100f) );
	}
}
