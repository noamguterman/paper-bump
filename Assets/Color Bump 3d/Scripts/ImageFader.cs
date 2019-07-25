using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
	public void Fade(float from, float to, float time)
	{
		Image component = GetComponent<Image>();
		component.canvasRenderer.SetAlpha(from);
		component.CrossFadeAlpha(to, time, ignoreTimeScale: true);
	}
}
