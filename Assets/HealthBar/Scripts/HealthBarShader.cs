/*! 
 * \file
 * \author Stig Olavsen <stig.olavsen@randomrnd.com>
 * \author http://www.RandomRnD.com
 * \date © 2011-August-05
 * \brief A simple script to control the healthbar shader
 * \details Use this script to control the displayed health, or just
 * update the shader parameter "_Health" directly
 */

using UnityEngine;
using System;
using System.Collections;
using System.Linq;

/// <summary>
/// Add this to your gameobject along with either a "HealthBar/HealthBar Opaque"
/// or "HealthBar/HealthBar Transparent" shader, to control the displayed
/// health value of the shader.
/// 
/// Call the function SetHealth(healthRatio) to set the ratio the healthbar should
/// display.
/// 
/// You can also just update the shader directly from your own script through the
/// shader-variable "_Health" by calling renderer.material.SetFloat("_Health", healthRatio);
/// 
/// The ratio should always be a number between 0.0 and 1.0.
/// </summary>

public class HealthBarShader : MonoBehaviour
{
	/// <summary>
	/// The function that updates the healthbar.
	/// 
	/// Call this function whenever you want to update the current displayed
	/// health on your healthbar.
	/// 
	/// You can also update the shader directly, and not use this script at all
	/// by calling renderer.material.SetFloat("_Health", healthRatio);
	/// </summary>
	/// <param name="ratio">
	/// The ratio of the healthbar, should be between 0.0 (empty) 
	/// and 1.0 (full)
	/// </param>
	public void SetHealth(float ratio)
	{
		// Clamp ratio between 0.0 and 1.0
		if (ratio < 0.0f) ratio = 0.0f;
		if (ratio > 1.0f) ratio = 1.0f;
		
		renderer.material.SetFloat("_Health", ratio);	
	}
}
