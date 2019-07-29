using UnityEngine;
using System.Collections;

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
        distance = Mathf.Clamp(distance, 7, 15);
        float zoom = 60 * 7 / distance;
        zoom = Mathf.Clamp(zoom, 47, 60);

        transform.GetChild(0).GetComponent<Camera>().fieldOfView = Mathf.Lerp(transform.GetChild(0).GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * 15);
        
	}

    public void LookatPlayer()
    {
        isFinished = true;
        iTween.MoveTo(gameObject, iTween.Hash("z", player.transform.position.z + offset.z * 2, "time", 2));
        //iTween.MoveTo(transform.GetChild(0).gameObject, iTween.Hash("fieldOfView", 60, "time", 1.5f));

        Hashtable ht = new Hashtable();
        ht.Add("from", transform.GetChild(0).GetComponent<Camera>().fieldOfView);
        ht.Add("to", 60f);
        ht.Add("time", 2f);
        ht.Add("onupdate", "ChangeView");
        ht.Add("easyType", "easeOutExpo");
        iTween.ValueTo(gameObject, ht);

    }

    private void ChangeView(float newValue)
    {
        transform.GetChild(0).GetComponent<Camera>().fieldOfView = newValue;
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
