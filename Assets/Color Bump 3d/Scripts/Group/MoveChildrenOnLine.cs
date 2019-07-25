using System;
using System.Collections;
using UnityEngine;

public class MoveChildrenOnLine : MonoBehaviour
{
	public Vector3 deltaA;

	public Vector3 deltaB;

	public float speed = 0.7f;

	public iTween.LoopType loopType = iTween.LoopType.pingPong;

	public iTween.EaseType easyType = iTween.EaseType.linear;

	public float distanceToPlayer;

	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				MoveOnLine2 moveOnLine = transform.gameObject.AddComponent<MoveOnLine2>();
				moveOnLine.deltaA = deltaA;
				moveOnLine.deltaB = deltaB;
				moveOnLine.speed = speed;
				moveOnLine.loopType = loopType;
				moveOnLine.easyType = easyType;
				moveOnLine.distanceToPlayer = distanceToPlayer;
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}
}
