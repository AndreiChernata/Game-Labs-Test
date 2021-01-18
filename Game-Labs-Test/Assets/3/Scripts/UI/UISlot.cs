using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdTask
{
	public class UISlot : MonoBehaviour
	{
		[SerializeField] private UnityEngine.UI.Text textBox = null;

		private List<ISlotItem> slotItems = new List<ISlotItem>();

		private System.Action<ISlotItem, ISlotItem> onItemChanged;

		private int currentIndex;

		[SerializeField] private GameObject leftButton;
		[SerializeField] private GameObject rightButton;

		private List<int> occupyIndexes;

		public void Initialize(System.Action<ISlotItem, ISlotItem> onItemChanged, List<ISlotItem> items, List<int> occupyIndexes)
		{
			this.onItemChanged = onItemChanged;
			slotItems = items;
			currentIndex = 0;
			textBox.text = slotItems[currentIndex].Name;
			this.occupyIndexes = occupyIndexes;
		}

		public void LeftClick()
		{
			int previusIndex = currentIndex;
			FreeCurrentIndex();
			DecreaseIndex();
			OccupyCurrentIndex();
			onItemChanged(slotItems[currentIndex], previusIndex == 0 ? null : slotItems[previusIndex]);
		}

		public void RightClick()
		{
			int previusIndex = currentIndex;
			FreeCurrentIndex();
			IncreaseIndex();
			OccupyCurrentIndex();
			onItemChanged(slotItems[currentIndex], previusIndex == 0 ? null : slotItems[previusIndex]);
		}

		private void DecreaseIndex()
		{
			do
			{
				currentIndex--;
				RoundIndex();
			}
			while (occupyIndexes.Contains(currentIndex));
		}

		private void IncreaseIndex()
		{
			do
			{
				currentIndex++;
				RoundIndex();
			}
			while (occupyIndexes.Contains(currentIndex));
		}

		private void RoundIndex()
		{
			if (currentIndex < 0)
				currentIndex = slotItems.Count - 1;
			if (currentIndex >= slotItems.Count)
				currentIndex = 0;
		}

		private void FreeCurrentIndex()
		{
			if (currentIndex != 0)
				occupyIndexes.Remove(currentIndex);
		}

		private void OccupyCurrentIndex()
		{
			if (currentIndex != 0 && !occupyIndexes.Contains(currentIndex))
				occupyIndexes.Add(currentIndex);

			textBox.text = slotItems[currentIndex].Name;
		}
	}
}