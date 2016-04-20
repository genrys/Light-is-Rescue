using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour
{
    public static Battery Instance { get; private set; }

	private GameObject player;
	private string playerTag;

	private void Awake()
    {
        Initialize();
	}

	private void OnTriggerEnter(Collider other)
    {
        TakeTheBattery(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
    {
		Deactivating (other.gameObject);
	}

    private void TakeTheBattery(GameObject other)
    {
        if (other.gameObject == player)
            Flashlight.numOfBatterys++;
    }

	public void Activating()
    {
		BatterySpawn.Instance.batSpawnPointIndex = Random.Range (0, BatterySpawn.Instance.batterySpawnPoint.Length);
		transform.position = BatterySpawn.Instance.batterySpawnPoint [BatterySpawn.Instance.batSpawnPointIndex].position;
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
        Instance = this;
    }
}
