using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdTask
{
	public class Weapon : ISlotItem
	{
		private string weaponType;

		private int baseDamage;
		private float baseReload;

		private float reload;

		private float fireTimer = 0F;

		public virtual string Name { get; set; }

		public Weapon(int baseDamage, float baseReload, string name)
		{
			Name = name;
			this.baseDamage = baseDamage;
			this.baseReload = baseReload;
		}

		public void AddToShip(SpaceShip spaceShip)
		{
			spaceShip.AddWeapon(this);
		}

		public void RemoveFromShip(SpaceShip spaceShip)
		{
			spaceShip.RemoveWeapon(this);
		}

		public void Setup(float reloadMultiplayer)
		{
			reload = baseReload * reloadMultiplayer;
		}

		public int Fire()
		{
			if (fireTimer <= 0F)
			{
				fireTimer = reload;
				return baseDamage;
			}
			else
				fireTimer -= Time.deltaTime;

			return 0;

		}
	}
}