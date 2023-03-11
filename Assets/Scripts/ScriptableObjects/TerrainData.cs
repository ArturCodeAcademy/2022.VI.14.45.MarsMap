using UnityEngine;

[CreateAssetMenu(fileName = nameof(TerrainData))]
public class TerrainData : ScriptableObject
{
	public float UniformScale;
	public Texture2D HeightMap;
	public Material SurfaceMaterial;
}
