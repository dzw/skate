/*! \mainpage

\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
\version 1.0.2011.05.02
 
\section About

A simple healthbar script for Unity. Lets you easily add a healthbar to
an object, and update it.

There are two methods included, one that changes the actual texture, and one
that is shader based (recommended).

To work correctly, the UV mapping of the object (refered to as canvas) has to
be set correctly.

If you are using the texture-modifying method, it is recommended to keep 
the resolution of the texture as low as possible.

Modifying the texture while running is costly for the GPU, and so you might
encounter performance problems if you have a lot of healthbars displayed that
are all updated simultaneously.

If you are using the shader based version, these problems should not be an issue.

\section Usage
 
\subsection UsageTexMod Usage of texture modifying version
Attach the HealthBar script to a GameObject. It will try to use the transform
you have attached it to as its canvas, and the texture of this transform as the
healthbar texture. If this is not what you want, adjust this in the inspector as
needed.

The canvas object should be properly UV mapped, and mapped to use the entire width
of the texture.

The texture needs to be marked as "Read/Write Enabled" in the texture importer, and
set to a Texture Format of either ARGB32, RGBA32, RGB24 or Alpha8. If you want
transparency in the texture or the background, use either ARGB32 or RGBA32.

If the texture doesn't meet these needs, it will allow you to correct it from
within the HealthBar inspector.

While the scene is playing, you can access the inspector for the HealthBar and
adjust the health displayed with a slider (this is intended for testing purposes,
and will only update the display on the HealthBar, it can't change the health
of any of your gameobjects).

\subsection UsageShader Usage of shader version

Attach either the "HealthBar/HealthBar Opaque" (if you do not need transparency)
or the "HealthBar/HealthBar Transparent" shader to a canvas mesh.

You can then attach the HealthBarShader.cs script to the same gameobject and use
it to update the health, or you can set the health ratio directly on the shader
from your own script.

\section Example

\subsection ExTexMod Example of texture modifying version
In your gameobject (creature or what have you) where you track the health,
get a reference to the healthbar associated with it. This should be done in
your gameobjects Awake or Start function.
\code
	HealthBar hb = GetComponent<HealthBar>();
\endcode

You can then update the healthbar by calling SetHealth. The value passed
should be between 0.0 and 1.0, for example the ratio between your creatures
current and maximum health
\code
	hb.SetHealth(creatureCurrentHealth / creatureMaximumHealth);
\endcode

\subsection ExShader Example of shader version
In your gameobject (creature or what have you) where you track the health,
get a reference to the healthbar-shader script associated with it. 
This should be done in your gameobjects Awake or Start function.
\code
	HealthBarShader hbs = GetComponent<HealthBarShader>();
\endcode

You can then update the healthbar by calling SetHealth. The value passed
should be between 0.0 and 1.0, for example the ratio between your creatures
current and maximum health
\code
	hb.SetHealth(creatureCurrentHealth / creatureMaximumHealth);
\endcode

Or you can just access the shader variable directly from your own script:
\code
    renderer.material.SetFloat("_Health", ratio);
\endcode

\section Changelog

 - 1.1 Added shader based healthbar
 - 1.0 Initial version


\section License

  Copyright (C) 2011 Stig Olavsen

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
     
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
     
  3. This notice may not be removed or altered from any source distribution.

  Stig Olavsen <stig.olavsen@randomrnd.com>

 */


/*! 
 * \file
 * \author Stig Olavsen <stig.olavsen@randomrnd.com>
 * \author http://www.RandomRnD.com
 * \date © 2011-May-02
 * \brief Definition of a custom inspector for the HealthBar class
 * \details Overrides the inspector in Unity to extend it
 */



/* --- Documentation for the other files in the project --- */

/*! \file ground.mat
\brief Material for the ground in the test scene
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
  */

/*! \file healthbar-frame.mat
\brief Material for the included healthbar frame
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
 */

/*! \file healthbar-gradient.mat
\brief Material for the texture modifying healthbar
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
 */

/*! \file healthbar-shader.mat
\brief Material for the shader based healthbar
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-August-05
 */

/*! \file healthdisplay.blend
\brief The blender model for the healthbar frame and canvas
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
 */

/*! \file Healthbar.prefab
\brief A finished healthbar as a prefab
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
 */

/*! \file healthbar-test.unity
\brief A test scene for the texture modifying healthbar
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
 */

/*! \file healthbar-shader-test.unity
\brief A test scene for the shader based healthbar
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-August-05
 */

/*! \file healthbar-frame.png
\brief Texture for the frame of the healthbar model
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
 */

/*! \file healthbar-gradient.png
\brief The texture for the actual healthbar
\author Stig Olavsen <stig.olavsen@randomrnd.com>
\author http://www.RandomRnD.com
\date © 2011-May-02
 */


/* --- --- */


using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Custom editor for the HealthBar class
/// 
/// Adds a few extras to the editor inspector. It checks that the
/// texture is read/writable and in the correct color format,
/// and allows you to correct it if it isn't - from within the
/// inspector.
/// 
/// When the scene is playing, you can use the health slider
/// and "Set" button to set the displayed health of the healthbar.
/// </summary>
[CustomEditor(typeof(HealthBar))]
public class EditorHealthBar : Editor 
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
		HealthBar hb = (HealthBar) target;  // Grab the object we are editing
				
		base.OnInspectorGUI();
		
		// We must always have a canvas and texture
		if (hb.healthbarTexture == null || hb.canvas == null)
		{
			if (hb.canvas == null)
			{
				hb.canvas = hb.transform;
			}
			if (hb.healthbarTexture == null)
			{
				hb.healthbarTexture = (Texture2D) hb.canvas.renderer.sharedMaterial.mainTexture;
			}
			EditorUtility.SetDirty(target);
		}

		GUILayout.Space(25.0f);
		
		// Grab the importer for the texture, to check that we can use it
		string hbpath = AssetDatabase.GetAssetPath(hb.healthbarTexture);
		TextureImporter ti = (TextureImporter) AssetImporter.GetAtPath(hbpath);
		
		// Texture must be readable, if it isn't draw a button to change
		// the import settings
		if (!ti.isReadable)
		{
			GUILayout.Label("Warning! Texture is not readable!");
			GUILayout.Label("Click here to modify import settings");
			GUILayout.Label("and make texture readable/writable");
			if (GUILayout.Button("Make Readable"))
			{
				ti.isReadable = true;
				AssetDatabase.ImportAsset(hbpath, ImportAssetOptions.ForceUpdate);
			}
		}
		
		// Texture must be in one of the following formats: ARGB32, RGBA32, RGB24 or Alpha8
		// If it isn't, display some buttons to change the import settings
		if (ti.textureFormat != TextureImporterFormat.Alpha8 &&
		    ti.textureFormat != TextureImporterFormat.ARGB32 &&
		    ti.textureFormat != TextureImporterFormat.RGBA32 &&
		    ti.textureFormat != TextureImporterFormat.RGB24)
		{
			GUILayout.Label("Warning! Texture is in wrong format!");
			GUILayout.Label("Supported formats are Alpha8, ARGB32, RGBA32 and RGB24");
			GUILayout.Label("Click a button below to modify import settings");
			GUILayout.Label("and set one of the supported formats (recommended: RGBA32)");
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("RGBA32"))
			{
				ti.textureFormat = TextureImporterFormat.RGBA32;
				AssetDatabase.ImportAsset(hbpath, ImportAssetOptions.ForceUpdate);
			}
			if (GUILayout.Button("ARGB32"))
			{
				ti.textureFormat = TextureImporterFormat.ARGB32;
				AssetDatabase.ImportAsset(hbpath, ImportAssetOptions.ForceUpdate);
			}
			if (GUILayout.Button("RGB24"))
			{
				ti.textureFormat = TextureImporterFormat.RGB24;
				AssetDatabase.ImportAsset(hbpath, ImportAssetOptions.ForceUpdate);
			}
			if (GUILayout.Button("Alpha8"))
			{
				ti.textureFormat = TextureImporterFormat.Alpha8;
				AssetDatabase.ImportAsset(hbpath, ImportAssetOptions.ForceUpdate);				
			}
			GUILayout.EndHorizontal();
		}

		
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