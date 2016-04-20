using UnityEngine;
using System.Collections;


public class ObjectSpawn : MonoBehaviour
{
    public static ObjectSpawn Instance { get; private set; }

    private GameObject player;
    private float[] timerOfObject;

    public GameObject[] spawningObject;
    public Transform[] objSpawnPoint;
    public GameObject gamePrefab;

    public int timeForSpawn;
    public int numberOfObject;
    public int objSpawnPointIndex;

    private void Awake()
    {
        InitializeObject();
    }

    private void Update()
    {
        ActivateObjects();
    }

    private void ActivateObjects()
    {
        for (int i = 0; i < numberOfObject; i++)
        {
            if (spawningObject[i].activeInHierarchy == false)
            {
                timerOfObject[i] -= Time.deltaTime;
                if (timerOfObject[i] <= 0f)
                {
                    ChangePosition(spawningObject[i]);
                    spawningObject[i].SetActive(true);
                    timerOfObject[i] = timeForSpawn;
                }
                else
                    return;
                return;
            }
        }
    }

    public void ChangePosition(GameObject spawningObject)
    {
        objSpawnPointIndex = Random.Range(0, objSpawnPoint.Length);
        spawningObject.transform.position = objSpawnPoint[objSpawnPointIndex].position;
    }

    private void InstantiateObjects()
    {
        for (int i = 0; i < numberOfObject; i++)
        {
            objSpawnPointIndex = Random.Range(0, objSpawnPoint.Length);
            spawningObject[i] = (GameObject)Instantiate(gamePrefab, objSpawnPoint[objSpawnPointIndex].position, objSpawnPoint[objSpawnPointIndex].rotation);
            spawningObject[i].SetActive(false);
        }
    }

    private void InitializeObject()
    {
        Instance = this;
        spawningObject = new GameObject[numberOfObject];
        timerOfObject = new float[numberOfObject];
        InstantiateObjects();
        for (int j = 0; j < numberOfObject; j++)
            timerOfObject[j] = timeForSpawn;
    }
}
