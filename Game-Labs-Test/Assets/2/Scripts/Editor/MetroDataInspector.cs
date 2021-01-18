using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SecondTask
{
	[CustomEditor(typeof(MetroData))]
	public class MetroDataInspector : Editor
	{

		private List<int> maskResult = new List<int>();
		public override void OnInspectorGUI()
		{
			MetroData m_target = target as MetroData;
			List<StationData> stations = m_target.stations;
			List<BranchData> branches = m_target.branches;
			EditorGUILayout.BeginVertical();
			for (int i=0; i< stations.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				string stationName = EditorGUILayout.TextField(stations[i].stationName, GUILayout.Width(100));
				EditorGUILayout.LabelField("X: ", GUILayout.Width(20));
				int x = EditorGUILayout.IntField(stations[i].x, GUILayout.Width(20));
				EditorGUILayout.LabelField("Y: ", GUILayout.Width(20));
				int y = EditorGUILayout.IntField(stations[i].y, GUILayout.Width(20));
				EditorGUILayout.LabelField("Branch: ", GUILayout.Width(60));
				int branch = EditorGUILayout.Popup(stations[i].branchId, m_target.GetBrahchesList(), GUILayout.Width(30));
				EditorGUILayout.LabelField("Connections: ", GUILayout.Width(80));
				int mask = EditorGUILayout.MaskField(stations[i].connectionStation, m_target.GetStationsList());
				EditorGUILayout.EndHorizontal();
				stations[i] = new StationData(stations[i].stationId, stationName, x, y, branch, mask);
			}
			if (GUILayout.Button("Add station"))
			{
				m_target.AddStation("New station", 0, 0, 0, 0);
			}

			for (int i = 0; i < branches.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(branches[i].branchId.ToString(), GUILayout.Width(20));
				Color color = EditorGUILayout.ColorField(branches[i].color, GUILayout.Width(150));
				EditorGUILayout.EndHorizontal();
				branches[i] = new BranchData(branches[i].branchId, color);
			}
			if (GUILayout.Button("Add brach"))
			{
				m_target.AddBranch(Color.black);
			}
			EditorGUILayout.EndVertical();

			EditorUtility.SetDirty(m_target);
		}
	}
}
