using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	public float speed;

	public float maxVeclocity;

	public float constanceForce;

	public Material colorMaterial;
    public Material ballMaterial;

	public GameObject spherePieces;

	public Camera fixedCamera;

	private Rigidbody rb;

	private Vector3 lastPoint;

	private Plane plane;

	private bool isButtonDown;

	private bool started;
    private bool isGrab;
    private bool isGrabAnim;

	public static PlayerController instance;

    private int sceneNumber;
    

	private void Awake()
	{
		instance = this;

        string name = SceneManager.GetActiveScene().name;
        sceneNumber = int.Parse(name.Split('_')[1]);

        if (sceneNumber == 4 && PlayerPrefs.GetInt("isShownThropies", 0) == 1)
        {
            GameObject.Find("Thropies_Panel").SetActive(false);
        }
    }

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		plane = new Plane(Vector3.up, Vector3.zero);
		speed *= 1.3f;
	}

	private void FixedUpdate()
	{
		if (!started)
		{
			return;
		}
		if (isButtonDown)
		{
			Ray ray = fixedCamera.ScreenPointToRay(Input.mousePosition);
			if (plane.Raycast(ray, out float enter))
			{
				Vector3 point = ray.GetPoint(enter);
				Vector3 a = point - lastPoint;
				rb.AddForce(a * speed);
				lastPoint = point;
			}
		}
		rb.AddForce(Vector3.forward * constanceForce);
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVeclocity);
	}

	public void Dead()
	{
		base.gameObject.SetActive(false);
		GameObject gameObject = Instantiate(spherePieces, base.transform.position, Quaternion.identity);
        rb.Sleep();
		//Vector3 force = rb.velocity * 10f;
		//IEnumerator enumerator = gameObject.transform.GetEnumerator();
		//try
		//{
		//	while (enumerator.MoveNext())
		//	{
		//		Transform transform = (Transform)enumerator.Current;
		//		transform.GetComponent<Rigidbody>().AddForce(force);
		//	}
		//}
		//finally
		//{
		//	IDisposable disposable;
		//	if ((disposable = (enumerator as IDisposable)) != null)
		//	{
		//		disposable.Dispose();
		//	}
		//}
		GameController.instance.GameOver();
        SoundManager.Instance.PlayGameOverSFX();
	}

	public void StartPlaying()
	{
		started = true;
		GameController.instance.StartPlaying();
	}

    public void GrabPaper()
    {
        StartCoroutine(GrabAnim());
    }

    IEnumerator GrabAnim()
    {
        transform.GetComponent<Animator>().SetTrigger("Grab");
        SoundManager.Instance.PlayPaperGrabSFX();
        isGrabAnim = true;

        yield return new WaitForSeconds(0.5f);
        isGrab = true;
        StartPlaying();
    }

    public void FlatPaper()
    {
        transform.localEulerAngles = new Vector3(-90, 0, 0);
        transform.GetComponent<Animator>().SetTrigger("Flat");
        SoundManager.Instance.PlayPaperGrabSFX();
        rb.Sleep();
    }


    public void ResetPlaying()
    {
        isButtonDown = false;
        lastPoint = Vector3.zero;
    }

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
            if(sceneNumber == 4 && PlayerPrefs.GetInt("isShownThropies", 0) == 0)
            {
                PlayerPrefs.SetInt("isShownThropies", 1);
                GameObject.Find("Thropies_Panel").SetActive(false);
            }
            else
            {
                if (isGrab == false && isGrabAnim == false)
                {
                    GrabPaper();
                }
                //else if (!started && !CUtils.IsPointerOverUIObject() && isGrab == true)
                //{
                //             StartPlaying();
                //}
                Ray ray = fixedCamera.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out float enter))
                {
                    lastPoint = ray.GetPoint(enter);
                }
                isButtonDown = true;
            }
		}
		else if (Input.GetMouseButtonUp(0))
		{
			isButtonDown = false;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			Dead();
		}
		Vector3 position = base.transform.position;
		if (position.y < -6f)
		{
			Dead();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Obstacle"))
		{
			MeshRenderer meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = collision.gameObject.GetComponentInChildren<MeshRenderer>();
			}
			//if (meshRenderer != null && meshRenderer.sharedMaterial == colorMaterial && !Prefs.PlayerNeverDie)
            if (meshRenderer != null && meshRenderer.sharedMaterial != ballMaterial && !Prefs.PlayerNeverDie)
            {
				Dead();
				Sound.instance.Play(Sound.Others.HitObject);
			}
            else
            {
                SoundManager.Instance.PlayObstacleSFX();
            }
		}
	}
}
