using EZCameraShake;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player")]
	public GameObject player;
	public GameObject virtualPlayer;

	public CameraController mainCamera;
    public CameraShaker cameraShaker;

    [Header("Material")]
    public Material obstacleMaterial;
    public Material planeMaterial;
    public Material defaultMaterial;

    [Header("Start_Stop Line")]
    public Image handImage;
    public SpriteRenderer arrowLeft;
    public SpriteRenderer arrowRight;
    private bool arrowFading;

    public TextMeshPro stageStartText;
    public TextMeshPro stageEndText;

    public TextMeshPro trophiesText;

    public GameObject startLine;
    public GameObject destinationLine;

    [Header("UI")]
    public GameObject gameOverScreen;
    public GameObject upgradeScreen;
    public Text txt_currentLevel;
    public Text txt_nextLevel;
    public Image img_fill;
    [HideInInspector]
    public float completeProgress;

    private float distance;

	public GameObject levelButtonPrefab;

	public Transform levelContainer;

	public GameObject levelSelectorScreen;

	//public GameObject menuObject;
	public Toggle playerNeverDie;

	[Header("Color Manager")]
	public Color[] obstacleColors;

    [Header("Floor Color Manager")]
    public Color[] floorColors;

    [Header("Sky Color Manager")]
    public Color[] skyColors;


    [HideInInspector]	public bool isGameOver;
    [HideInInspector]   public bool isRevive = false;

    private int sceneNumber;

	public static GameController instance;

	private float lastEscapeTime = -10f;

    public GameObject curvedObj;
	private void Awake()
	{
		instance = this;
		Application.targetFrameRate = 60;
        GameObject obj = GameObject.Instantiate(curvedObj);
        obj.GetComponent<VacuumShaders.CurvedWorld.CurvedWorld_Controller>().pivotPoint = Camera.main.transform;
    }

	private void Start()
	{
		string name = SceneManager.GetActiveScene().name;
		sceneNumber = int.Parse(name.Split('_')[1]);

        stageStartText.text = "STAGE " + sceneNumber;
        stageEndText.text = "STAGE " + sceneNumber + " COMPLETED";
        txt_currentLevel.text = sceneNumber.ToString();
        txt_nextLevel.text = (sceneNumber + 1).ToString();

        if(sceneNumber >= 4)
        {
            int num = Mathf.Min(FindObjectOfType<AnimalManager>().GetAnimalIdx_ToShow(), 12);

            if(PlayerPrefs.GetInt("CollectAll", 0) == 1)
            {
                trophiesText.text = "TROPHIES 12 / 12";
            }
            else
            {
                trophiesText.text = "TROPHIES " + num.ToString() + "/12";
            }
        }
        else
        {
            trophiesText.text = "";
        }

        if (GameState.numPlayed != 0)
		{
			int num = UnityEngine.Random.Range(0, obstacleColors.Length);
			int num2 = 0;
			do
			{
				num2 = UnityEngine.Random.Range(0, obstacleColors.Length);
			}
			while (num == num2);
            //planeMaterial.color = obstacleColors[num];
            planeMaterial.color = floorColors[num2];
            obstacleMaterial.color = obstacleColors[num2];
            Camera.main.backgroundColor = skyColors[num2];
		}
		for (int i = 0; i < Const.TOTAL_LEVEL; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(levelButtonPrefab, levelContainer);
			gameObject.transform.localScale = Vector3.one;
		}
		//menuObject.SetActive(GameConfig.instance.enableTesting);
		playerNeverDie.isOn = Prefs.PlayerNeverDie;
		//Music.instance.PlayAMusic();
		GameState.numPlayed++;

        distance = Mathf.Abs(destinationLine.transform.position.z - startLine.transform.position.z);

        virtualPlayer.GetComponent<ConstantForce>().force = new Vector3(0,0,12);
    }

    public void ShowHand()
    {
        handImage.enabled = true;
    }

	public void StartPlaying()
	{
		virtualPlayer.GetComponent<ConstantForce>().enabled = true;
		handImage.CrossFadeAlpha(0f, 0.3f, true);
		arrowFading = true;
	}

	public void GameOver()
	{
		isGameOver = true;
        cameraShaker.ShakeOnce(2f, 1.2f, 0.1f, 2f);

        if(isRevive == false)
        {
            isRevive = true;

            virtualPlayer.GetComponent<ConstantForce>().force = Vector3.zero;
            //Music.instance.Pause();
            Timer.Schedule(this, 1.5f, delegate
            {
                virtualPlayer.GetComponent<ConstantForce>().force = Vector3.forward * 4f;
                gameOverScreen.SetActive(true);
                mainCamera.GameOver();
            });
            Timer.Schedule(this, 1.5f, delegate
            {
                virtualPlayer.GetComponent<ConstantForce>().enabled = false;
            });
        }
        else
        {
            Invoke("Replay", 3);
        }
	}

    public void ReviveGame()
    {
#if UNITY_EDITOR
        RemovePassedObstacles();
        player.transform.position = new Vector3(0, 0.38f, player.transform.position.z - 5);
        virtualPlayer.transform.position = player.transform.position;
        //Music.instance.Play();

        GameObject.Find("PlayerDeath(Clone)").SetActive(false);

        isGameOver = false;
        mainCamera.Revive();
        gameOverScreen.GetComponent<GameOverController>().HidePanel();

        Invoke("Play_Continue", 1);
#else
        AdsManager.Instance.ShowRewardedVideo("Revive");
#endif


    }

    void Play_Continue()
    {
        virtualPlayer.GetComponent<ConstantForce>().force = new Vector3(0, 0, 12);
        virtualPlayer.GetComponent<ConstantForce>().enabled = true;
        player.gameObject.SetActive(true);
        PlayerController.instance.ResetPlaying();
    }

    private void RemovePassedObstacles()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            if(obj.transform.position.z < (player.transform.position.z - 1))
            {
                obj.SetActive(false);
            }
        }
    }

    public void GameComplete()
	{
        PlayerController.instance.FlatPaper();

        PlayerController.instance.enabled = false;
		virtualPlayer.GetComponent<ConstantForce>().enabled = false;
		//Music.instance.Pause();
        SoundManager.Instance.PlayVictorySFX();

        mainCamera.LookatPlayer();

        if (Prefs.UnlockedLevel == sceneNumber)
		{
			Prefs.UnlockedLevel++;
		}

        if(sceneNumber < 4 || PlayerPrefs.GetInt("CollectAll", 0) == 1)
        {
            Timer.Schedule(this, 4f, delegate
            {
                NextLevel();
            });
        }
        else
        {
            Timer.Schedule(this, 3.5f, delegate
            {
                //virtualPlayer.GetComponent<ConstantForce>().force = Vector3.forward * 4f;
                upgradeScreen.SetActive(true);
                mainCamera.Complete();
                foreach(MoveOnLine4 mov in FindObjectsOfType<MoveOnLine4>())
                {
                    if(mov.gameObject.name.Contains("Paperplane"))
                    {
                        mov.gameObject.SetActive(false);
                    }
                }
                
            });
        }
	}

	public void NextLevel()
	{
        if (hasUniqueItem == true && PlayerPrefs.GetInt("UniqueItem_11", 0) == 1)
        {
            PlayerPrefs.SetInt("CollectAll", 1);
        }

		int num = Mathf.Min(Const.TOTAL_LEVEL, sceneNumber + 1);
        SceneManager.LoadScene("Level_" + num);
	}

	public void OnMenuOpen()
	{
		levelSelectorScreen.SetActive(true);
	}

	public void OnMenuClose()
	{
		levelSelectorScreen.SetActive(false);
	}

	public void RateGame()
	{
		CUtils.OpenStore();
	}

	public void Replay()
	{
        if(PlayerPrefs.GetInt("CollectAll", 0) == 0)
        {
            if (hasUniqueItem == true)
            {
                PlayerPrefs.SetInt("UniqueItem_" + uniqueIndex.ToString(), 0);
            }
        }
        
        Time.timeScale = 1;
		CUtils.ReloadScene();
	}

    private bool hasUniqueItem = false;
    private int uniqueIndex;
    public void HasUniqueItem(int uniqueIdx)
    {
        hasUniqueItem = true;
        uniqueIndex = uniqueIdx;
    }


    public void OnPlayerNeverDieValueChanged()
	{
		Prefs.PlayerNeverDie = playerNeverDie.isOn;
	}

	private void Update()
	{
		if (arrowFading)
		{
			Color color = arrowLeft.color;
			if (color.a < 0.0001f)
			{
				arrowFading = false;
			}
			else
			{
				Color color2 = arrowLeft.color;
				color2.a = Mathf.Lerp(color2.a, 0f, Time.deltaTime * 7f);
				arrowLeft.color = color2;
				arrowRight.color = color2;
			}
		}

        if(isGameOver == false)
        {
            float passDistance = Mathf.Max(player.transform.position.z - startLine.transform.position.z, 0);
            completeProgress = Mathf.Min(1, passDistance / distance);
            img_fill.fillAmount = completeProgress;
        }
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Time.time - lastEscapeTime < 4f)
			{
				Application.Quit();
				return;
			}
			Toast.instance.ShowMessage("Press back again to quit game", 2f);
			lastEscapeTime = Time.time;
		}
	}


    public void OnRVRewardReceived(string str)
    {
        if(str == "Revive")
        {
            RemovePassedObstacles();
            player.transform.position = new Vector3(0, 0.38f, player.transform.position.z - 5);
            virtualPlayer.transform.position = player.transform.position;
            //Music.instance.Play();

            GameObject.Find("PlayerDeath(Clone)").SetActive(false);

            isGameOver = false;
            mainCamera.Revive();
            gameOverScreen.GetComponent<GameOverController>().HidePanel();
            Invoke("Play_Continue", 1);
        }
    }

    public void OnRVRewardReceived_Fail(string str)
    {
        if (str == "Revive")
        {
            Replay();
        }
    }
}
