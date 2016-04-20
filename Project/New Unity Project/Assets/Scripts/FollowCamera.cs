using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    private Vector3 offset;

    public Transform target;
	public float smoothing;

	private void Start()
    {
        Initialize();
	}

	private void FixedUpdate()
    {
        ChangePosition();
	}

    private void ChangePosition()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    private void Initialize()
    {
        smoothing = 5f;
        offset = transform.position - target.position;
    }
}
