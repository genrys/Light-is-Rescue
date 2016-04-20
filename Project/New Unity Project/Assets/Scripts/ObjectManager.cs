using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance { get; private set; }

    private GameObject manager;
    private AudioSource objAudio;

    private string batteryTag;
    private string medChestTag;
    private string playerTag;

    protected GameObject player;
    
    private void Update()
    {
        Initialize();
    }

    protected void ChooseObject(GameObject pickUpObject, AudioClip objAudioClip, Transform objParticleSys)
    {
        if (gameObject.CompareTag(medChestTag))
        {
            PlayerHealth.Instance.plusMedChestHealth = true;
        } else if (gameObject.CompareTag(batteryTag)) {
            Flashlight.numOfBatterys++;
        }
        TurnEffects(objAudioClip, objParticleSys);
    }

    private void TurnEffects(AudioClip objAudioClip, Transform objParticleSys)
    {
        objAudio.PlayOneShot(objAudioClip);
        Instantiate(objParticleSys, transform.position, transform.rotation);
    }

    public void ActivatingObject(GameObject spawnObject)
    {
        //spawnObject.GetComponent<ObjectSpawn>().objSpawnPointIndex = Random.Range(0, spawnObject.GetComponent<ObjectSpawn>().objSpawnPoint.Length);
        //transform.position = spawnObject.GetComponent<ObjectSpawn>().objSpawnPoint[spawnObject.GetComponent<ObjectSpawn>().objSpawnPointIndex].position;
        //Debug.Log(" spawn " + objectSpawnScript.objSpawnPointIndex + " on " + objectSpawnScript.objSpawnPoint[objectSpawnScript.objSpawnPointIndex]);
    }

    protected void DeactivatingObject(GameObject other)
    {
        gameObject.SetActive(false);
    }

    private void Initialize()
    {
        playerTag = "Player";
        player = GameObject.FindGameObjectWithTag(playerTag);       
        objAudio = GetComponent<AudioSource>();
        batteryTag = "Battery";
        medChestTag = "MedChest";
        Instance = this;
    }   
}
