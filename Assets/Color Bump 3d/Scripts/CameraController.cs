using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject virtualPlayer;
    public GameObject player;

    [HideInInspector]
	public Vector3 offset;

    bool isFinished = false;
	public void Start()
	{
		offset = transform.position - virtualPlayer.transform.position;
	}

	private void LateUpdate()
	{
        if (isFinished == true)
            return;
		transform.position = virtualPlayer.transform.position + offset;

        float distance = Vector3.Distance(player.transform.position, virtualPlayer.transform.position);

	}

    public void GameOver()
    {
        isFinished = true;
        iTween.MoveTo(gameObject, iTween.Hash("x", -8f, "time", 1));
    }

    public void Complete()
    {
        isFinished = true;
        iTween.MoveTo(gameObject, iTween.Hash("x", 8f, "time", 1));
    }

    public void Revive()
    {
        iTween.MoveTo(gameObject, iTween.Hash("x", 0, "time", 1));
        Invoke("SetContinue", 1);
    }

    void SetContinue()
    {
        isFinished = false;
    }
}
