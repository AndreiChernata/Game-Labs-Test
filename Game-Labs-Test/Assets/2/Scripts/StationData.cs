using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondTask
{
	[System.Serializable]
	public struct StationData
	{
		public int stationId;
		public string stationName;
		public int x;
		public int y;
		public int branchId;

		//calculate by layer mask
		public int connectionStation;

		public StationData(int stationId, string stationName, int x, int y, int branchId, int connectionStation)
		{
			this.stationId = stationId;
			this.stationName = stationName;
			this.x = x;
			this.y = y;
			this.branchId = branchId;
			this.connectionStation = connectionStation;
		}

		public Vector3 GetPosition()
		{
			return new Vector3(x, y) * 2F;
		}
	}
}
