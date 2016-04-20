using UnityEngine;
using System.Collections;

public class InstantiatePickUp : MonoBehaviour
{
	private GameObject[] collectableObjects;

    private float groundBorder;
    private string pathToPrefabOfCollectObject;

	public int numberOfFirstTypeGameObjects;
    public int numberOfSecondTypeGameObjects;

    private void Start()
	{
        Initialize();
        InstantiateObjects();
	}

    /// <summary>
    /// Instatntiate all collectable objects
    /// </summary>
    private void InstantiateObjects()
    {
        InstantiatingByType(0, numberOfFirstTypeGameObjects, Color.green, 2);
        InstantiatingByType(numberOfFirstTypeGameObjects, numberOfFirstTypeGameObjects + numberOfSecondTypeGameObjects, Color.blue, 1);
    }
   /// <summary>
   /// Instantiate object by set parameters
   /// </summary>
   /// <param name="startIndex"></param>
   /// <param name="numberOfObjects"></param>
   /// <param name="objectColor"></param>
   /// <param name="numberOfPoints"></param>
    private void InstantiatingByType(int startIndex, int numberOfObjects, Color objectColor, int numberOfPoints)
    {
        {
            for (int i = startIndex; i < numberOfObjects; i++)
            {
                collectableObjects[i] = (GameObject)Instantiate(Resources.Load(pathToPrefabOfCollectObject), new Vector3(Random.Range(-groundBorder, groundBorder), 0.5f, Random.Range(-groundBorder, groundBorder)), Quaternion.identity);
                collectableObjects[i].GetComponent<Rotator>().SetCharactersOfObject(objectColor, numberOfPoints);
            }
        }
    }
    /// <summary>
    /// Initialize method
    /// </summary>
	private void Initialize()
    {
        groundBorder = 6f;
        pathToPrefabOfCollectObject = "Prefabs/CollectableObject";
        collectableObjects = new GameObject[numberOfFirstTypeGameObjects + numberOfSecondTypeGameObjects];
    }
}
