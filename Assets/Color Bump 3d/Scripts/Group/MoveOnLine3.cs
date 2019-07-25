using UnityEngine;

public class MoveOnLine3 : MonoBehaviour
{
	public float fromX;

	public float toX;

	public float speed = 0.7f;

	private Vector3 pointA;

	private Vector3 pointB;

	private void Start()
	{
		float x = fromX;
		Vector3 position = base.transform.position;
		float y = position.y;
		Vector3 position2 = base.transform.position;
		pointA = new Vector3(x, y, position2.z);
		float x2 = toX;
		Vector3 position3 = base.transform.position;
		float y2 = position3.y;
		Vector3 position4 = base.transform.position;
		pointB = new Vector3(x2, y2, position4.z);
		if (!CUtils.EqualVector3(base.transform.position, pointA))
		{
			iTween.MoveTo(base.gameObject, iTween.Hash("position", pointA, "speed", speed, "easeType", iTween.EaseType.linear, "oncomplete", "OnMoveToPointComplete"));
		}
		else
		{
			OnMoveToPointComplete();
		}
	}

	private void OnMoveToPointComplete()
	{
		iTween.MoveTo(base.gameObject, iTween.Hash("position", pointB, "looptype", "pingpong", "speed", speed, "easeType", iTween.EaseType.linear));
	}
}
