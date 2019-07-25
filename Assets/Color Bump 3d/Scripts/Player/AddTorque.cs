using UnityEngine;

public class AddTorque : MonoBehaviour
{
	public float distanceToPlayer = 7f;

	public Vector3 torque;

	private void Start()
	{
		GetComponent<Rigidbody>().mass = 0.01f;
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		float z = position.z;
		Vector3 position2 = PlayerController.instance.transform.position;
		if (z - position2.z < distanceToPlayer)
		{
			base.enabled = false;
			GetComponent<Rigidbody>().AddTorque(torque);
		}
	}
}
