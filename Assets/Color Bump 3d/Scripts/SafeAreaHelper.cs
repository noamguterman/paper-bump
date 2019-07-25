using UnityEngine;

public class SafeAreaHelper : MonoBehaviour
{
	private RectTransform panel;

	private void Start()
	{
		panel = GetComponent<RectTransform>();
	}

	private void ApplySafeArea()
	{
		Vector2 position = Screen.safeArea.position;
		Vector2 anchorMax = Screen.safeArea.position + Screen.safeArea.size;
		position.x /= Screen.width;
		position.y /= Screen.height;
		anchorMax.x /= Screen.width;
		anchorMax.y /= Screen.height;
		panel.anchorMin = position;
		panel.anchorMax = anchorMax;
	}
}
