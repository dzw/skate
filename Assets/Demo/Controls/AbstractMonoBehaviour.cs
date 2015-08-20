using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SponsorPay.Demo
{
	public abstract class AbstractMonoBehaviour : MonoBehaviour
	{

		// Constants
		public const float HorizontalMargin = 16;
		public const float GuiLabelHeight = 50;
		public const float GuiTapTargetHeight = 90;
		public const float GuiRectSmallPadding = 0;
		public const float GuiRectBigPadding = 20;
		public const float HorizontalPadding = 10;
		public const float GuiPaddingTopToggle = 15;
		public const float InertiaDuration = 0.75f;
		
		// Private Variables

		protected float ViewHeight = 2840;
		protected float scrollVelocity = 0f;
		protected float timeTouchPhaseEnded = 0f;
		protected Rect windowRect;
		
		protected float GuiRectWidth;

		protected Vector2 scrollPosition = Vector2.zero;
		protected Touch touch;

		protected string dialogTitle;
		protected string dialogMessage;
		
		protected bool showDialog = false;

		// Update: called once per frame
		void Update() 
		{		
			if (Input.touchCount != 1) {
				
				if (scrollVelocity != 0.0f) {
					float t = (Time.time - timeTouchPhaseEnded) / InertiaDuration;
					if (scrollPosition.y <= 0 || scrollPosition.y >= ViewHeight) {
						scrollVelocity = 0.0f;
					}
					
					float frameVelocity = Mathf.Lerp(scrollVelocity, 0, t);
					scrollPosition.y += frameVelocity * Time.deltaTime;
					
					if (t >= 1.0f) {
						scrollVelocity = 0.0f;
					}
				}
				return;
			}
		}
		
		
		// OnGUI: called for rendering and handling GUI events.
		void OnGUI()
		{   
			// Recalculated OnOrientationChance
			GuiRectWidth = Screen.width - (2 * HorizontalMargin);
			
			Skin();
			
			if (Input.touchCount == 0) {
				Draw();
				return;
			}
			
			Touch touch = Input.touches[0];
			
			if (touch.phase == TouchPhase.Began) {
				scrollVelocity = 0.0f;
				
				dragging = true;
			} else if (touch.phase == TouchPhase.Moved) {
				scrollPosition.y += touch.deltaPosition.y;
			}
			
			Draw();
			
			if (touch.phase == TouchPhase.Ended) {
				if (Mathf.Abs(touch.deltaPosition.y) >= 10) {
					scrollVelocity = (int)(touch.deltaPosition.y / touch.deltaTime);
				}
				
				dragging = false;
				timeTouchPhaseEnded = Time.time;
			}
		}
		
		
		// Skin: Set the theme
		
		void Skin() 
		{
			GUIStyle lStyle = new GUIStyle();
			lStyle.fontSize = 30;
			lStyle.normal.textColor = Color.white;
			
			GUIStyle tStyle = GUI.skin.textField;
			tStyle.fontSize = 50;
			tStyle.normal.textColor = Color.white;
			tStyle.alignment = TextAnchor.MiddleLeft;
			
			GUIStyle bStyle = GUI.skin.button;
			bStyle.fontSize = 40;
			bStyle.normal.textColor = Color.white;
			
			GUIStyle tgStyle = GUI.skin.toggle;
			tgStyle.fontSize = 30;
			tgStyle.normal.textColor = Color.white;
			tgStyle.border.top = 0;
			tgStyle.border.right = 0;
			tgStyle.border.bottom = 0;
			tgStyle.border.left = 0;
			tgStyle.overflow.right = -(Screen.width - 70);
			tgStyle.overflow.bottom = -50;
			tgStyle.padding.left = 50;
			
			GUI.skin.label = lStyle;
			GUI.skin.textField = tStyle;
			GUI.skin.button = bStyle;
			GUI.skin.toggle = tgStyle;
			GUI.skin.window = GUIStyle.none;
			GUI.skin.verticalScrollbar = GUIStyle.none;
			GUI.skin.horizontalScrollbar = GUIStyle.none;
		}
		// Draw: Add UI Controls to the Window
		
		void Draw() {
			if (showDialog) {
				drawDialogMessage();
				return;
			} 
			
			if (keyboard != null && keyboard.done) {
				currentFocusedControl = GUI.GetNameOfFocusedControl();
				updatedTextValue = keyboard.text;
			}
			
			DrawImpl();
		}

		protected abstract void DrawImpl();
		protected abstract void DialogClosed();
		
		// Dialog Methods
		void drawDialogMessage() {
			GUI.ModalWindow(10, new Rect (HorizontalMargin, 0, GuiRectWidth, Screen.height ), LoginWindow, "");
		}
		
		void LoginWindow(int windowID) {
			GUIStyle style = GUI.skin.label;
			style.alignment = TextAnchor.UpperLeft;
			style.clipping = TextClipping.Clip;
			style.wordWrap = true;
			style.fontSize = 32;
			
			float buttonOffsetY = Screen.height - GuiTapTargetHeight - 2 * GuiRectBigPadding;
			float labelHeight = buttonOffsetY - GuiRectBigPadding;
			
			GUI.Label(new Rect(0, GuiRectBigPadding, GuiRectWidth, labelHeight), dialogTitle + ": " + dialogMessage, style);
			if (GUI.Button(new Rect(0, buttonOffsetY, GuiRectWidth, GuiTapTargetHeight), "OK")) {
				showDialog = false;
				DialogClosed();
			}
		}

		// SPGUI
		TouchScreenKeyboard keyboard;
		string currentFocusedControl = "";
		string updatedTextValue = "";
		bool dragging = false;
		
		
		protected string TextField(string controlName, Rect rect, string text, TouchScreenKeyboardType type) 
		{
			GUI.SetNextControlName(controlName);
			if (rect.Contains(Event.current.mousePosition)) {
				if (Input.GetMouseButtonDown(0)) {
					GUI.color = new Color(1,1,1,2);
					GUI.enabled = false;
				} else if (Input.GetMouseButtonUp(0) && !dragging) {
					GUI.color = Color.white;
					GUI.enabled = true;
					FocusField(controlName, text, type);
				}
			}
			
			if (currentFocusedControl == controlName) {
				text = updatedTextValue;
				currentFocusedControl = "";
				updatedTextValue = "";
			}
			
			return GUI.TextField(rect, text);
		}
		
		void FocusField(string controlName, string text, TouchScreenKeyboardType type)
		{
			if (GUI.GetNameOfFocusedControl() == controlName) return;
			
			GUI.FocusControl(controlName);
			keyboard = TouchScreenKeyboard.Open(text, type);
		}

	}
}

