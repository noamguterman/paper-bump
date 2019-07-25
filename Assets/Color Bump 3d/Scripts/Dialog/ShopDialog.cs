using UnityEngine;
using UnityEngine.UI;

public class ShopDialog : MonoBehaviour
{
	public Text[] numRubyTexts;

	public Text[] priceTexts;

	protected void Start()
	{
	}

	public void OnBuyProduct(int index)
	{
		UnityEngine.Debug.LogError("Please enable, import and install Unity IAP to use this function");
	}
}
