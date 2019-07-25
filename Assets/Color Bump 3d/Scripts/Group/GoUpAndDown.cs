using UnityEngine;

public class GoUpAndDown : MonoBehaviour
{
	public float goupValue = 3f;

	public float goupTime = 0.5f;

	public float goDownTime = 0.3f;

	public float distanceToPlayer = 12f;

	public float distanceToPlayerToGoDown = 0.5f;

	public bool fallByGravity;

	private bool isDone;

	private void Start()
	{
		GetComponent<Rigidbody>().isKinematic = true;
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		float z = position.z;
		Vector3 position2 = PlayerController.instance.transform.position;
		if (z - position2.z < distanceToPlayerToGoDown)
		{
			if (fallByGravity)
			{
				GetComponent<Rigidbody>().isKinematic = false;
			}
			else
			{
				iTween.MoveBy(base.gameObject, iTween.Hash("z", 0f - goupValue, "time", goDownTime));
			}
			base.enabled = false;
		}
		else if (!isDone)
		{
			Vector3 position3 = base.transform.position;
			float z2 = position3.z;
			Vector3 position4 = PlayerController.instance.transform.position;
			if (z2 - position4.z < distanceToPlayer)
			{
				isDone = true;
				iTween.MoveBy(base.gameObject, iTween.Hash("z", goupValue, "time", goupTime));
			}
		}
	}
}
