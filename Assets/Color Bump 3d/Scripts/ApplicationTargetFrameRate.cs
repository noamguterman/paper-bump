using UnityEngine;

public class ApplicationTargetFrameRate : MonoBehaviour
{
	private void Start()
	{
		Application.targetFrameRate = 60;
	}

	private void Update()
	{
	}
}
