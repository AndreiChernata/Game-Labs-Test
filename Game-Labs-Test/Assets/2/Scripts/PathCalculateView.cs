using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SecondTask
{
	public class PathCalculateView : MonoBehaviour
	{

		[SerializeField] private Dropdown dropdown1;
		[SerializeField] private Dropdown dropdown2;

		private System.Action<int, int> onButtonPress;
		private System.Action<int> calculateCallback;

		[SerializeField] private Text pathLengh;
		[SerializeField] private Text transitionCount;


		public void Initialize(string[] stationsNames, System.Action<int, int> onButtonPress, ref System.Action<Vector2> calculateCallback)
		{
			List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
			for (int i = 0; i < stationsNames.Length; i++)
			{
				Dropdown.OptionData optionData = new Dropdown.OptionData(stationsNames[i]);
				options.Add(optionData);
			}
			dropdown1.ClearOptions();
			dropdown2.ClearOptions();
			dropdown1.AddOptions(options);
			dropdown2.AddOptions(options);

			this.onButtonPress = onButtonPress;

			calculateCallback += ShowResult;
		}

		public void OnPressButton()
		{
			onButtonPress?.Invoke(dropdown1.value, dropdown2.value);
		}

		private void ShowResult(Vector2 value)
		{
			pathLengh.text = value.x.ToString();
			transitionCount.text = value.y.ToString();
		}


	}
}
