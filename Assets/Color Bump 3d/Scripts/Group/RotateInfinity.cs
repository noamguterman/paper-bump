using UnityEngine;

public class RotateInfinity : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y,
        Z
    }

    public Axis rotAxis = Axis.X;

	public float speed = 100f;

	private Vector3 m_EulerAngleVelocity;

	private Rigidbody rb;

	private bool isEnabled = true;

	private void Start()
	{
        switch (rotAxis)
        {
            case Axis.X:
                m_EulerAngleVelocity = new Vector3(speed, 0f, 0f);
                break;
            case Axis.Y:
                m_EulerAngleVelocity = new Vector3(0f, speed, 0f);
                break;
            case Axis.Z:
                m_EulerAngleVelocity = new Vector3(0f, 0f, speed);
                break;
        }
		
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (isEnabled)
		{
			Quaternion rhs = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
			rb.MoveRotation(rb.rotation * rhs);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag("Plane"))
		{
			isEnabled = false;
		}
	}
}
