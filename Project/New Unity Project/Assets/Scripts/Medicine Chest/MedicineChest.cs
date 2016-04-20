using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MedicineChest : MonoBehaviour
{
    public static MedicineChest Instance { get; private set; }

    private GameObject player;
    private AudioSource medAudio;

    public Transform medParticle;

    public bool plusMedChestHealth;
    public string playerTag;

	private void Awake()
    {
        Initialize();		
	}

	private void OnTriggerEnter(Collider other)
    {
        TakeTheChest(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
    {	
	    Deactivating (other.gameObject);
	}

    private void TakeTheChest(GameObject other)
    {
        if (other.gameObject == player)
        {
            plusMedChestHealth = true;
            medAudio.Play();
            Instantiate(medParticle, transform.position, transform.rotation);
        }
    }

	public void Activating()
    {
		MedSpawn.Instance.medSpawnPointIndex = Random.Range (0, MedSpawn.Instance.medSpawnPoint.Length);
		transform.position = MedSpawn.Instance.medSpawnPoint [MedSpawn.Instance.medSpawnPointIndex].position;
	}

	private void Deactivating(GameObject other)
    {
        if (other.gameObject == player)
            gameObject.SetActive (false);
	}

    private void Initialize()
    {
        playerTag = "Player";
        player = GameObject.FindGameObjectWithTag(playerTag);
        medAudio = GetComponent<AudioSource>();
        plusMedChestHealth = false;
        Instance = this;
    }
}
