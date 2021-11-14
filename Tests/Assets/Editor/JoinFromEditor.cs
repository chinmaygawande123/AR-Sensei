using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JoinFrom))]
public class JoinFromEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		JoinFrom myScript = (JoinFrom)target;
		
		if (GUILayout.Button("Join Meeting"))
		{
			myScript.JoinMeeting();
		}
	}
}
