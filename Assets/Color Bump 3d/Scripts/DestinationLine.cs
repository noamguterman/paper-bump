using UnityEngine;

public class DestinationLine : MonoBehaviour
{
    public GameObject[] finishEffect;
	private void Update()
	{
		Vector3 position = PlayerController.instance.transform.position;
		float z = position.z;
		Vector3 position2 = transform.position;
		if (z > position2.z)
		{
			if (position.y > -1f)
			{
				GameController.instance.GameComplete();
                for(int i=0;i< finishEffect.Length; i++)
                {
                    finishEffect[i].SetActive(true);
                }
                enabled = false;
			}
		}
		if (GameController.instance.isGameOver)
		{
			Vector3 position4 = GameController.instance.virtualPlayer.transform.position;
			float z2 = position4.z;
			if (z2 > position2.z - 10f)
			{
				GameController.instance.virtualPlayer.GetComponent<ConstantForce>().enabled = false;
				enabled = false;
			}
		}
	}
}
