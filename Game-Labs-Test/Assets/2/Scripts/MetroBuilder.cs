using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondTask
{
	public class MetroBuilder : MonoBehaviour
	{
		private MetroData metroData;
		private Station stationsPrefab;

		private List<Station> stations;

		[SerializeField] private PathCalculateView view;

		private System.Action<Vector2> onCalculateEnd;

		private void Start()
		{
			metroData = Resources.Load<MetroData>("Data/MetroData");
			stationsPrefab = Resources.Load<Station>("Prefabs/Station");

			stations = new List<Station>();

			for (int i=0; i<metroData.stations.Count; i++)
			{
				Station station = Instantiate(stationsPrefab, transform);
				station.transform.localPosition = metroData.stations[i].GetPosition();
				station.Initialize(metroData.branches.Find(b => metroData.stations[i].branchId == b.branchId).color, metroData.stations[i].stationName);

				stations.Add(station);
			}
			metroData.BuildMatrix();

			int[] nativeTunnels = new int[metroData.stations.Count];
			Dictionary<int, List<int>> branchesConnectedCount = new Dictionary<int, List<int>>();

			for(int i= 0; i<metroData.stations.Count-1; i++)
				for (int j = i+1; j < metroData.stations.Count; j++)
				{
					if(metroData.pathMatrix[i,j] == -1)
					{
						if (metroData.pathMatrix[i, i] == metroData.pathMatrix[j, j])
						{
							stations[i].BuildLine(metroData.stations[j].GetPosition(), metroData.GetBranchColor(metroData.stations[i].branchId));
							metroData.pathMatrix[i, j] = metroData.pathMatrix[j, i] = metroData.stations[i].branchId + 1;
							nativeTunnels[i] = nativeTunnels[j] = 2;
						}
						else
						{
							if (!branchesConnectedCount.ContainsKey(i))
								branchesConnectedCount.Add(i, new List<int>());

							if (!branchesConnectedCount[i].Contains(metroData.stations[j].branchId))
								branchesConnectedCount[i].Add(metroData.stations[j].branchId);
						}
					
					}
				}

			for (int i = 0; i < metroData.stations.Count - 1; i++)
				for (int j = i + 1; j < metroData.stations.Count; j++)
				{
					if (metroData.pathMatrix[i, j] == -1)
					{
						if(nativeTunnels[i] > 1 && branchesConnectedCount[i].Count == 1)
						{
							stations[i].BuildLine(metroData.stations[j].GetPosition(), metroData.GetBranchColor(metroData.stations[j].branchId));
							metroData.pathMatrix[i, j] = metroData.pathMatrix[j, i] = metroData.stations[j].branchId + 1;
							nativeTunnels[j]++;
						}
					}
				}

			for (int i = 0; i < metroData.stations.Count - 1; i++)
				for (int j = i + 1; j < metroData.stations.Count; j++)
				{
					if (metroData.pathMatrix[i, j] == -1)
					{
						int index = -1;
						if (nativeTunnels[i] < 2 || nativeTunnels[j] < 2)
							index = nativeTunnels[i] < 2 ? metroData.stations[i].branchId : metroData.stations[j].branchId;
						else
							index = branchesConnectedCount[i].Find(ind => ind != metroData.stations[j].branchId);

						stations[i].BuildLine(metroData.stations[j].GetPosition(), metroData.GetBranchColor(index));
						metroData.pathMatrix[i, j] = metroData.pathMatrix[j, i] = index + 1;
					}
				}

			view.Initialize(metroData.GetStationsList(), CalculatePath,  ref onCalculateEnd);
		}

		private void CalculatePath(int start, int end)
		{
			onCalculateEnd?.Invoke(metroData.CalculatePath(start, end));
		}

	}
}
