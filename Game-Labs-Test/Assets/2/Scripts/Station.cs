using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SecondTask
{
	public class Station : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer;
		[SerializeField] private UnityEngine.UI.Text stationName;


		public void Initialize(Color color, string name)
		{
			spriteRenderer.color = color;
			stationName.text = name;
		}

		public void BuildLine(Vector3 target, Color color)
		{
			GameObject newGameObject = new GameObject();
			newGameObject.transform.parent = transform;
			newGameObject.transform.localPosition = Vector3.zero;
			LineRenderer lineRenderer = newGameObject.AddComponent<LineRenderer>();
			lineRenderer.material = spriteRenderer.material;
			lineRenderer.startWidth = lineRenderer.endWidth = 0.2F;
			lineRenderer.positionCount = 2;
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, target);
			lineRenderer.startColor = lineRenderer.endColor = color;
		}
	}
}
