using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SponsorPay;
using SponsorPay.Demo;


/**
 * This is an example on how to use the SponsorPay Unity Plugin.
 * If you attach it to a game object, it will draw a user interface 
 * from which the plugin's methods can be exercised.
 */
public class SponsorPaySDKDemo : AbstractMonoBehaviour {


	// Public Variables

	#if UNITY_ANDROID && !UNITY_EDITOR
	public string appId = "1246"; 
	public string securityToken = "12345678";       
	#else
	public string appId = "1245"; 
	public string securityToken = "888";
	#endif
	public string userId = "test_user_id_1";
	public string customCurrencyName = "TestCoins";
	public string placementId = "1";
	public string currencyId = "";


	public Vector2 windowMargin;
	public Vector2 listMargin;


	// Internal Variables

	SponsorPay.SponsorPayPlugin sponsorPayPlugin;

	
	string coinsLabel = "";
	string actionId = "";
	string mbeOffersStatus = "NO";
	string interstitialAdStatus = "No ads available";
	string credentialsToken = "";

	bool showNotification = true;
	bool toggleOn = true;
	bool mbeShowNotification = false;
	bool mbeToggleOn = false;
	bool mbeVCStoggleOn = false;
	bool mbeVCSshowNotification = false;
	bool overrideCredentials = false;
	
	// Awake: Called when the script instance is being loaded.

	void Awake() 
	{
		
	}
	

	// Start: Use this for initialization

	void Start()
	{
		print ("SponsorPaySDKDemo's Start invoked");

		// get a hold of the SponsorPay plugin instance (SponsorPayPluginMonoBehaviour must be attached to a scene object)
		sponsorPayPlugin = SponsorPayPluginMonoBehaviour.PluginInstance;

		sponsorPayPlugin.EnableLogging(true);
		sponsorPayPlugin.SetLogLevel(SPLogLevel.Debug);


		// Register delegates to be notified of the result of the "get new coins" request
		sponsorPayPlugin.OnSuccessfulCurrencyRequestReceived += new SponsorPay.SuccessfulCurrencyResponseReceivedHandler(OnSuccessfulCurrencyRequestReceived);
		sponsorPayPlugin.OnDeltaOfCoinsRequestFailed += new SponsorPay.ErrorHandler(OnSPDeltaOfCoinsRequestFailed);
		
		// Register delegates to be notified of the result of a BrandEngage request
		sponsorPayPlugin.OnBrandEngageRequestResponseReceived += new SponsorPay.BrandEngageRequestResponseReceivedHandler(OnSPBrandEngageResponseReceived);
		sponsorPayPlugin.OnBrandEngageRequestErrorReceived += new SponsorPay.BrandEngageRequestErrorReceivedHandler(OnSPBrandEngageErrorReceived);
				
		// Register delegates to be notified when a native exception occurs on the plugin
		sponsorPayPlugin.OnNativeExceptionReceived += new SponsorPay.NativeExceptionHandler(OnNativeExceptionReceivedFromSDK);
		sponsorPayPlugin.OnOfferWallResultReceived += new SponsorPay.OfferWallResultHandler(OnOFWResultReceived);
		sponsorPayPlugin.OnBrandEngageResultReceived += new SponsorPay.BrandEngageResultHandler(OnMBEResultReceived);

		//Interstitial delegates
		sponsorPayPlugin.OnInterstitialRequestResponseReceived += new SponsorPay.InterstitialRequestResponseReceivedHandler(OnSPInterstitialResponseReceived);
		sponsorPayPlugin.OnInterstitialRequestErrorReceived += new SponsorPay.InterstitialRequestErrorReceivedHandler(OnSPInterstitialErrorReceived);

		sponsorPayPlugin.OnInterstitialStatusCloseReceived += new SponsorPay.InterstitialStatusCloseHandler(OnSPInterstitialStatusCloseReceived);
		sponsorPayPlugin.OnInterstitialStatusErrorReceived += new SponsorPay.InterstitialStatusErrorHandler(OnSPInterstitialStatusErrorReceived);
		
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("additional_param", "WOOT");
		dictionary.Add("another_one", "ImHERE");

		sponsorPayPlugin.AddParameters(dictionary);
	}

	override protected void DialogClosed()
	{
		coinsLabel = "";
	}

	override protected void DrawImpl()
	{
		float y = GuiRectBigPadding;
		float x = HorizontalMargin;
		
		// ScrollView
		Rect outerScrollRect = new Rect(0, 0, Screen.width, Screen.height);
		Rect innerScrollRect = new Rect(0, 0, Screen.width, ViewHeight);
		scrollPosition = GUI.BeginScrollView (outerScrollRect, scrollPosition, innerScrollRect, false, false);
		
		// General SDK Settings
		GUI.Label(new Rect(x, y, GuiRectWidth, GuiLabelHeight),  "App ID:");
		y += GuiLabelHeight + GuiRectSmallPadding;
		
		appId = TextField("appId", new Rect(x, y, GuiRectWidth, GuiTapTargetHeight), appId, TouchScreenKeyboardType.NumberPad);
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		GUI.Label(new Rect(x, y, GuiRectWidth, GuiLabelHeight),  "User ID: ");
		
		y += GuiLabelHeight + GuiRectSmallPadding;
		userId = TextField("userId", new Rect(x, y, GuiRectWidth, GuiTapTargetHeight), userId, TouchScreenKeyboardType.Default);
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		GUI.Label(new Rect(x, y, GuiRectWidth, GuiLabelHeight),  "Security Token: ");
		
		y += GuiLabelHeight + GuiRectSmallPadding;
		securityToken = TextField("securityToken", new Rect(x, y, GuiRectWidth, GuiTapTargetHeight), securityToken, TouchScreenKeyboardType.Default);
		
		y += GuiTapTargetHeight + GuiRectBigPadding;

		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Start SDK"))
		{
			startSDK();
		}
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		credentialsToken = TextField("credentialsToken", new Rect(x, y, GuiRectWidth, GuiTapTargetHeight), credentialsToken, TouchScreenKeyboardType.Default);
		
		y += GuiTapTargetHeight;
		
		overrideCredentials = GUI.Toggle(new Rect (x, y + GuiPaddingTopToggle, GuiRectWidth, GuiTapTargetHeight), overrideCredentials, "Use credentials token");
		
		y += GuiTapTargetHeight + GuiRectBigPadding;

		/////////////Added on version 7.0.0///////////////////////
		GUI.Label(new Rect(x, y, GuiRectWidth, GuiLabelHeight),  "Placement ID:");
		y += GuiLabelHeight + GuiRectSmallPadding;
		
		placementId = TextField("placementId", new Rect(x, y, GuiRectWidth, GuiTapTargetHeight), placementId, TouchScreenKeyboardType.Default);
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		//////////////////////////////////////////////////////////
		
		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Launch OfferWall")) 
		{
			launchOfferWall();
		}
		
		//////User Segmentation
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "User Segmentation")) 
		{
			launchUserSegmentation();
		}
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Request currency")) 
		{
			requestNewCoins();
			coinsLabel = "Waiting for response from VCS...";
		}
		
		y += GuiTapTargetHeight;
		
		toggleOn = GUI.Toggle(new Rect (x, y + GuiPaddingTopToggle, GuiRectWidth, GuiTapTargetHeight), toggleOn, "Show VCS Notifications");
		
		if (showNotification ^ toggleOn)
		{
			showNotification = toggleOn;
			showVCSNotification(showNotification);
		}
		
		x = HorizontalMargin;
		y += GuiTapTargetHeight + GuiRectSmallPadding;
		
		GUI.Label(new Rect(x, y, GuiRectWidth, GuiLabelHeight),  coinsLabel);
		y += 3 * GuiLabelHeight + GuiRectBigPadding;

		/////////////Added on version 7.0.0///////////////////////
		GUI.Label(new Rect(x, y, GuiRectWidth, GuiLabelHeight),  "Currency ID:");
		y += GuiLabelHeight + GuiRectSmallPadding;
		
		currencyId = TextField("currencyId", new Rect(x, y, GuiRectWidth, GuiTapTargetHeight), currencyId, TouchScreenKeyboardType.Default);
		
		y += GuiTapTargetHeight + GuiRectBigPadding;

		/////////////////////////////////////////////////////////////
		GUI.Label(new Rect(x, y, GuiRectWidth, GuiLabelHeight),  "Custom currency name");
		y += GuiLabelHeight + GuiRectSmallPadding;
		
		customCurrencyName = TextField("customCurrencyName", new Rect(x, y, GuiRectWidth, GuiTapTargetHeight), customCurrencyName, TouchScreenKeyboardType.Default);
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		x = HorizontalMargin;
		
		float buttonWidth = (GuiRectWidth / 3.0f) - (HorizontalPadding);
		
		actionId = TextField("actionId", new Rect(x, y, GuiRectWidth, GuiTapTargetHeight), actionId, TouchScreenKeyboardType.Default);
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Report action completion"))
		{
			reportActionCompletion(actionId);
		}
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Request offers")) 
		{
			requestMBEOffers();
		}
		
		y += GuiTapTargetHeight + GuiRectSmallPadding;
		
		GUI.Label(new Rect(x, y + (GuiLabelHeight/2.0f), buttonWidth, GuiLabelHeight),  "Offers available?");
		x += 3 * buttonWidth - HorizontalMargin;
		
		GUI.Label(new Rect(x, y + (GuiLabelHeight/2.0f), buttonWidth, GuiLabelHeight),  mbeOffersStatus);
		
		x = HorizontalMargin;
		y += GuiTapTargetHeight + GuiRectSmallPadding;
		
		buttonWidth = (GuiRectWidth / 2.0f) - (HorizontalPadding);
		
		mbeToggleOn  = GUI.Toggle(new Rect (x, y + GuiPaddingTopToggle, GuiRectWidth, GuiTapTargetHeight), mbeToggleOn, "Show rewards notification");
		
		if (mbeShowNotification ^ mbeToggleOn)
		{
			mbeShowNotification = mbeToggleOn;
			showMBERewardNotification ( mbeShowNotification );
		}
		
		y += GuiTapTargetHeight + GuiRectSmallPadding;
		
		mbeVCStoggleOn = GUI.Toggle(new Rect (x, y + GuiPaddingTopToggle, GuiRectWidth, GuiTapTargetHeight), mbeVCStoggleOn, "Show VCS Notifications");
		
		if (mbeVCSshowNotification ^ mbeVCStoggleOn)
		{
			mbeVCSshowNotification = mbeVCStoggleOn;
		}
		
		y += GuiTapTargetHeight + GuiRectSmallPadding;
		
		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Start MBE Engagement"))
		{
			startMBE();
		}
		
		buttonWidth = (GuiRectWidth / 3.0f) - (HorizontalPadding);
		
		x = HorizontalMargin;
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Request Interstitial"))
		{
			requestInterstitialAds();
		}
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		if (GUI.Button(new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Show Interstitial"))
		{
			showInterstitialAd();
		}
		
		y += GuiTapTargetHeight + GuiRectBigPadding;
		
		GUI.Label(new Rect(x, y + (GuiLabelHeight/2.0f), buttonWidth, GuiLabelHeight),  interstitialAdStatus);
		
		GUI.EndScrollView();
	}

	// Actions

	// startSDK: Starts the SDK with the values entered in the GUI
	private void startSDK() {
		string token = sponsorPayPlugin.Start (appId, userId, securityToken);
		credentialsToken = string.IsNullOrEmpty (token) ? "" : credentialsToken;

	}

	private void launchUserSegmentation(){
		Application.LoadLevel(1);
	}


	// launchOfferWall: Launches the Mobile Offer Wall for the appId and userId entered in the GUI fields
	private void launchOfferWall() {
		if (overrideCredentials) {
			sponsorPayPlugin.LaunchOfferWall(credentialsToken, customCurrencyName, placementId);
		} else{
			sponsorPayPlugin.LaunchOfferWall(null, customCurrencyName, placementId);
		}
	}


	// requestNewCoins: Sends the request for new coins to SponsorPay's Virtual Currency Server.
	// The result will be delivered asynchronously to the delegates registered on
	// sponsorPayPlugin.OnDeltaOfCoinsReceived or sponsorPayPlugin.OnDeltaOfCoinsRequestFailed
	private void requestNewCoins() {
		if (overrideCredentials) {
#if UNITY_ANDROID && !UNITY_EDITOR
			sponsorPayPlugin.RequestNewCoins(credentialsToken, customCurrencyName, currencyId);
#else
			sponsorPayPlugin.RequestNewCoins(credentialsToken, null, currencyId);
#endif
		} else {
#if UNITY_ANDROID && !UNITY_EDITOR
			sponsorPayPlugin.RequestNewCoins(null, customCurrencyName, currencyId);
#else
			sponsorPayPlugin.RequestNewCoins(null, null, currencyId);
#endif
		}
	}


	// showVCSNotification
	private void showVCSNotification(bool shownNotification) {
		sponsorPayPlugin.ShowVCSNotifications(showNotification);
	}
	

	// reportActionCompletions
	private void reportActionCompletion(string actionId) {
		if(overrideCredentials) {
			sponsorPayPlugin.ReportActionCompletion(credentialsToken, actionId);
		} else {
			sponsorPayPlugin.ReportActionCompletion(actionId);
		}
	}


	// requestMBEOffers: Request Mobile Brand Engagement offers
	private void requestMBEOffers() {
		if (overrideCredentials) {
			sponsorPayPlugin.RequestBrandEngageOffers (credentialsToken, customCurrencyName, mbeVCSshowNotification, currencyId, placementId);
		} else {
			sponsorPayPlugin.RequestBrandEngageOffers (null, customCurrencyName, mbeVCSshowNotification,currencyId, placementId);
		}
	}
	

	// showMBERewardNotification
	private void showMBERewardNotification (bool showRewardNotification) {
		sponsorPayPlugin.ShowBrandEngageRewardNotification (showRewardNotification);
	}
	

	// startMBE: Start the Mobile Brand Engagement
	private void startMBE () {
		sponsorPayPlugin.StartBrandEngage();
	}


	// requestInterstitialAds: Request an interstitial ad
	private void requestInterstitialAds() {
		if (overrideCredentials) {
			sponsorPayPlugin.RequestInterstitialAds(credentialsToken, placementId);
		} else {
			sponsorPayPlugin.RequestInterstitialAds(null, placementId);
		}
	}
	

	// showInterstitialAd: Show an interstitial
	private void showInterstitialAd () {
		sponsorPayPlugin.ShowInterstitialAd();
	}




	// Delegates    


	// OnNativeExceptionReceivedFromSDK:
	public void OnNativeExceptionReceivedFromSDK(string message) {
		dialogTitle = "Error";
		dialogMessage = message;
		showDialog = true;
	}
	

	// OnOFWResultReceived:
	public void OnOFWResultReceived(string message) {
		dialogTitle = "OfferWall return status";
		dialogMessage = message;
		showDialog = true;
	}
		

	// OnMBEResultReceived:
	public void OnMBEResultReceived(string message) {
		mbeOffersStatus = "NO";
		dialogTitle = "BrandEngage return status";
		dialogMessage = message;
		showDialog = true;
	}
	

	// OnSPInterstitialStatusCloseReceived:
	public void OnSPInterstitialStatusCloseReceived(string closeReason) {
		interstitialAdStatus = "No offers";
		dialogTitle = "Interstitial return status";
		dialogMessage = closeReason;
		showDialog = true;
	}


	// OnSPInterstitialStatusErrorReceived:
	public void OnSPInterstitialStatusErrorReceived(string message) {
		interstitialAdStatus = "No offers";
		dialogTitle = "Interstitial return error message";
		dialogMessage = message;
		showDialog = true;
	}
	

	// OnSuccessfulCurrencyRequestReceived: Registered to be called upon reception of the answer for a successful delta of coins request   
	public void OnSuccessfulCurrencyRequestReceived(SuccessfulCurrencyResponse response) {
		coinsLabel = "Delta of coins: " + response.DeltaOfCoins.ToString() +
				". Transaction ID: " + response.LatestTransactionId +
				".\nCurreny ID: " + response.CurrencyId +
				". Currency Name: " + response.CurrencyName;
	}

	// OnSPDeltaOfCoinsRequestFailed: Registered to be called if an error is triggered by the delta of coins request
	public void OnSPDeltaOfCoinsRequestFailed(SponsorPay.RequestError error) {
		// Update the UI with information about the error
		coinsLabel = String.Format("Delta of coins request failed.\n"
			+ "Error Type: {0}\nError Code: {1}\nError Message: {2}",
			error.Type, error.Code, error.Message);
	}
	
	
	// Registered to be called upon reception of the answer for a successful offer request  
	public void OnSPBrandEngageResponseReceived(bool offersAvailable) {
		if (offersAvailable) {
			mbeOffersStatus = "YES";
		} else {
			mbeOffersStatus = "NO";
		}
		
	}

	// Registered to be called if an error is triggered by the offer request
	public void OnSPBrandEngageErrorReceived(string message) {
		mbeOffersStatus = "Error:\n" + message;
	}

	
	// Registered to be called upon reception of the answer for a successful offer request  
	public void OnSPInterstitialResponseReceived(bool adsAvailable) {
		if (adsAvailable) {
			interstitialAdStatus = "Ads are available";
		} else {
			interstitialAdStatus = "No ads";
		}
	}

	// Registered to be called if an error is triggered by the offer request
	public void OnSPInterstitialErrorReceived(string message) {
		interstitialAdStatus = "Error:\n" + message;
	}




}
