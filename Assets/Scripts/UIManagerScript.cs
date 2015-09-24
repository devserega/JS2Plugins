using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame() {
		googleAnalytics.LogScreen("Main Menu");
	}
}
