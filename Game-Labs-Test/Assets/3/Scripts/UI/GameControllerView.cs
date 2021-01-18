using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThirdTask
{
	public class GameControllerView : MonoBehaviour
	{
		[SerializeField] private Button battleStart;

		[SerializeField] private Text winText;
		[SerializeField] private Button reloadButton;
		private System.Action onBattleStart;

		public void Initialize(ref System.Action onBattleStart, ref System.Action<string> onBattleEnd)
		{
			battleStart.onClick.AddListener(HideStartButton);
			onBattleEnd += ShowResult;
			reloadButton.onClick.AddListener(ReloadScene);
			this.onBattleStart = onBattleStart;
		}

		private void HideStartButton()
		{
			battleStart.gameObject.SetActive(false);
			onBattleStart?.Invoke();
		}

		private void ShowResult(string winner)
		{
			winText.gameObject.SetActive(true);
			reloadButton.gameObject.SetActive(true);
			winText.text = "Winner: " + winner;
		}

		private void ReloadScene()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("3");
		}
	}
}