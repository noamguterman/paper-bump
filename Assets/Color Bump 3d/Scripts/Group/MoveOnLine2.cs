using UnityEngine;

public class MoveOnLine2 : MonoBehaviour
{
	public Vector3 deltaA;

	public Vector3 deltaB;

	public float speed = 2f;

	public iTween.LoopType loopType = iTween.LoopType.pingPong;

	public iTween.EaseType easyType = iTween.EaseType.easeInOutSine;

	public float delay;

	[Header("")]
	public float distanceToPlayer = -1f;

	public float distanceToCamera = -1f;

	private Vector3 pointA;

	private Vector3 pointB;

	private Rigidbody rb;

	private GameObject temp;

	private void Start()
	{
		pointA = base.transform.position + deltaA;
		pointB = base.transform.position + deltaB;
		rb = GetComponent<Rigidbody>();
        temp = new GameObject("_Temp");
		temp.transform.position = base.transform.position;
		temp.hideFlags = HideFlags.HideInHierarchy;
	}

	private void Update()
	{
		if (distanceToCamera != -1f)
		{
			Vector3 position = base.transform.position;
			float z = position.z;
			Vector3 position2 = Camera.main.transform.position;
			if (z - position2.z < distanceToCamera)
			{
				goto IL_008c;
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
			goto IL_008c;
		}
		return;
		IL_008c:
		OnStart();
	}

	private void OnStart()
	{
		if (temp == null)
		{
			base.enabled = false;
			return;
		}
		if (!CUtils.EqualVector3(base.transform.position, pointA))
		{
			iTween.MoveTo(temp, iTween.Hash("position", pointA, "speed", speed, "easeType", easyType, "oncomplete", "OnMoveToPointComplete", "oncompletetarget", base.gameObject, "onupdatetarget", base.gameObject, "onupdate", "OnUpdate"));
		}
		else
		{
			OnMoveToPointComplete();
		}
		base.enabled = false;
	}

	private void OnMoveToPointComplete()
	{
		iTween.MoveTo(temp, iTween.Hash("position", pointB, "looptype", loopType, "speed", speed, "easeType", easyType, "onupdatetarget", base.gameObject, "onupdate", "OnUpdate", "delay", delay));
	}

	private void OnUpdate()
	{
		rb.MovePosition(temp.transform.position);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Plane"))
		{
			UnityEngine.Object.Destroy(temp);
		}
	}
}
