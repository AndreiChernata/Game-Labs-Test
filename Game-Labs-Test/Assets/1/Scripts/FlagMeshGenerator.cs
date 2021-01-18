using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstTast
{
	public class FlagMeshGenerator : MonoBehaviour
	{
		[SerializeField] private int width = 10;
		[SerializeField] private int height = 5;

		private int widthVericlesCount = 20;
		private int heightVerticlesCount = 10;

		private List<Vector3> vertices = new List<Vector3>();
		private List<Vector3> normals = new List<Vector3>();
		private List<Vector2> uvs = new List<Vector2>();
		private List<int> triangles = new List<int>();

		[SerializeField] private Material gpuAnimationMaterial;
		[SerializeField] private Material unlit;
		[SerializeField] private MeshRenderer meshRenderer;
		[SerializeField] private MeshFilter meshFilter;

		private Mesh mesh;

		public void Create(int width, int height, bool cpu)
		{
			widthVericlesCount = width;
			heightVerticlesCount = height;

			meshRenderer.sharedMaterial = cpu ? unlit : gpuAnimationMaterial;
			if (mesh != null)
				Clear();
			CreateNewFlag();

			if (cpu)
				StartCoroutine(AnimateMesh());
		}

		private void CreateNewFlag()
		{
			float wightStep = (float)width / widthVericlesCount;
			float heightStep = (float)height / heightVerticlesCount;
			for (float x = 0; x <= width; x += wightStep)
				for (float y = 0; y <= height; y += heightStep)
				{
					vertices.Add(new Vector3(x, y));
					uvs.Add(new Vector2(x / width, y / height));
					normals.Add(Vector3.forward);
				}


			for (int j = 0; j < heightVerticlesCount; j++)
				for (int i = 0; i < widthVericlesCount; i++)
				{
					triangles.Add(j + i * (heightVerticlesCount + 1));
					triangles.Add((j + 1) + i * (heightVerticlesCount + 1));
					triangles.Add(j + (i + 1) * (heightVerticlesCount + 1));



					triangles.Add(j + (i + 1) * (heightVerticlesCount + 1));
					triangles.Add((j + 1) + i * (heightVerticlesCount + 1));
					triangles.Add((j + 1) + (i + 1) * (heightVerticlesCount + 1));

				}


			mesh = new Mesh();

			mesh.vertices = vertices.ToArray();
			mesh.triangles = triangles.ToArray();
			mesh.uv = uvs.ToArray();
			mesh.normals = normals.ToArray();

			meshFilter.mesh = mesh;

			transform.position = Vector3.zero - new Vector3(width, height) / 2F;
		}

		private void Clear()
		{
			Destroy(mesh);
			vertices.Clear();
			normals.Clear();
			uvs.Clear();
			triangles.Clear();

			StopAllCoroutines();
		}

		[SerializeField] private float timeMultiplayer = 20F;
		private IEnumerator AnimateMesh()
		{
			while (true)
			{
				for (int i = 0; i < widthVericlesCount + 1; i++)
					for (int j = 0; j < heightVerticlesCount + 1; j++)
					{
						Vector3 current = vertices[i + j * (widthVericlesCount + 1)];
						current.z = Mathf.Lerp(current.z, Mathf.Sin(current.x - Time.realtimeSinceStartup * timeMultiplayer), current.x / (widthVericlesCount + 1));
						vertices[i + j * (widthVericlesCount + 1)] = current;
					}
				mesh.vertices = vertices.ToArray();

				yield return null;
			}
		}


	}
}
