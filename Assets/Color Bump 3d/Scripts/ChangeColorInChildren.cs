using System.Collections;
using UnityEngine;

public class ChangeColorInChildren : MonoBehaviour
{
	public int numObjects = 1;

	public float time = 0.3f;

	public int currentIndex;

	public float distanceToCamera = 20f;

	private Material obstacleMaterial;

	private Material defaultMaterial;

	private void Start()
	{
		obstacleMaterial = GameController.instance.obstacleMaterial;
		defaultMaterial = GameController.instance.defaultMaterial;
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		float z = position.z;
		Vector3 position2 = Camera.main.transform.position;
		if (z - position2.z < distanceToCamera)
		{
			base.enabled = false;
			StartCoroutine(DoJob());
		}
	}

	private IEnumerator DoJob()
	{
		numObjects = Mathf.Clamp(numObjects, 0, base.transform.childCount);
		while (true)
		{
			for (int i = 0; i < base.transform.childCount; i++)
			{
				ChangeMaterial(i, defaultMaterial);
			}
			for (int j = currentIndex; j < currentIndex + numObjects; j++)
			{
				ChangeMaterial(j % base.transform.childCount, obstacleMaterial);
			}
			currentIndex = (currentIndex + numObjects) % base.transform.childCount;
			yield return new WaitForSeconds(time);
		}
	}

	private void ChangeMaterial(int index, Material material)
	{
		MeshRenderer[] componentsInChildren = base.transform.GetChild(index).GetComponentsInChildren<MeshRenderer>();
		MeshRenderer[] array = componentsInChildren;
		foreach (MeshRenderer meshRenderer in array)
		{
			meshRenderer.material = material;
		}
	}
}
