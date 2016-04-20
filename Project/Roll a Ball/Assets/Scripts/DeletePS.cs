using UnityEngine;
using System.Collections;

public class DeletePS : MonoBehaviour {

	private IEnumerator Start(){

		yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
		Destroy (gameObject);

	}
}
