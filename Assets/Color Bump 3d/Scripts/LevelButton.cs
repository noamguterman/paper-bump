using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
	private int index;

	private void Start()
	{
		index = base.transform.GetSiblingIndex();
		base.transform.GetChild(0).GetComponent<Text>().text = (index + 1).ToString();
	}

	public void OnClick()
	{
		SceneManager.LoadScene("Level_" + (index + 1));
	}
}
