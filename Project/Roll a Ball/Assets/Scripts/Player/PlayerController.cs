using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	private Rigidbody rigidBodyComponent;

    private float speed;
    private string horAxisName;
    private string vertAxisName;

    private void Start()
	{
		Initialize ();
	}

	private void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis (horAxisName);
		float moveVertical = Input.GetAxis (vertAxisName);

		Vector3 move = new Vector3 (moveHorizontal, 0f, moveVertical);

        rigidBodyComponent.AddForce (move * speed);
	}
    /// <summary>
    /// Initialize method
    /// </summary>
	private void Initialize()
	{
        horAxisName = "Horizontal";
        vertAxisName = "Vertical";
        speed = 10f;
        rigidBodyComponent = GetComponent<Rigidbody> ();
	}
}
