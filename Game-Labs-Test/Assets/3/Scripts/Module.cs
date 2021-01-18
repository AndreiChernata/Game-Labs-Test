using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdTask
{
	public abstract class Module : ISlotItem
	{
		public virtual int AdditionalShield
		{
			get
			{
				return 0;
			}
		}

		public virtual int AdditionalLife
		{
			get
			{
				return 0;
			}
		}

		public virtual int IncreaseReload
		{
			get
			{
				return 0;
			}
		}

		public virtual int ShieldRecovery
		{
			get
			{
				return 0;
			}
		}

		public void AddToShip(SpaceShip spaceShip)
		{
			spaceShip.AddModule(this);
		}

		public void RemoveFromShip(SpaceShip spaceShip)
		{
			spaceShip.RemoveModule(this);
		}

		public virtual string Name { get; set; }
	}
}