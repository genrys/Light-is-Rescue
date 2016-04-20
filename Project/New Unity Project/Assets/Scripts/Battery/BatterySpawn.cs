using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BatterySpawn : MonoBehaviour
{
	public static BatterySpawn Instance{ get; private set;}

    private float[] timerOfBattery;
    private string pathToPrefabBattery;

    public GameObject[] battery;
	public Transform[] batterySpawnPoint;
   
    public int numberOfBattery;
	public int batSpawnPointIndex;
    public bool getBattery;
	
	private void Awake()
    {
        Initialize();
	}

	private void Update()
    {
		ActivateBatterys ();    
    }

	private void ActivateBatterys()
    {
		for (int i = 0; i < numberOfBattery; i++)
        {
			if (battery [i].activeInHierarchy == false)
            {
				timerOfBattery[i] -= Time.deltaTime;
				if (timerOfBattery[i] <= 0f)
                {
					battery [i].GetComponent<Battery> ().Activating ();
					battery [i].SetActive(true);
					timerOfBattery [i] = 5f; 
				} else
					return;
				return;
			}
		}
	}

	private void InstantiateBattery()
    {
		for (int i = 0; i < numberOfBattery; i++)
        {
			batSpawnPointIndex = Random.Range (0, batterySpawnPoint.Length);
			battery[i] = (GameObject)Instantiate(Resources.Load(pathToPrefabBattery), batterySpawnPoint[batSpawnPointIndex].position, batterySpawnPoint[batSpawnPointIndex].rotation);
			battery [i].SetActive (false);
		}
	}

    private void Initialize()
    {
        Instance = this;
        pathToPrefabBattery = "Prefabs/Battery";
        battery = new GameObject[numberOfBattery];
        timerOfBattery = new float[numberOfBattery];
        InstantiateBattery();
        for (int j = 0; j < numberOfBattery; j++)
            timerOfBattery[j] = 5f;
    }
}
