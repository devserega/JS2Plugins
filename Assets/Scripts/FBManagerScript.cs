using UnityEngine;
using System.Collections;

//using System;
using System.Collections.Generic;
//using System.Linq;
using Facebook.Unity;

//using U3DXT.iOS.Social;
//using U3DXT.iOS.Native.Social;

public class FBManagerScript : MonoBehaviour {
	//private static ShareDialogMode shareDialogMode;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Awake(){    
		Debug.Log("FBManagerScript::Awake Called");
		
	}
	
	public void FBInit(){
		if (!FB.IsInitialized) {
			FB.Init(this.OnInitComplete, this.OnHideUnity);
			Debug.Log("FBManagerScript::FBInit Called "+FB.IsInitialized);
		}
	}
	
	public void FBLogin(){
		if (FB.IsInitialized) {
			this.CallFBLogin ();
		}
	}
	
	public void FBLogout(){
		if(FB.IsLoggedIn){
			this.CallFBLogout();
		}
	}
	
	public void FBShare(){
		if (FB.IsLoggedIn) {
			this.StartCoroutine (this.TakeScreenshot ());
		}
	}
	
	private void CallFBLogin(){
		FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email", "user_friends" }, this.HandleResult);
	}
	
	protected void HandleResult(IResult result){
		if (result == null)
		{
			//this.LastResponse = "Null Response\n";
			//LogView.AddLog(this.LastResponse);
			return;
		}
		
		//this.LastResponseTexture = null;
		
		// Some platforms return the empty string instead of null.
		if (!string.IsNullOrEmpty(result.Error))
		{
			//this.Status = "Error - Check log for details";
			//this.LastResponse = "Error Response:\n" + result.Error;
			//LogView.AddLog(result.Error);
		}
		else if (result.Cancelled)
		{
			//this.Status = "Cancelled - Check log for details";
			//this.LastResponse = "Cancelled Response:\n" + result.RawResult;
			//LogView.AddLog(result.RawResult);
		}
		else if (!string.IsNullOrEmpty(result.RawResult))
		{
			//this.Status = "Success - Check log for details";
			//this.LastResponse = "Success Response:\n" + result.RawResult;
			//LogView.AddLog(result.RawResult);
		}
		else
		{
			//this.LastResponse = "Empty Response\n";
			//LogView.AddLog(this.LastResponse);
		}
	}
	
	private void CallFBLoginForPublish()
	{
		// It is generally good behavior to split asking for read and publish
		// permissions rather than ask for them all at once.
		//
		// In your own game, consider postponing this call until the moment
		// you actually need it.
		FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, this.HandleResult);
	}
	
	private void CallFBLogout(){
		FB.LogOut();
		Debug.Log("FBManagerScript::CallFBLogout Called");
	}
	
	private void OnInitComplete(){
		//this.Status = "Success - Check logk for details";
		//this.LastResponse = "Success Response: OnInitComplete Called\n";
		//LogView.AddLog("OnInitComplete Called");
		Debug.Log("OnInitComplete Called");
	}
	
	private void OnHideUnity(bool isGameShown){
		//this.Status = "Success - Check logk for details";
		//this.LastResponse = string.Format("Success Response: OnHideUnity Called {0}\n", isGameShown);
		//LogView.AddLog("Is game shown: " + isGameShown);
		Debug.Log("Is game shown: " + isGameShown);
	}
	
	protected void OnGUI()
	{
		//GUI.enabled = false;
		//GUILayout.Button("btnStartGame");   // this is disabled
		//GUI.enabled = true;
		
		//Debug.Log ("TEST");
	}
	
	private IEnumerator TakeScreenshot()
	{
		yield return new WaitForEndOfFrame();

     	var width = Screen.width;
      	var height = Screen.height;
     	var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

       	// Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        byte[] screenshot = tex.EncodeToPNG();

        var wwwForm = new WWWForm();
        wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
        wwwForm.AddField("message", "Test");
        FB.API("me/photos", HttpMethod.POST, this.HandleResult, wwwForm);
    }
}