using UnityEngine;

public class MoveOnLine4 : MonoBehaviour
{
	public float fromX;

	public float toX;

	public float speed = 0.7f;

	private Vector3 pointA;

	private Vector3 pointB;

    [Header("")]
    public float distanceToPlayer = -1f;
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
        transform.GetComponent<Renderer>().enabled = false;
	}

    private void OnStart()
    {
        if (!CUtils.EqualVector3(base.transform.position, pointA))
        {
            iTween.MoveTo(base.gameObject, iTween.Hash("position", pointA, "speed", speed, "easeType", iTween.EaseType.linear, "oncomplete", "OnMoveToPointComplete"));
        }
        else
        {
            OnMoveToPointComplete();
        }
        base.enabled = false;
        transform.GetComponent<Renderer>().enabled = true;
    }

    private void Update()
    {
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
    private void OnMoveToPointComplete()
	{
		iTween.MoveTo(base.gameObject, iTween.Hash("position", pointB, "looptype", "loop", "speed", speed, "easeType", iTween.EaseType.linear));
	}
}
