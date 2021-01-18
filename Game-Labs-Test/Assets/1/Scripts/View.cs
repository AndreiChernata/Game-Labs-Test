using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FirstTast
{
	public class View : MonoBehaviour
	{
		[SerializeField] private InputField x;
		[SerializeField] private InputField y;

		[SerializeField] private FlagMeshGenerator generator;
		[SerializeField] private Toggle isCPU;

		public void CreateNewMesh()
		{
			int xValue = -1;
			int yValue = -1;

			int.TryParse(x.text, out xValue);
			int.TryParse(y.text, out yValue);

			if (xValue > 0 && yValue > 0)
				generator.Create(xValue, yValue, isCPU.isOn);
			else
				Debug.LogError("Incorrect input value");
		}
	}
}
