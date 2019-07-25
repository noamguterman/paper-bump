using System;

public class YesNoDialog : Dialog
{
	public Action onYesClick;

	public Action onNoClick;

	public virtual void OnYesClick()
	{
		onYesClick();
		Sound.instance.PlayButton();
		Close();
	}

	public virtual void OnNoClick()
	{
		onNoClick();
		Sound.instance.PlayButton();
		Close();
	}
}
