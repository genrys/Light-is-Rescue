using UnityEngine;
using System.Collections;

public class DeleteParticleSystem : MonoBehaviour {

	private IEnumerator Start(){

		yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);

		Destroy (gameObject);

	}




}
