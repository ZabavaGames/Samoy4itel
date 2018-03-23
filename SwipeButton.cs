using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Touch;
using System.Runtime.Remoting.Messaging;


namespace MyMobileProject1 {

public class SwipeButton : MonoBehaviour {

	public RusLesson1 RS;
	private LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

	// Use this for initialization
	void Start () {
		RS = GameObject.Find ("SceneControl").GetComponent<RusLesson1>();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void OnEnable() {
	// Hook into the events we need
		LeanTouch.OnFingerSwipe += OnFingerSwipe;
	}
	
	protected virtual void OnDisable() {
	// Unhook the events
		LeanTouch.OnFingerSwipe -= OnFingerSwipe;
	}

	public void OnFingerSwipe(LeanFinger finger) {
		int direction = 0;
		var snapshots = finger.Snapshots;
		Button b, bs = null;
	// Find the position under the current finger
	//	var start = finger.GetStartWorldPosition(1.0f);
	//	var end = finger.GetLastWorldPosition (1.0f);
//		var start = finger.StartScreenPosition;
//		var end = finger.LastScreenPosition;
//		var middle = start + (end - start) / 2;

		foreach (LeanSnapshot ls in snapshots) {
			// Find the collider at this position
			var point = ls.ScreenPosition;
			var hit = Physics2D.OverlapPoint(point, LayerMask); 
 			// Get the rigidbody component
			if (hit != null && (b = GetButton (hit.attachedRigidbody.gameObject)) != null) {
				if (RS.ButtonIsVisible (b)) {
					if (RS.CheckLetterIncorrect (b)) {
						direction = SwipeDirection (finger);
						RS.SwipeButton (b, direction);   
						break;  // зачеркиваем одну кнопку за раз
						}
					else if (bs == null) {
					//	second = true;
						bs = b;
						}
					else if (bs != b) {
				Debug.Log ("две буквы " + bs.name + b.name);
						break;  // отменяем свайп, если вторая кнопка подряд неправильная
						}
					}
				}
//VAR 2
//		var ray = finger.GetStartRay();
//		var hit2 = default(RaycastHit);
//		if (Physics.Raycast(ray, out hit2, float.PositiveInfinity, LayerMask) == true)
//			RS.SwipeButton (hit2.collider.gameObject);
			}

		}

	private Button GetButton (GameObject objekt) {
		Button b;
		if (objekt != null && (b = objekt.GetComponent<Button>()) != null) 
			return b;
		else return null;
	}


	private bool SwipedInThisDirection(Vector2 swipe, Vector2 direction)
	{
		// Find the normalized dot product between the swipe and our desired angle (this will return the acos between the vectors)
		var dot = Vector2.Dot(swipe.normalized, direction.normalized);

		// With 8 directions, each direction takes up 45 degrees (360/8), but we're comparing against dot product, so we need to halve it
		// var limit = Mathf.Cos(22.5f * Mathf.Deg2Rad);
		// Return true if this swipe is within the limit of this direction
		// return dot >= limit;
		return (dot > 0) ? true: false;
	}

	private int SwipeDirection (LeanFinger finger) {
		int dir = -1;
		bool l, r, u, d;
		l = r = u = d = false;
	// Store the swipe delta in a temp variable
		var swipe = finger.SwipeScreenDelta;
		var left  = new Vector2(-1.0f,  0.0f);
		var right = new Vector2( 1.0f,  0.0f);
		var down  = new Vector2( 0.0f, -1.0f);
		var up    = new Vector2( 0.0f,  1.0f);
			
				if (SwipedInThisDirection(swipe, left) == true)
				{
		//			InfoText.text = "You swiped left!";
					l = true;
				}
			
				if (SwipedInThisDirection(swipe, right) == true)
				{
		//			InfoText.text = "You swiped right!";
					r = true;
				}
			
				if (SwipedInThisDirection(swipe, down) == true)
				{
		//			InfoText.text = "You swiped down!";
					d = true;
				}
			
				if (SwipedInThisDirection(swipe, up) == true)
				{
		//			InfoText.text = "You swiped up!";
					u = true;
				}
			if (l && u || r && d) dir = 1;
			if (l && d || r && u) dir = 0;

	return dir;
	}

}
}
