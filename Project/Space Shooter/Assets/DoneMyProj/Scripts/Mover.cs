using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{

    public float speed;

	private void Start()
	{
        Initialize();
	}
    
    private void Initialize()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
