using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdTask
{
	public class EmptySlot : ISlotItem
	{
		string ISlotItem.Name { get => "---"; set => throw new System.NotImplementedException(); }

		public void AddToShip(SpaceShip spaceShip)
		{
		}

		public void RemoveFromShip(SpaceShip spaceShip)
		{
		}
	}
}
