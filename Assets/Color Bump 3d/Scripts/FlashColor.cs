using System.Collections;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
	public float time = 0.2f;

	public float distanceToCamera = -1f;

	public float distanceToPlayer = -1f;

	private Material obstacleMaterial;

	private Material defaultMaterial;

	private int index;

	private void Start()
	{
		obstacleMaterial = GameController.instance.obstacleMaterial;
		defaultMaterial = GameController.instance.defaultMaterial;
	}

	private void Update()
	{
		if (distanceToCamera != -1f)
		{
			Vector3 position = base.transform.position;
			float z = position.z;
			Vector3 position2 = Camera.main.transform.transform.position;
			if (z - position2.z < distanceToCamera)
			{
				goto IL_0091;
			}
		}
		if (distanceToPlayer != -1f)
		{
			Vector3 position3 = base.transform.position;
			float z2 = position3.z;
			Vector3 position4 = PlayerController.instance.transform.position;
			if (!(z2 - position4.z < distanceToPlayer))
			{
				return;
			}
			goto IL_0091;
		}
		return;
		IL_0091:
		base.enabled = false;
		StartCoroutine(DoJob());
	}

	private IEnumerator DoJob()
	{
		while (true)
		{
			ChangeMaterial((index % 2 != 0) ? defaultMaterial : obstacleMaterial);
			index++;
			yield return new WaitForSeconds(time);
		}
	}

	private void ChangeMaterial(Material material)
	{
		MeshRenderer[] componentsInChildren = GetComponentsInChildren<MeshRenderer>();
		MeshRenderer[] array = componentsInChildren;
		foreach (MeshRenderer meshRenderer in array)
		{
			meshRenderer.material = material;
		}
	}
}
