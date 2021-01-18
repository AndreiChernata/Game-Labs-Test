using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdTask
{
	public static class MockDataLayer
	{
		public static List<ISlotItem> WeaponsData
		{
			get
			{
				return new List<ISlotItem>
			{
				new EmptySlot(),
				new Weapon(3, 5, "A"),
				new Weapon(2, 4, "B"),
				new Weapon(5, 20, "C")
			};
			}
		}
		public static List<ISlotItem> ModulesData
		{
			get
			{
				return new List<ISlotItem>
			{
				new EmptySlot(),
				new ModuleA(),
				new ModuleB(),
				new ModuleC(),
				new ModuleD()
			};
			}
		}
	}
}
