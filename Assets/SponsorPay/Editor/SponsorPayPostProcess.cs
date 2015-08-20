using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;

public class SponsorPayPostProcess
{

	[PostProcessBuild(500)]
	public static void OnPostProcessBuild( BuildTarget target, string path )
	{

		if (target == BuildTarget.iPhone)
		{
			UnityEditor.XCodeEditor.SponsorPay.XCProject project = new UnityEditor.XCodeEditor.SponsorPay.XCProject(path);
			
			// Find and run through all projmods files to patch the project
			string projModPath = System.IO.Path.Combine(Application.dataPath, "SponsorPay/iOS");
			string[] files = System.IO.Directory.GetFiles(projModPath, "*.projmods", System.IO.SearchOption.AllDirectories);
			foreach( var file in files ) 
			{
				project.ApplyMod(Application.dataPath, file);
			}
			project.Save();
			
		}
	}
}

