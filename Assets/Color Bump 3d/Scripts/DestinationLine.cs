using UnityEngine;

public class DestinationLine : MonoBehaviour
{
    public GameObject[] finishEffect;
	private void Update()
	{
		Vector3 position = PlayerController.instance.transform.position;
		float z = position.z;
		Vector3 position2 = base.transform.position;
		if (z > position2.z)
		{
			Vector3 position3 = PlayerController.instance.transform.position;
			if (position3.y > -1f)
			{
				GameController.instance.GameComplete();
                for(int i=0;i< finishEffect.Length; i++)
                {
                    finishEffect[i].SetActive(true);
                }
                base.enabled = false;
			}
		}
		if (GameController.instance.isGameOver)
		{
			Vector3 position4 = GameController.instance.virtualPlayer.transform.position;
			float z2 = position4.z;
			Vector3 position5 = base.transform.position;
			if (z2 > position5.z - 10f)
			{
				GameController.instance.virtualPlayer.GetComponent<ConstantForce>().enabled = false;
				base.enabled = false;
			}
		}
	}
}
