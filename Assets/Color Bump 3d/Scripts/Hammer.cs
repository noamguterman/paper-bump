using System.Collections;
using UnityEngine;

public class Hammer : MonoBehaviour
{
	public int direction = -1;

	public float time = 0.5f;

	public float waitTime = 0.5f;

	public float distanceToCamera = 20f;

	private void Update()
	{
		Vector3 position = base.transform.position;
		float z = position.z;
		Vector3 position2 = Camera.main.transform.position;
		if (z - position2.z < distanceToCamera)
		{
			base.enabled = false;
			StartCoroutine(Rotate());
		}
	}

	private IEnumerator Rotate()
	{
		while (true)
		{
			iTween.RotateBy(base.gameObject, iTween.Hash("y", 0.5f * (float)direction, "time", time));
			Sound.instance.Play(Sound.Others.Rotate);
			yield return new WaitForSeconds(waitTime);
		}
	}
}
