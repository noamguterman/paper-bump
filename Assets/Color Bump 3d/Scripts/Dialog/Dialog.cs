using System;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
	public Animator anim;

	public AnimationClip hidingAnimation;

	public Text title;

	public Text message;

	public Action<Dialog> onDialogOpened;

	public Action<Dialog> onDialogClosed;

	public Action onDialogCompleteClosed;

	public Action<Dialog> onButtonCloseClicked;

	public DialogType dialogType;

	public bool enableAd = true;

	public bool enableEscape = true;

	private AnimatorStateInfo info;

	private bool isShowing;

	protected virtual void Awake()
	{
		if (anim == null)
		{
			anim = GetComponent<Animator>();
		}
	}

	protected virtual void Start()
	{
		onDialogCompleteClosed = (Action)Delegate.Combine(onDialogCompleteClosed, new Action(OnDialogCompleteClosed));
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	private void Update()
	{
		if (enableEscape && UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}
	}

	public virtual void Show()
	{
		base.gameObject.SetActive(true);
		if (anim != null && IsIdle())
		{
			isShowing = true;
			anim.SetTrigger("show");
			onDialogOpened(this);
		}
		if (enableAd)
		{
			Timer.Schedule(this, 0.3f, delegate
			{
				CUtils.ShowInterstitialAd();
			});
		}
	}

	public virtual void Close()
	{
		if (isShowing)
		{
			isShowing = false;
			if (anim != null && IsIdle() && hidingAnimation != null)
			{
				anim.SetTrigger("hide");
				Timer.Schedule(this, hidingAnimation.length, DoClose);
			}
			else
			{
				DoClose();
			}
			onDialogClosed(this);
		}
	}

	private void DoClose()
	{
		UnityEngine.Object.Destroy(base.gameObject);
		if (onDialogCompleteClosed != null)
		{
			onDialogCompleteClosed();
		}
	}

	public void Hide()
	{
		base.gameObject.SetActive(false);
		isShowing = false;
	}

	public bool IsIdle()
	{
		info = anim.GetCurrentAnimatorStateInfo(0);
		return info.IsName("Idle");
	}

	public bool IsShowing()
	{
		return isShowing;
	}

	public virtual void OnDialogCompleteClosed()
	{
		onDialogCompleteClosed = (Action)Delegate.Remove(onDialogCompleteClosed, new Action(OnDialogCompleteClosed));
	}

	public void PlayButton()
	{
		Sound.instance.PlayButton();
	}
}
