using UnityEngine;
using System.Collections;

public class KeyManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool down = Input.GetKeyDown(KeyCode.Escape);
		bool held = Input.GetKey(KeyCode.Escape);
		bool up = Input.GetKeyUp(KeyCode.Escape);
	
		if (down) {
			Debug.Log("DOWN");
			Application.Quit();
		} else if (held) {
			//Debug.Log("HELD");
		} else if (up) {
			//Debug.Log("UP");
		} else {

		}
	}
}
