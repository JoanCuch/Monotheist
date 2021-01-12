using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Monotheist;

[CustomEditor(typeof(HumanStatsSpy))]
public class HumanStatsSpyEditor : Editor
{
	public override void OnInspectorGUI()
	{
		//Repaint();
		//base.OnInspectorGUI();
		HumanStatsSpy _statsList = (HumanStatsSpy)target;
		int _maxWitdh = 100;

		if (_statsList == null)
			return;

		if(_statsList.currentGoalState != null)
			EditorGUILayout.LabelField("Goal state", _statsList.currentGoalState.ToString(), EditorStyles.whiteLabel);

		if(_statsList.currentActionState != null)
			EditorGUILayout.LabelField("Action state", _statsList.currentActionState.ToString(), EditorStyles.whiteLabel);

		foreach (NeedStats stats in _statsList.needsList)
		{
			EditorGUILayout.LabelField(stats.tag.ToString(), EditorStyles.whiteLabel);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Satisfaction", GUILayout.MaxWidth(_maxWitdh));
			EditorGUILayout.Slider(stats.satisfaction, 0, 100);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("State", GUILayout.MaxWidth(_maxWitdh));
			EditorGUILayout.LabelField(stats.currentState.ToString());
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("List count", GUILayout.MaxWidth(_maxWitdh));
			EditorGUILayout.LabelField(stats.listCount.ToString());
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("List state", GUILayout.MaxWidth(_maxWitdh));
			EditorGUILayout.LabelField(stats.listState.ToString());
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

			
		}		
	}
}
