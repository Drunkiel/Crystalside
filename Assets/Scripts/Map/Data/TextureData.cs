using UnityEngine;

[CreateAssetMenu(menuName = "Map generator/TextureData")]
public class TextureData : UpdatableData
{
	public Color[] baseColours;
	[Range(0, 20)]
	public float[] baseStartHeights;
	private string[] layers = new string[6] { "First", "Second", "Third", "Fourth", "Fifth", "Sixth" };

	float savedMinHeight;
	float savedMaxHeight;

	public void ApplyToMaterial(Material material)
	{
		for (int i = 0; i < layers.Length; i++)
		{
            material.SetColor("_" + layers[i] + "_Layer_Color", baseColours[i]);
        }

        UpdateMeshHeights(material, savedMinHeight, savedMaxHeight);
	}

	public void UpdateMeshHeights(Material material, float minHeight, float maxHeight)
	{
        savedMinHeight = minHeight;
        savedMaxHeight = maxHeight;

        for (int i = 0; i < layers.Length; i++)
        {
            material.SetFloat("_" + (i + 1) + "LayerHeight", baseStartHeights[i]);
        }
    }
}
