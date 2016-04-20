using UnityEngine;
using System.Collections;

static class objectsTags
{
    public static string floorTag = "Floor";
    public static string safezoneTag = "SafeZone";
    public static string indahouseTag = "InDaHouse";
    public static string stepTag = "Step";
    public static string playerRunTrigger = "Run";
    public static string horAxisName = "Horizontal";
    public static string vertAxisName = "Vertical";
    public static string enemyTag = "Enemy";
    public static string batteryTag = "Battery";
    public static string medChestTag = "MedChest";
}

public class PlayerCharachter : MonoBehaviour
{
    public static PlayerCharachter Instance { get; private set; }

    private Animator playerAnim;
    private Vector3 move;
    private Vector3 movePlayerToMouse;
    private Rigidbody playerRB;
    private Ray camRayToGetMousePosition;

    private GameObject safeZone;
    private GameObject inDaHouse;
    private GameObject step;
    private GameObject enemyObject;
    
    private int floorMask;
    private float camRayLength;
    private float speed;

    public Camera fpCamera;
    public EnemyMovement enemyMovement;

    public bool isSafeZone;
	public bool firstPersonCamera;
    
    private void Awake()
    {
        Initialize();
	}

    private void FixedUpdate()
    {
        PLayerMovementByCharachter();
    }

    private void PLayerMovementByCharachter()
    {
        float horizontal = Input.GetAxisRaw(objectsTags.horAxisName);
        float v = Input.GetAxisRaw(objectsTags.vertAxisName);
        if (!firstPersonCamera)
        {
            Movement(horizontal, v);
            MoveByCursor();
            Animating(horizontal, v);
        }
        else
        {
            fpCharacter();
        }
    }

    private void Movement(float horCoord, float vertCoord)
    {
		move.Set (horCoord, 0, vertCoord);
		move = move.normalized * speed * Time.deltaTime;
		playerRB.MovePosition (transform.position + move);
	}

    private void MoveByCursor()
    {
        GetMousePosition();
        MoveToMousePosition(camRayToGetMousePosition);
        RotateToMouse(movePlayerToMouse);
    }

    public void GetMousePosition()
    {
        camRayToGetMousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void MoveToMousePosition(Ray camRay)
    {
        RaycastHit floorHitToGetMousePosition;
        if (Physics.Raycast(camRay, out floorHitToGetMousePosition, camRayLength, floorMask))
        {
            movePlayerToMouse = floorHitToGetMousePosition.point - transform.position;
            movePlayerToMouse.y = 0f;
        }
    }

    private void RotateToMouse(Vector3 playerToMouse)
    {
        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
        playerRB.MoveRotation(newRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckIsSafeZone(other.gameObject, false, true);
        CheckIsHouse(other.gameObject, true);
        CheckIsStep(other.gameObject, 0.6f, false);
	}

    private void OnTriggerExit(Collider other)
    {
        CheckIsSafeZone(other.gameObject, true, false);
        CheckIsHouse(other.gameObject, false);
        CheckIsStep(other.gameObject, 0.0f, false);
	}
    /*
    private void CheckPickUpObject(typeObject type)
    {
        switch (type)
        {
            case typeObject.battery:
                break;
            case typeObject.medChest:
                break;
       }
    }
    */
    private void CheckIsSafeZone(GameObject other, bool isActivateNavigation, bool isSafeZoneActive)
    {
        if (other.gameObject == safeZone)
        {
            enemyMovement.NavigationActivating(isActivateNavigation);
            isSafeZone = isSafeZoneActive;
        }
    }

    private void CheckIsHouse(GameObject other, bool inTheHouse)
    {
        if (other.gameObject == inDaHouse)
        {
            fpCamera.GetComponent<Camera>().enabled = inTheHouse;
            GetComponent<MouseLook>().enabled = inTheHouse;
            firstPersonCamera = inTheHouse;
            enemyMovement.NavigationActivating(!inTheHouse);
            isSafeZone = inTheHouse;
        }
    }

    private void CheckIsStep(GameObject other, float newPosition, bool isAlreadyStep)
    {
        if (other.gameObject == step)
        {
            if (!isAlreadyStep && !isSafeZone)
            transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);    
        }
    }

    private void Animating (float horizontal, float vertical)
	{
		bool run = horizontal != 0f || vertical != 0f;
		playerAnim.SetBool (objectsTags.playerRunTrigger, run);
	}

    private void fpCharacter()
    {
		transform.rotation = Quaternion.Euler (0, GetComponent<MouseLook>().curYrotation, 0);
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
			playerAnim.SetBool (objectsTags.playerRunTrigger, true);
        else
			playerAnim.SetBool (objectsTags.playerRunTrigger, false);

		if (Input.GetKey (KeyCode.W))
			transform.position += transform.forward / 10;
		if (Input.GetKey (KeyCode.S))
            transform.position -= transform.forward / 10;
		if (Input.GetKey (KeyCode.D))
			transform.position += transform.right / 10;
		if (Input.GetKey (KeyCode.A)) 
			transform.position -= transform.right / 10;
	}

    private void Initialize()
    {
        floorMask = LayerMask.GetMask(objectsTags.floorTag);
        safeZone = GameObject.FindGameObjectWithTag(objectsTags.safezoneTag);
        inDaHouse = GameObject.FindGameObjectWithTag(objectsTags.indahouseTag);
        step = GameObject.FindGameObjectWithTag(objectsTags.stepTag);
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        isSafeZone = false;
        firstPersonCamera = false;
        camRayLength = 100f;
        speed = 6f;
        Instance = this;
    }
}