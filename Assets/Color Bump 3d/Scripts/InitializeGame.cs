using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeGame : MonoBehaviour
{
	public Material obstacleMaterial;

	public Shader shaderStandard;

	public Shader legacySpectacular;

	private void Start()
	{
		obstacleMaterial.shader = legacySpectacular;
		int num = Mathf.Min(Prefs.UnlockedLevel, Const.TOTAL_LEVEL);
		SceneManager.LoadScene("Level_" + num);
	}
}
