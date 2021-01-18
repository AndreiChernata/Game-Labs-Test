using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThirdTask
{
	public class SpaceShipView : MonoBehaviour
	{
		private static UISlot slotPrefab;
		private static UISlot SlotPrefab
		{
			get
			{
				if (slotPrefab == null)
					slotPrefab = Resources.Load<UISlot>("Prefabs/UI/UISlot");

				return slotPrefab;
			}
		}

		private List<UISlot> weaponSlots;
		private List<UISlot> moduleSlots;

		[SerializeField] private Transform weaponSlotParent = null;
		[SerializeField] private Transform moduleSlotParent = null;

		private System.Action<ISlotItem, ISlotItem> onWeaponChanged;
		private System.Action<ISlotItem, ISlotItem> onModuleChanged;

		private List<ISlotItem> allWeapons;
		private List<ISlotItem> allModules;

		private List<int> occupyWeaponsIndexes = new List<int>();
		private List<int> occupyModulesIndexes = new List<int>();

		private SpaceShip ship;

		[SerializeField] private Text lifeCount;
		[SerializeField] private Text shieldCount;

		public void Initialize(SpaceShip ship, int weaponSlotsCount, int moduleSlotsCount)
		{
			weaponSlots = new List<UISlot>();
			moduleSlots = new List<UISlot>();

			this.ship = ship;

			onWeaponChanged += OnWeaponChanged;
			onModuleChanged += OnModuleChanged;

			allWeapons = MockDataLayer.WeaponsData;
			allModules = MockDataLayer.ModulesData;

			for (int i = 0; i < weaponSlotsCount; i++)
			{
				UISlot slot = Instantiate(SlotPrefab, weaponSlotParent);
				slot.Initialize(onWeaponChanged, allWeapons, occupyWeaponsIndexes);
				weaponSlots.Add(slot);
			}

			for (int i = 0; i < moduleSlotsCount; i++)
			{
				UISlot slot = Instantiate(SlotPrefab, moduleSlotParent);
				slot.Initialize(onModuleChanged, allModules, occupyModulesIndexes);
				moduleSlots.Add(slot);
			}
		}

		private void OnWeaponChanged(ISlotItem selectedItem, ISlotItem previusItem)
		{
			OnSlotChanged(weaponSlots, selectedItem, previusItem);
		}

		private void OnModuleChanged(ISlotItem selectedItem, ISlotItem previusItem)
		{
			OnSlotChanged(moduleSlots, selectedItem, previusItem);
		}

		private void OnSlotChanged(List<UISlot> currentList, ISlotItem selectedItem, ISlotItem previusItem)
		{
			if (selectedItem != null)
				selectedItem.AddToShip(ship);
			if (previusItem != null)
				previusItem.RemoveFromShip(ship);
		}

		public void UpdateLife(int life)
		{
			lifeCount.text = life.ToString();
		}

		public void UpdateShield(int shield)
		{
			shieldCount.text = shield.ToString();
		}

		public void ShowStats()
		{
			lifeCount.gameObject.SetActive(true);
			shieldCount.gameObject.SetActive(true);
		}
	}
}
