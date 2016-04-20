using UnityEngine;
using System.Collections;

public class PickUpObject : ObjectManager {

    public Transform objparticle;
    public AudioClip objAudioClip;

    private void OnTriggerEnter(Collider other)
    {
        CheckPickUp(other);
    }

    private void OnTriggerExit(Collider other)
    {
        DeactivatingObject(other.gameObject);
    }

    private void CheckPickUp(Collider other)
    {
        if (other.gameObject == player)
        {
            ChooseObject(gameObject, objAudioClip, objparticle);
        }
    }
}
