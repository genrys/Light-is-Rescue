using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{

	public float xMinValue;
	public float xMaxValue;
	public float zMinValue;
	public float zMaxValue;
	
}

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigidbodyComponent;
    private AudioSource fireAudio;

    private float timeToNextFire;
    private string horNameAxis;
    private string vertNameAxis;
    private string fireButton;

    public float speed;
	public float tiltOfPlayer;
	public float fireRate;

	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;


    private void Start()
    {
        Initialize();
    } 

    private void Update()
	{
        Fire();
	}

	private void FixedUpdate()
	{
		float moveHor = Input.GetAxis (horNameAxis);
		float moveVer = Input.GetAxis (vertNameAxis);

		Vector3 movement = new Vector3 (moveHor, 0f, moveVer);
        rigidbodyComponent.velocity = movement * speed;

		transform.position = new Vector3
		(
			Mathf.Clamp(rigidbodyComponent.position.x, boundary.xMinValue,boundary.xMaxValue),
			0.0f,
			Mathf.Clamp(rigidbodyComponent.position.z, boundary.zMinValue,boundary.zMaxValue)
		);

        rigidbodyComponent.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbodyComponent.velocity.x * -tiltOfPlayer);	
	}

    private void Fire()
    {
        if (Input.GetButton(fireButton) && Time.time > timeToNextFire)
        {
            timeToNextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            fireAudio.Play();
        }
    }

    private void Initialize()
    {
        horNameAxis = "Horizontal";
        vertNameAxis= "Vertical";
        fireButton = "Fire1";
        speed = 5f;
        tiltOfPlayer = 5f;
        fireRate = 0.3f;
        rigidbodyComponent = GetComponent<Rigidbody>();
        fireAudio = GetComponent<AudioSource>();
    }
}
