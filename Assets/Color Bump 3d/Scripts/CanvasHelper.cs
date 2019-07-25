using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasHelper : MonoBehaviour
{
	public float minAspect;

	public float maxAspect;

	public float minMatch;

	public float maxMatch;

	private CanvasScaler canvasScaler;

	private void Start()
	{
		canvasScaler = GetComponent<CanvasScaler>();
		Update();
	}

	private void Update()
	{
		float num = (float)Screen.width / (float)Screen.height;
		if (num > maxAspect)
		{
			canvasScaler.matchWidthOrHeight = maxMatch;
		}
		else if (num < minAspect)
		{
			canvasScaler.matchWidthOrHeight = minMatch;
		}
		else
		{
			canvasScaler.matchWidthOrHeight = (num - minAspect) / (maxAspect - minAspect) * (maxMatch - minMatch) + minMatch;
		}
	}
}
