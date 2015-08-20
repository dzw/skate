using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Globalization;
using SponsorPay;
using SponsorPay.Demo;

/**
 * This is an example on how to use the SponsorPay Unity Plugin.
 * If you attach it to a game object, it will draw a user interface 
 * from which the plugin's methods can be exercised.
 */
public class UserSegmentationSponsorPaySDK : AbstractMonoBehaviour
{
	
		// Private Variables
	
		private string age = string.Empty;
		private string birthdate = string.Empty;
		private string gender = string.Empty;
		private string sexualOrientation = string.Empty;
		private string ethnicity = string.Empty;
		private string location = string.Empty;
		private string maritalStatus = string.Empty;
		private string numberOfChildrens = string.Empty;
		private string annualHouseholdIncome = string.Empty;
		private string education = string.Empty;
		private string zipcode = string.Empty;
		private string interests = string.Empty;
		private string iap = string.Empty;
		private string iapAmount = string.Empty;
		private string numberOfSessions = string.Empty;
		private string psTime = string.Empty;
		private string lastSession = string.Empty;
		private string connection = string.Empty;
		private string device = string.Empty;
		private string appVersion = string.Empty;

		private Vector2 windowMargin;
		private Vector2 listMargin;

		SPUser SPUser;
#if UNITY_IPHONE
		private bool locationToggleOn;
#endif

		// Awake: Called when the script instance is being loaded.
	
		void Awake ()
		{
		
		}
	
	
		// Start: Use this for initialization
	
		void Start ()
		{
				print ("UserSegmentationSponsorPaySDK's Start invoked");

				SPUser = SponsorPayPluginMonoBehaviour.PluginInstance.SPUser;
				ViewHeight = 3640;

#if UNITY_IPHONE
				Input.location.Start();
#endif

				GetSPUserValues();
		}
		
		override protected void DrawImpl()
		{

				float y = GuiRectBigPadding;
				float x = HorizontalMargin;
		
				// ScrollView
				Rect outerScrollRect = new Rect (0, 0, Screen.width, Screen.height);
				Rect innerScrollRect = new Rect (0, 0, Screen.width, ViewHeight);
				scrollPosition = GUI.BeginScrollView (outerScrollRect, scrollPosition, innerScrollRect, false, false);

				// General SDK Settings
				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Age:");
				y += GuiLabelHeight + GuiRectSmallPadding;
		
				age = TextField ("age", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), age, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;
				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Birthday(yyyy/MM/dd): ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				birthdate = TextField ("birthdate", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), birthdate, TouchScreenKeyboardType.Default);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;
				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Gender: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				gender = TextField ("gender", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), gender, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Sexual Orientation: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				sexualOrientation = TextField ("sexualOrientation", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), sexualOrientation, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Ethnicity: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				ethnicity = TextField ("ethnicity", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), ethnicity, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "latitude:value, longtitude:value ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				location = TextField ("location", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), location, TouchScreenKeyboardType.Default);

#if UNITY_IPHONE
				y += GuiTapTargetHeight + GuiRectSmallPadding;
				locationToggleOn = GUI.Toggle(new Rect (x, y + GuiPaddingTopToggle, GuiRectWidth, GuiTapTargetHeight), locationToggleOn, "Update location");
#endif

				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Marital Status: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				maritalStatus = TextField ("maritalStatus", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), maritalStatus, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Children: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				numberOfChildrens = TextField ("children", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), numberOfChildrens, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Annual Household Income: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				annualHouseholdIncome = TextField ("annualHouseholdIncome", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), annualHouseholdIncome, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Education: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				education = TextField ("education", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), education, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Zipcode: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				zipcode = TextField ("zipcode", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), zipcode, TouchScreenKeyboardType.Default);

				y += GuiTapTargetHeight + GuiRectBigPadding;
 
				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Interests: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				interests = TextField ("interests", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), interests, TouchScreenKeyboardType.Default);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "IAP: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				iap = TextField ("iap", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), iap, TouchScreenKeyboardType.Default);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "IAP amount: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				iapAmount = TextField ("iap_amount", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), iapAmount, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Number of Sessions: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				numberOfSessions = TextField ("numberOfSessions", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), numberOfSessions, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "PS time: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				psTime = TextField ("ps_time", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), psTime, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Last session: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				lastSession = TextField ("last_session", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), lastSession, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Connection: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				connection = TextField ("connection", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), connection, TouchScreenKeyboardType.NumberPad);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;
 
				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "Device: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				device = TextField ("device", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), device, TouchScreenKeyboardType.Default);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				GUI.Label (new Rect (x, y, GuiRectWidth, GuiLabelHeight), "App version: ");
		
				y += GuiLabelHeight + GuiRectSmallPadding;
				appVersion = TextField ("app_version", new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), appVersion, TouchScreenKeyboardType.Default);
		
				y += GuiTapTargetHeight + GuiRectBigPadding;

				if (GUI.Button (new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Submit")) {
						SetProvidedValues ();
						GotoInitialScene ();
				}

				y += GuiTapTargetHeight + GuiRectBigPadding;
				
				if (GUI.Button (new Rect (x, y, GuiRectWidth, GuiTapTargetHeight), "Clear")) {
						ResetSPUser ();
				}

				GUI.EndScrollView ();
		}
	
		override protected void DialogClosed()
		{
			// do nothing
		}

		
		private void GetSPUserValues()
		{
			setValue(out age, SPUser.GetAge());
			setValue(out birthdate, SPUser.GetBirthdate());
			setValue(out location, SPUser.GetLocation());
			setValue(out numberOfChildrens, SPUser.GetNumberOfChildrens());
			setValue(out annualHouseholdIncome, SPUser.GetAnnualHouseholdIncome());
			setValue(out zipcode, SPUser.GetZipcode());
			setValue(out interests, SPUser.GetInterests());
			setValue(out iapAmount, SPUser.GetIapAmount());
			setValue(out numberOfSessions, SPUser.GetNumberOfSessions());
			setValue(out psTime, SPUser.GetPsTime());
			setValue(out lastSession, SPUser.GetLastSession());
			setValue(out device, SPUser.GetDevice());
			setValue(out appVersion, SPUser.GetAppVersion());

			setValue(out iap, SPUser.GetIap());
			//enums
			setValue(out gender, SPUser.GetGender());;
			setValue(out sexualOrientation, SPUser.GetSexualOrientation());
			setValue(out ethnicity, SPUser.GetEthnicity() );
			setValue(out maritalStatus, SPUser.GetMaritalStatus() );
			setValue(out education, SPUser.GetEducation() );
			setValue(out connection, SPUser.GetConnection() );
		}

		private void setValue(out string var, object value)
		{
			if (value != null)
			{
				if (value is SPLocation)
				{
					SPLocation loc = (SPLocation)value;
					var = "Latitude: "+ loc.Lat +", Longitude:" + loc.Long;
				} else if (value is string[]) {
					var = string.Join(", ", (string[])value);
				} else if (value is DateTime){
					var = ((DateTime)value).ToString("yyyy/MM/dd");
				} else {
					var = (string)Convert.ChangeType(value, TypeCode.String);
				}
			} else {
				var = string.Empty;
			}
		}

		private void ResetSPUser ()
		{
			age = string.Empty;
			birthdate = string.Empty;
			gender = string.Empty;
			sexualOrientation = string.Empty;
			ethnicity = string.Empty;
			location = string.Empty;
			maritalStatus = string.Empty;
		 	numberOfChildrens = string.Empty;
			annualHouseholdIncome = string.Empty;
		    education = string.Empty;
			zipcode = string.Empty;
			interests = string.Empty;
			iap = string.Empty;
			iapAmount = string.Empty;
			numberOfSessions = string.Empty;
			psTime = string.Empty;
			lastSession = string.Empty;
			connection = string.Empty;
			device = string.Empty;
			appVersion = string.Empty;
		}

		private void SetProvidedValues ()
		{
				getAgeAndSetSPUser ();
				getBirthdateAndSetSPUser ();
#if UNITY_IPHONE
				if (locationToggleOn) {
					getLocationAndSetSPUser ();
				}
#else
				getLocationAndSetSPUser ();
#endif
				getNumberOfChildrensAndSetSPUser ();
				getAnnualHouseholdIncomeAndSetSPUser ();
				getZipcodeAndSetSPUser ();
				getInterestsAndSetSPUser ();
				getIapAmountAndSetSPUser ();
				getNumberOfSessionsAndSetSPUser ();
				getPsTimeAndSetSPUser ();
				getLastSessionAndSetSPUser ();
				getDeviceAndSetSPUser ();
				getAppVersionAndSetSPUser ();


				hasIapAndSetSPUser();

				//enums
				getGenderAndSetSPUser();
				getSexualOrientationAndSetSPUser();
				getEthnicityAndSetSPUser();
				getMaritalStatusAndSetSPUser();
				getEducationAndSetSPUser();
				getConnectionAndSetSPUser();

		}

		private void GotoInitialScene ()
		{
				Application.LoadLevel (0);
		}

		private Boolean checkIfValueHasBeenSet (string value)
		{
				return !string.IsNullOrEmpty (value);
		}

		private void getAgeAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (age)) {
						SPUser.SetAge (Int32.Parse (age));
				}
		}
	
		private void getBirthdateAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (birthdate)) {

						string pattern = "yyyy/MM/dd";
						DateTime parsedDate;
			
						DateTime.TryParseExact (birthdate, pattern, null, DateTimeStyles.None, out parsedDate);

						SPUser.SetBirthdate (parsedDate);
				}
		}
	
		private void getGenderAndSetSPUser ()
		{	
				if (checkIfValueHasBeenSet (gender)) {
						SPUser.SetGender((SPUserGender)Enum.Parse(typeof(SPUserGender), gender));
				}
		}
	
		private void getSexualOrientationAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (sexualOrientation)) {
					SPUser.SetSexualOrientation((SPUserSexualOrientation)Enum.Parse(typeof(SPUserSexualOrientation), sexualOrientation));
				}
		}
	
		private void getEthnicityAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (ethnicity)) {
						SPUser.SetEthnicity((SPUserEthnicity)Enum.Parse(typeof(SPUserEthnicity), ethnicity));
				}
		}
	
		private void getLocationAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (location)) {
						String[] latAndLongt = location.Split (',');
	
						String latitude = spliLatAndLongt (latAndLongt [0]);
						String longtitude = spliLatAndLongt (latAndLongt [1]);
				
						SponsorPay.SPLocation locationToBeSet = new SponsorPay.SPLocation ();
						locationToBeSet.Lat = float.Parse (latitude);
						locationToBeSet.Long = float.Parse (longtitude);
	        
						SPUser.SetLocation (locationToBeSet);
				}
		}
	
		private String spliLatAndLongt (String latOrLongt)
		{
				String latOrLongtFullValue = latOrLongt;
				String[] latOrLongtArray = latOrLongtFullValue.Split (':');
		
				return latOrLongtArray [1].Replace (" ", "");
		}
	
		private void getMaritalStatusAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (maritalStatus)) {
						SPUser.SetMaritalStatus((SPUserMaritalStatus)Enum.Parse(typeof(SPUserMaritalStatus), maritalStatus));
				}
		}
	
		private void getNumberOfChildrensAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (numberOfChildrens)) {
						SPUser.SetNumberOfChildrens (Int32.Parse (numberOfChildrens));
				}
		}
	
		private void getAnnualHouseholdIncomeAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (annualHouseholdIncome)) {
						SPUser.SetAnnualHouseholdIncome (Int32.Parse (annualHouseholdIncome));
				}
		}
	
		private void getEducationAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (education)) {
						SPUser.SetEducation((SPUserEducation)Enum.Parse(typeof(SPUserEducation), education));
				}
		}
	
		private void getZipcodeAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (zipcode)) {
						SPUser.SetZipcode (zipcode);
				}
		}
	
		private void getInterestsAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (interests)) {
						string[] words = interests.Split (',');
						SPUser.SetInterests (words);
				}
		}
	
		private void hasIapAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (iap)) {				
						SPUser.SetIap (Boolean.Parse(iap));
				}
		}
	
		private void getIapAmountAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (iapAmount)) {
						SPUser.SetIapAmount (float.Parse (iapAmount));
				}
		}
	
		private void getNumberOfSessionsAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (numberOfSessions)) {
						SPUser.SetNumberOfSessions (Int32.Parse (numberOfSessions));
				}
		}
	
		private void getPsTimeAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (psTime)) {
						SPUser.SetPsTime (long.Parse (psTime));
				}
		}
	
		private void getLastSessionAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (lastSession)) {
						SPUser.SetLastSession (long.Parse (lastSession));
				}
		}
	
		private void getConnectionAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (connection)) {
						SPUser.SetConnection((SPUserConnection)Enum.Parse(typeof(SPUserConnection), connection));
				}
		}
	
		private void getDeviceAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (device)) {
						SPUser.SetDevice (device);
				}
		}
	
		private void getAppVersionAndSetSPUser ()
		{
				if (checkIfValueHasBeenSet (appVersion)) {
						SPUser.SetAppVersion (appVersion);
				}
		}
	
}
	