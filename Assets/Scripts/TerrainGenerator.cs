using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private TerrainData _terrainData;
    [SerializeField] private TextureData _textureData;

    private float _minHeight;
    private float _maxHeight;

    private const int CHUNK_SIZE = 64;

	private void Start()
	{
		Generate();
	}

	private void Generate()
	{
		int heightMapX = _terrainData.HeightMap.width;
		int heightMapY = _terrainData.HeightMap.height;

		for (int x = 0; x <= heightMapX - CHUNK_SIZE; x += CHUNK_SIZE - 1)
		{
			for (int z = 0; z <= heightMapY - CHUNK_SIZE; z += CHUNK_SIZE - 1)
			{
				ConstructChunk(x, z);
			}
		}

		_textureData.InitializeMaterial(_terrainData.SurfaceMaterial, _minHeight, _maxHeight);
	}

	private void ConstructChunk(int offsetX, int offsetZ)
	{
		GameObject chunkObject = new GameObject("Map Chunk");
		MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = chunkObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();

		List<Vector3> verts = new List<Vector3>();
		List<int> tris = new List<int>();

		for(int x = 0; x < CHUNK_SIZE; x++)
		{
			for (int z = 0; z < CHUNK_SIZE; z++)
			{
				float surfaceHeight = _terrainData.HeightMap
					.GetPixel(x + offsetX, z + offsetZ).grayscale;
				surfaceHeight *= _terrainData.UniformScale;
				verts.Add(new Vector3(x, surfaceHeight, z));
				if (x == 0 || z == 0)
					continue;

				tris.Add(CHUNK_SIZE * x + z);
				tris.Add(CHUNK_SIZE * x + z - 1);
				tris.Add(CHUNK_SIZE * (x - 1) + z - 1);

				tris.Add(CHUNK_SIZE * (x - 1) + z - 1);
				tris.Add(CHUNK_SIZE * (x - 1) + z);
				tris.Add(CHUNK_SIZE * x + z);

				_maxHeight = Mathf.Max(surfaceHeight, _maxHeight);
				_minHeight = Mathf.Min(surfaceHeight, _minHeight);
			}
		}

		int[] triangles = tris.ToArray();
		Vector3[] flatShadedVerts = new Vector3[triangles.Length];
		for (int i = 0; i < triangles.Length; i++)
		{
			flatShadedVerts[i] = verts[triangles[i]];
			triangles[i] = i;
		}

		mesh.vertices = flatShadedVerts;
		mesh.triangles = triangles;
		mesh.RecalculateBounds();

		meshFilter.mesh = mesh;
		meshRenderer.sharedMaterial = _terrainData.SurfaceMaterial;
		chunkObject.transform.position = new Vector3(offsetX, 0, offsetZ);
	}
}
