using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MedSpawn : MonoBehaviour
{
	public static MedSpawn Instance{ get; private set;}

    private GameObject player;

    private float[] timerOfMedChest;

    public GameObject[] medChest;
	public Transform[] medSpawnPoint;

    public int numberOfMedChest;
	public int medSpawnPointIndex;
    public string pathToMedPrefab;

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
		for (int i = 0; i < numberOfMedChest; i++)
        {
			if (medChest [i].activeInHierarchy == false)
            {
				timerOfMedChest[i] -= Time.deltaTime;
				if (timerOfMedChest[i] <= 0f)
                {
					medChest[i].GetComponent<MedicineChest> ().Activating ();
					medChest [i].SetActive(true);
					timerOfMedChest [i] = 20f; 
				}
                else
					return;
			return;
			}
		}
	}

	private void InstantiateBattery()
    {
		for (int i = 0; i < numberOfMedChest; i++)
        {
			medSpawnPointIndex = Random.Range (0, medSpawnPoint.Length);
			medChest[i] = (GameObject)Instantiate(Resources.Load(pathToMedPrefab), medSpawnPoint[medSpawnPointIndex].position, medSpawnPoint[medSpawnPointIndex].rotation);
			medChest[i].SetActive (false);
		}
	}

    private void Initialize()
    {
        Instance = this;
        pathToMedPrefab = "Prefabs/MedicineChest";
        medChest = new GameObject[numberOfMedChest];
        timerOfMedChest = new float[numberOfMedChest];
        InstantiateBattery();
        for (int j = 0; j < numberOfMedChest; j++)
            timerOfMedChest[j] = 20f;

    }
}
