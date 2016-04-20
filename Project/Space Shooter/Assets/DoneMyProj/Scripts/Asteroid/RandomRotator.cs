using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{

    private Rigidbody rigidbodyComponent;

    public float  tumble;

	private void Start()
	{
        Initialize();  
	}

    private void Initialize()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        rigidbodyComponent.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
