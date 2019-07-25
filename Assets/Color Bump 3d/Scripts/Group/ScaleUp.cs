using UnityEngine;

public class ScaleUp : MonoBehaviour
{
	public bool scaleX;

	public float scaleXValue;

	public bool scaleY;

	public float scaleYValue;

	public bool scaleZ;

	public float scaleZValue;

	public float time = 0.2f;

	public float distanceToCamera = -1f;

	public float distanceToPlayer = -1f;

	private void Start()
	{
		GetComponent<Rigidbody>().isKinematic = true;
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
		Vector3 localScale = base.transform.localScale;
		if (scaleX)
		{
			localScale.x = scaleXValue;
		}
		if (scaleY)
		{
			localScale.y = scaleYValue;
		}
		if (scaleZ)
		{
			localScale.z = scaleZValue;
		}
		GetComponent<Collider>().enabled = false;
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", localScale, "time", time, "oncomplete", "OnComplete"));
		base.enabled = false;
	}

	private void OnComplete()
	{
		GetComponent<Collider>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
	}
}
