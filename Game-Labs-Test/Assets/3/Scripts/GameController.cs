using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdTask
{
	public class GameController : MonoBehaviour
	{
		private System.Action onBattleStart;
		private System.Action<string> onBattleEnd;

		[SerializeField] private SpaceShip firstShip;
		[SerializeField] private SpaceShip secondShip;

		[SerializeField] private GameControllerView view;

		private void Start()
		{
			firstShip.Initialize(ref onBattleStart, ref onBattleEnd, secondShip);
			secondShip.Initialize(ref onBattleStart, ref onBattleEnd, firstShip);
			view.Initialize(ref onBattleStart, ref onBattleEnd);
		}

	}
}
