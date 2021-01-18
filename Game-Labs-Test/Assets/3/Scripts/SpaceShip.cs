using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdTask
{
	public class SpaceShip : MonoBehaviour
	{
		[SerializeField] private string ShipName;


		private List<Module> modules = new List<Module>();
		private List<Weapon> weapons = new List<Weapon>();


		[SerializeField] private int weaponSlotsCount;
		[SerializeField] private int moduleSlotsCount;

		[SerializeField] private int baseLife;
		[SerializeField] private int baseShield;
		[SerializeField] private float baseShildRecoverySeconds;
		private float reloadMultiplayer;
		private float shieldRecovery;

		private bool battle;
		private System.Action<string> onBattleEnd;
		private SpaceShip enemy;

		private SpaceShipView view;

		public void Initialize(ref System.Action onBattleStart, ref System.Action<string> onBattleEnd, SpaceShip enemy)
		{
			onBattleStart += StartBattle;
			onBattleEnd += delegate { battle = false; };
			this.onBattleEnd += onBattleEnd;
			this.enemy = enemy;
			view = GetComponentInChildren<SpaceShipView>();
			view.Initialize(this, weaponSlotsCount, moduleSlotsCount);
		}

		public void AddModule(Module module)
		{
			if (module != null)
				modules.Add(module);
		}

		public void RemoveModule(Module module)
		{
			if (module != null)
				modules.Remove(module);
		}

		public void AddWeapon(Weapon weapon)
		{
			if (weapon != null)
				weapons.Add(weapon);
		}

		public void RemoveWeapon(Weapon weapon)
		{
			if (weapon != null)
				weapons.Remove(weapon);
		}

		private void StartBattle()
		{
			float reloadMultiplayerInPercent = 0;
			float shieldRecoveryMultiplayerInPercent = 0;
			for (int i = 0; i < modules.Count; i++)
			{
				baseLife += modules[i].AdditionalLife;
				baseShield += modules[i].AdditionalLife;
				reloadMultiplayerInPercent += modules[i].IncreaseReload;
				shieldRecoveryMultiplayerInPercent += modules[i].ShieldRecovery;
			}

			shieldRecovery = baseShildRecoverySeconds - baseShildRecoverySeconds / 100F * shieldRecoveryMultiplayerInPercent;
			reloadMultiplayer = 1F + 1F / 100F * reloadMultiplayerInPercent;

			for (int i = 0; i < weapons.Count; i++)
				weapons[i].Setup(reloadMultiplayer);

			battle = true;

			shieldRecoveryTimer = shieldRecovery;

			view.ShowStats();
		}

		private void GetDamage(int damage)
		{
			baseShield -= damage;
			if (baseShield < 0F)
			{
				baseLife -= Mathf.Abs(baseShield);
				baseShield = 0;
				if (baseLife <= 0)
					onBattleEnd?.Invoke(enemy.ShipName);
			}

			view.UpdateLife(baseLife);
			view.UpdateShield(baseShield);
		}

		private float shieldRecoveryTimer;

		private void Update()
		{
			if (battle)
			{
				int damage = 0;
				for (int i = 0; i < weapons.Count; i++)
					damage += weapons[i].Fire();

				enemy.GetDamage(damage);

				if (shieldRecoveryTimer <= 0F)
				{
					baseShield++;
					shieldRecoveryTimer = shieldRecovery;
					view.UpdateShield(baseShield);
				}
				else
					shieldRecoveryTimer -= Time.deltaTime;

			}
		}
	}
}
