using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GET : MonoBehaviour
{
	public Color[] BaseColors;
	[Range(0, 1)] public float[] BaseHeights;

	[SerializeField] private int _count;
	[SerializeField] private Color _start;
	[SerializeField] private Color _end;

	private void OnValidate()
	{
		BaseHeights = new float[_count];
		BaseColors = new Color[_count];
		for (int i = 0; i < _count; i++)
		{
			BaseHeights[i] = (float)i / _count;
			BaseColors[i] = Color.Lerp(_start, _end, BaseHeights[i]);
		}
	}
}
