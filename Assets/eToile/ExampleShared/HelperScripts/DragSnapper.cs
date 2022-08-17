using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Snap a scroll rect to its children items. All self contained.
/// Note: Only supports 1 direction
/// </summary>
/// 

[RequireComponent(typeof(ScrollRect))]

public class DragSnapper : UIBehaviour, IEndDragHandler, IBeginDragHandler
{
	public SnapDirection direction;                                                     // The scroll direction.
	public AnimationCurve decelerationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);    // Transition curve (easing).
	public float speed = 0.5f;                                                          // The speed in which we snap.
	public float snapPercentage = 0.2f;	                                                // Percent to snal to next streen.
	public int startItem = 0;                                                           // First item to show.

    ScrollRect scrollRect;
    int itemCount;                                                                      // Item count in our scroll rect (2 minimum)
    float value;	                                                                    // Last valid snapped position.
	int target;		                                                                    // Last target snap position.

	new void Start()
	{
		itemCount = transform.Find("Container").childCount;
        // Set first item view:
        scrollRect = gameObject.GetComponent<ScrollRect>();
		target = startItem;
		scrollRect.normalizedPosition = new Vector2(startItem / (float) itemCount, 0f);
		value = scrollRect.normalizedPosition.x;
        // Set ScrollRect scroll direction:
        if (direction == SnapDirection.Horizontal)
        {
            scrollRect.horizontal = true;
            scrollRect.vertical = false;
        }
        else if (direction == SnapDirection.Vertical)
        {
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
        }
    }

    // The direction we are snapping in:
    public enum SnapDirection
	{
		Horizontal,
		Vertical,
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		StopCoroutine(SnapRect());                                                      // If we are snapping, stop for the next input.
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		StartCoroutine(SnapRect());
	}

	private IEnumerator SnapRect()
	{
		if(itemCount < 2) print ("Item count must be 2 or more");

		float delta = 1f / (itemCount - 1);                                             // Item percentage (just in case they change dynamically).
		float startNormal = direction == SnapDirection.Horizontal ? scrollRect.horizontalNormalizedPosition : scrollRect.verticalNormalizedPosition;
		float tempDelta = (startNormal - value) / delta;	                            // Displacement percentage.

		// Closest item depending on the current position:
		target = Mathf.RoundToInt(value/delta);
		if(tempDelta > snapPercentage) target++;
		else if(tempDelta < -snapPercentage) target--;
		target = Mathf.Clamp(target, 0, itemCount - 1);

		float endNormal = delta * target;                                               // Normalized value for the target item.
		float duration = Mathf.Abs((endNormal - startNormal) / speed);                  // Time needed to get the target depending on speed.
		
		float timer = 0f;
		while (timer < 1f)                                                              // Animate deceleration.
		{
			timer = Mathf.Min(1f, timer + Time.deltaTime / duration);                   // Calculate our timer based on our speed.
			value = Mathf.Lerp(startNormal, endNormal, decelerationCurve.Evaluate(timer));  // Our value based on the animation curve.
			
			if (direction == SnapDirection.Horizontal)                                  // Depending on direction we set our horizontal or vertical position.
				scrollRect.horizontalNormalizedPosition = value;
			else
				scrollRect.verticalNormalizedPosition = value;

			yield return new WaitForEndOfFrame();                                       // Wait until next frame.
		}
	}
}
