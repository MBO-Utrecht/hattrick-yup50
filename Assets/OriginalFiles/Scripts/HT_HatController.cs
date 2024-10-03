using UnityEngine;
using System.Collections;

public class HT_HatController : MonoBehaviour {

	public Camera cam;

	private float maxWidth;
	private bool canControl = false;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - hatWidth;
	}
	
	// Update is called once per physics timestep
	

	public void ToggleControl (bool toggle) {
		canControl = toggle;
	}
}
