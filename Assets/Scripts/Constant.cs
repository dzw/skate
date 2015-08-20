using UnityEngine;
using System.Collections;
using System.Text;
using System;

using System.Security.Cryptography;

public class Constant
{

		public enum SCENES
		{
				WHEELSCENE,
				GAMESCENE
				
		}

		public enum API_REQUEST_TYPE
		{
				NONE,
				REGISTER_EMAIL,
				LOGIN_EMAIL,
				GRAPH_FACEBOOK,
				REGISTER_FB_USER,
				IMAGE_URL,
				UPDATE_SCORE,
				GET_USER_SCORE,
				GET_TOP_SCORE,
				GET_TOP_SCORER,
				GET_CATEGORY,
				GET_APP_SETTINGS,
				GET_QUIZ

		}
//	1577635702449281

//		public const string API_URL = "http://trivia.mipaes.com/api/api.php";
//		public const string PRIVATE_KEY = "TR#I4VI5A5A@!aT*89I1NG";
//		public  const string RegisterEmail = "register_email";
//		public  const string LoginEmail = "login_email";
//		public  const string RegisterOrLoginFB = "register_or_login_facebook";
//		public  const string UpdateScore = "update_score";
//		public  const string GetUserScore = "get_user_score";
//		public  const string GetTopScore = "get_top_score";
//		public  const string GetTopScorer = "get_top_scorer";
//		public  const string GetCategory = "get_category";
//		public  const string GetAppSettings = "get_app_settings";
//		public  const string GetQuiz = "get_quiz";
//		public  const string LOGIN_SCENE = "LoginScene";
//		public  const string REGISTRATION_SCENE = "RegistrationScene";
//		public  const string MAIN_SCENE = "MainScene";
//		public  const string QUIZ_SCENE = "QuizScene";
//		public const string SCORE_SCENE = "ScoreBoardScene";
		public const string USER_ID = "user_id";
		public const string USER_NAME = "user_name";
	public const string GRAPH_FACEBOOK_URL = "https://graph.facebook.com/v2.2/me?fields=id,first_name,email&access_token=";
	


}
