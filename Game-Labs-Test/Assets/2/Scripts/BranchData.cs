using UnityEngine;

namespace SecondTask
{
	[System.Serializable]
	public struct BranchData
	{
		public int branchId;
		public Color color;

		public BranchData(int branchId, Color color)
		{
			this.branchId = branchId;
			this.color = color;
		}
	}
}
