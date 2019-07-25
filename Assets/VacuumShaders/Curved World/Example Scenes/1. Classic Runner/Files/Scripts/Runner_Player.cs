//VacuumShaders 2014
// https://www.facebook.com/VacuumShaders

using UnityEngine;
using System.Collections;

namespace VacuumShaders.CurvedWorld.Example
{
    public class Runner_Player : MonoBehaviour
    {

        [SerializeField]private VirtualJoystick virtualJoystick;

        Vector3 movement;
        [SerializeField] private Transform childObj;
        private Vector3 windDirection;

        public Transform fanObj;

        public bool inWindZone = false;
        public GameObject windZone;
        Rigidbody rb;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float windSpeed;

        void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {
            rb = transform.GetComponent<Rigidbody>();
            Physics.gravity = new Vector3(0, -50, 0);
        }

        void OnDisable()
        {
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }

        float directX = 0;
        float directY = 0;
        float directX1 = 0;
        float directY1 = 0;

        private void FixedUpdate()
        {
            childObj.transform.position = new Vector3(fanObj.position.x, childObj.position.y, transform.position.z + 3);

            directX1 = Mathf.Lerp(directX1, virtualJoystick.InputDirection.x * moveSpeed, Time.fixedDeltaTime * 30);
            directY1 = Mathf.Lerp(directY1, virtualJoystick.InputDirection.y * moveSpeed, Time.fixedDeltaTime * 30);
            if (inWindZone)
            {
                windDirection = Vector3.Normalize(childObj.transform.position - transform.position);
            }
            else
            {
                windDirection = Vector3.zero;
            }

            directX = directX1 - windDirection.x * windSpeed;
            directY = directY1 - windDirection.z * windSpeed;

            Move(directX, directY);
            Turning(directX1, directY1);
        }

        void Move(float h, float v)
        {
            movement.Set(h, 0f, v);
            movement *= Time.fixedDeltaTime;
            rb.MovePosition(transform.position + movement);
        }

        void Turning(float h, float v)
        {
            Vector3 turnDir = new Vector3(virtualJoystick.InputDirection.x, 0f, virtualJoystick.InputDirection.y);

            transform.RotateAroundLocal(Vector3.right, Time.fixedDeltaTime * moveSpeed);
            transform.RotateAroundLocal(Vector3.right, v * 1.5f * Time.fixedDeltaTime);
            transform.RotateAroundLocal(Vector3.back, h * 1.5f * Time.fixedDeltaTime);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("enemy"))
                return;
            Vector3 force = (Vector3.forward + Vector3.up + Random.insideUnitSphere).normalized * Random.Range(1, 1.5f);
            collision.rigidbody.AddForce(force, ForceMode.Impulse);

            Runner_Car car = collision.gameObject.GetComponent<Runner_Car>();
            if (car != null)
            {
                car.speed = 1;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "windArea")
            {
                windZone = other.gameObject;
                inWindZone = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "windArea")
            {
                inWindZone = false;
            }
        }
    }
}