using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManagerScript : MonoBehaviour {
	//[SerializeField] string iOS_gameId = "";
	
	[Tooltip("The tracking code to be used for Android. Example value: UA-XXXX-Y.")]
	public string gameId_android;
	[Tooltip("The tracking code to be used for iOS. Example value: UA-XXXX-Y.")]
	public string gameId_ios;
	[Tooltip("The tracking code to be used for Other. Example value: UA-XXXX-Y.")]
	public string gameId_other;
	
	void Awake(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		Advertisement.Initialize (gameId_android, true);
		#elif UNITY_IPHONE && !UNITY_EDITOR
		Advertisement.Initialize (gameId_ios, true);
		#else
		Advertisement.Initialize (gameId_other, true);
		#endif
	}		
	
	public void ShowAd(string zone = ""){
		#if UNITY_EDITOR
		StartCoroutine(WaitForAd());	// simulate modality
		#endif
		
		if (string.Equals (zone, ""))
			zone = null;
		
		ShowOptions options = new ShowOptions();
		options.resultCallback = AdCallbackHandler;
		
		if (Advertisement.IsReady (zone))
			Advertisement.Show(zone, options);
	}
	
	void AdCallbackHandler(ShowResult result){
		switch (result) {
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");
			break;
		case ShowResult.Skipped:
			Debug.Log ("Ad skipped.");
			break;
		case ShowResult.Failed:
			Debug.Log("Failed");
			break;
		}
	}
	
	IEnumerator WaitForAd(){
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;
		
		while (Advertisement.isShowing)
			yield return null;
		
		Time.timeScale = currentTimeScale;
	}
}

