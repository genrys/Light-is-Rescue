using UnityEngine;
using System.Collections;

public class DeleteParticle : MonoBehaviour {

	IEnumerator Start()
	{
		yield return new WaitForSeconds (GetComponent<ParticleSystem> ().duration/2.0f);
        Destroy(gameObject);
    }
}
