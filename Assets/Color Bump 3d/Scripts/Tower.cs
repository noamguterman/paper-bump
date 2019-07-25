using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public GameObject[] parts;

	public float[] scaleYs;

	public float upTime = 0.2f;

	public float gapTime = 0.05f;

	public float distanceToCamera = 15f;

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
		int i = 0;
		GameObject[] array = parts;
		foreach (GameObject part in array)
		{
			iTween.ScaleTo(part, iTween.Hash("y", scaleYs[i], "time", upTime));
			yield return new WaitForSeconds(gapTime);
			i++;
		}
	}
}
