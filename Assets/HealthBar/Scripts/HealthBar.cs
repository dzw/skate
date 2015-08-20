/*! 
 * \file
 * \author Stig Olavsen <stig.olavsen@randomrnd.com>
 * \author http://www.RandomRnD.com
 * \date © 2011-May-02
 * \brief Definition for the HealthBar class
 * \details Displays a health bar on the specified canvas.
 */

using UnityEngine;
using System;
using System.Collections;
using System.Linq;

/// <summary>
/// Add this to a gameobject to display a healthbar.
/// 
/// Use healthbarTexture to define the texture that should be used
/// for the display. If none is provided, it will use the current texture
/// of the gameobject.
/// 
/// Set the canvas to define which transform the texture should be displayed
/// on. If none is provided, it will use the transform to which this script is
/// attached.
/// 
/// This script functions by reducing the width of the actual texture displayed,
/// so the canvas object used should be UV mapped to use the whole width of the
/// texture.
/// 
/// To update the healthbar, call the SetHealth function, with a value between
/// 0.0 and 1.0, which will determine how full the healthbar will be.
/// </summary>
/// 
/// <example>
/// Example:
/// <code>
/// HealthBar hb = GetComponent<HealthBar>();
/// hb.SetHealth(creatureCurrentHealth / creatureMaximumHealth);
/// </code>
/// </example>
public class HealthBar : MonoBehaviour
{
	/// <summary>
	/// The texture to use for the healthbar.
	/// </summary>
	public Texture2D healthbarTexture;
	/// <summary>
	/// The background color to show where the health is depleted.
	/// Use a transparent shader (ex. Transparent/Diffuse) if you
	/// want transparency.
	/// </summary>
	public Color backgroundColor;
	/// <summary>
	/// The GameObject on which the healthbar will be drawn.
	/// </summary>
	public Transform canvas;
	
	/// <summary>
	/// Holds the current displayed healthbar
	/// </summary>
	private Texture2D myTexture;
	/// <summary>
	/// Keeps the width of the last update, so we know when we need
	/// to "re-fill" part of the healthbar
	/// </summary>
	private int myLastWidth;
	
	/// <summary>
	/// Initialization routines
	/// </summary>
	void Start()
	{
		// Instantiate our texture
		myTexture = new Texture2D(healthbarTexture.width, 
		                          healthbarTexture.height, 
		                          healthbarTexture.format, 
		                          false);
		
		Color[] c = healthbarTexture.GetPixels(0, 0, healthbarTexture.width, healthbarTexture.height);
		
		myTexture.SetPixels(c);
		myTexture.Apply();
		
		myLastWidth = healthbarTexture.width;
		
		// Set the material of the canvas to our texture so we can update it
		canvas.renderer.material.mainTexture = myTexture;		
	}
	
	/// <summary>
	/// The function that updates the healthbar.
	/// 
	/// Call this function whenever you want to update the current displayed
	/// health on your healthbar.
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
		
		// Calculate the width in pixels of the requested ratio
		int w = (int)(ratio * myTexture.width);
		int h = myTexture.height;
		int hw = myTexture.width - w;
		
		// If health has increased, we must replace some pixels from
		// the original texture
		if (w > myLastWidth)
		{
			int ow = w - myLastWidth;
			Color[] c = healthbarTexture.GetPixels(myLastWidth, 0, ow, healthbarTexture.height);
			myTexture.SetPixels(myLastWidth, 0, ow, healthbarTexture.height, c);
		}
		
		myLastWidth = w;
		
		// Update texture to the current ratio
		Color[] blank = Enumerable.Repeat<Color>(backgroundColor, hw*h).ToArray();
		myTexture.SetPixels(w,0,hw,h,blank);
		myTexture.Apply();		
	}
}
