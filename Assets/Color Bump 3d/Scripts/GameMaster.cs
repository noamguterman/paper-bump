using UnityEngine;

public class GameMaster : MonoBehaviour
{
	public static GameMaster instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
