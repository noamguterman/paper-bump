using UnityEngine;

public class GoUp : MonoBehaviour
{
	public float goupValue;

	public float time = 0.2f;

	public float distanceToPlayer = 2f;

	private void Start()
	{
		GetComponent<Rigidbody>().isKinematic = true;
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		float z = position.z;
		Vector3 position2 = PlayerController.instance.transform.position;
		if (z - position2.z < distanceToPlayer)
		{
			iTween.MoveBy(base.gameObject, iTween.Hash("y", goupValue, "time", time, "oncomplete", "OnComplete"));
			base.enabled = false;
		}
	}

	private void OnComplete()
	{
		GetComponent<Rigidbody>().isKinematic = false;
	}
}
