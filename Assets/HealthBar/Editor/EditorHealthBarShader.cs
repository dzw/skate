/*! 
 * \file
 * \author Stig Olavsen <stig.olavsen@randomrnd.com>
 * \author http://www.RandomRnD.com
 * \date © 2011-August-05
 * \brief Definition of a custom inspector for the HealthBarShader class
 * \details Overrides the inspector in Unity to extend it
 */

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Custom editor for the HealthBarShader class
/// 
/// When the scene is playing, you can use the health slider
/// and "Set" button to set the displayed health of the healthbar.
/// </summary>
[CustomEditor(typeof(HealthBarShader))]
public class EditorHealthBarShader : Editor 
{
	/// <summary>
	/// The value of the inspector health-slider
	/// </summary>
	private float health = 1.0f;
	
	/// <summary>
	/// Draws the inspector GUI for HealthBar
	/// </summary>
	override public void OnInspectorGUI()
	{
		HealthBarShader hb = (HealthBarShader) target;  // Grab the object we are editing
				
		base.OnInspectorGUI();

		GUILayout.Space(25.0f);

		// If the application is playing, draw the controls that
		// let us set the health display on the fly from the editor.
		if (!EditorApplication.isPlaying)
		{
			GUI.enabled = false;
		}
		GUILayout.Label("Editor Healthbar Control");
		GUILayout.BeginHorizontal();
		GUILayout.Label("Health");
		health = GUILayout.HorizontalSlider(health, 0.0f, 1.0f);
		GUILayout.EndHorizontal();
		if (GUILayout.Button("Set"))
		{
			hb.SetHealth(health);			
		}
	}
}