using UnityEngine;

[CreateAssetMenu(fileName = nameof(TextureData))]
public class TextureData : ScriptableObject
{
	public Color[] BaseColors;
	[Range(0, 1)] public float[] BaseHeights;

	private Material _material;
	private float _minHeight;
	private float _maxHeight;

	public void UpdateMeshMaterial(Material material, float minHeight, float maxHeight)
	{
		material.SetColorArray(nameof(BaseColors), BaseColors);
		material.SetFloatArray(nameof(BaseHeights), BaseHeights);
		material.SetFloat(nameof(_minHeight), _minHeight);
		material.SetFloat(nameof(_maxHeight), _maxHeight);
	}

	public void InitializeMaterial(Material material, float minHeight, float maxHeight)
	{
		_material = material;
		_minHeight = minHeight;
		_maxHeight = maxHeight;
		_material.SetInt(nameof(BaseColors.Length), BaseColors.Length);
		UpdateMeshMaterial(material, minHeight, maxHeight);
	}
}
