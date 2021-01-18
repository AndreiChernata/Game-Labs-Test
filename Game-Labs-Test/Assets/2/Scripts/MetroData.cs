using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondTask
{
	[CreateAssetMenu(fileName = "MetroData", menuName = "Second Task/Create MetroData", order = 0)]
	public class MetroData : ScriptableObject
	{
		public List<StationData> stations = new List<StationData>();

		public List<BranchData> branches = new List<BranchData>();

		public int[,] pathMatrix;

		public void BuildMatrix()
		{
			pathMatrix = new int[stations.Count, stations.Count];

			for (int i = 0; i < stations.Count; i++)
			{
				pathMatrix[i, i] = stations[i].branchId;
				for (int j = 0; j < stations.Count; j++)
				{
					if (i != j)
					{
						int layer = 1 << j;
						if ((stations[i].connectionStation & layer) != 0)
						{
							pathMatrix[i, j] = -1;
							pathMatrix[j, i] = -1;
						}
					}
				}
			}
		}

		public Vector2 CalculatePath(int startPoint, int endPoint)
		{
			int[] distance = new int[stations.Count];
			int[][] path = new int[stations.Count][];
			for (int i = 0; i < distance.Length; i++)
			{
				distance[i] = 1000;
			}

			path[startPoint] = new int[1];
			path[startPoint][0] = startPoint;
			distance[startPoint] = 0;

			List<int> checkedPoints = new List<int>();

			int currentPoint = startPoint;

			while(checkedPoints.Count < stations.Count)
			{
				for (int i=0; i<stations.Count; i++)
				{
					if(pathMatrix[currentPoint,i] !=0)
					{
						if(distance[i] > distance[currentPoint] + 1)
						{
							distance[i] = distance[currentPoint] + 1;
							path[i] = new int[path[currentPoint].Length + 1];
							path[i][path[currentPoint].Length] = i;
							for (int k = 0; k < path[currentPoint].Length; k++)
								path[i][k] = path[currentPoint][k];

							checkedPoints.Remove(i);
						}
					}
				}
				checkedPoints.Add(currentPoint);
				bool nextPointFind = false;
				while (!nextPointFind && checkedPoints.Count < stations.Count)
				{
					for (int y = checkedPoints.Count - 1; y >= 0; y--)
					{
						List<int> neighboringPoint = GetNeighboringPoints(checkedPoints[y]);
						for (int i = 0; i < neighboringPoint.Count; i++)
							if (!checkedPoints.Contains(neighboringPoint[i]))
							{
								currentPoint = neighboringPoint[i];
								nextPointFind = true;
								break;
							}
					}
					
				}


			}
			return new Vector2(distance[endPoint], CalculateTransitionStation(path[endPoint]));
		}

		private int CalculateTransitionStation(int[] path)
		{
			int transitionCount = 0;
			for(int i=0; i<path.Length-2; i++)
			{
				if (pathMatrix[path[i], path[i + 1]] != pathMatrix[path[i + 1], path[i + 2]])
				{
					Debug.Log("Transitions: " + stations.Find(s => s.stationId ==  path[i + 1]).stationName);
					transitionCount++;
				}
			}

			return transitionCount;
		}

		private List<int> GetNeighboringPoints(int point)
		{
			List<int> np = new List<int>();
			for (int i = 0; i < stations.Count; i++)
			{
				if (pathMatrix[point, i] != 0)
					np.Add(i);
			}

			return np;
		}

		public Color GetBranchColor(int branchId)
		{
			return branches.Find(b => b.branchId == branchId).color;
		}

		#region editor_utilities
		public void AddStation(string stationName, int x, int y, int branchNumber, int connectionStations)
		{
			StationData stationData = new StationData(GetIdForNewStation(), stationName, x, y, branchNumber, connectionStations);
			stations.Add(stationData);
		}

		private int GetIdForNewStation()
		{
			for (int i = 0; i < stations.Count; i++)
				if (i != stations[i].stationId)
					return i;

			return stations.Count;
		}

		public void AddBranch(Color color)
		{
			BranchData branchData = new BranchData(GetIdForNewBranch(), color);
			branches.Add(branchData);
		}

		private int GetIdForNewBranch()
		{
			for (int i = 0; i < branches.Count; i++)
				if (i != branches[i].branchId)
					return i;

			return branches.Count;
		}

		public string[] GetBrahchesList()
		{
			string[] array = new string[branches.Count];

			for (int i = 0; i < branches.Count; i++)
				array[i] = branches[i].branchId.ToString();

			return array;

		}

		public string[] GetStationsList()
		{
			string[] array = new string[stations.Count];

			for (int i = 0; i < stations.Count; i++)
				array[i] = stations[i].stationName;

			return array;

		}
		#endregion
	}
}
