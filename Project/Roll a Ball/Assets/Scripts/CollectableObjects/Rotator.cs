using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    private GameObject displayManager;
    private int lastPointsByCollect;
    private string nameOfGround;

    public Transform pickParticle;
    public Color objectColor;
    public Vector3 rotationDirection;

    public float speedOfObject;
    public int pointsByCollect;
    public string playerTag;
    
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        ColorOfObject();
        RotatingObject();
    }
    /// <summary>
    /// Check collision with other object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        { 
            Instantiate(pickParticle, transform.position, transform.rotation);
            displayManager.GetComponent<DisplayTextManager>().SetCount(pointsByCollect);
            DeactivateObject();
        }
    }
    /// <summary>
    /// Deactivate object in hirearchy 
    /// </summary>
    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Set charachters an object by parameters
    /// </summary>
    /// <param name="objColor"></param>
    /// <param name="pointsForCollect"></param>
    public void SetCharactersOfObject(Color objColor, int pointsForCollect)
    {
        objectColor = objColor;
        gameObject.GetComponent<Renderer>().material.color = objectColor;
        pointsByCollect = pointsForCollect;
        lastPointsByCollect = pointsByCollect;
    }
    /// <summary>
    /// Rotating an object every frame
    /// </summary>
    private void RotatingObject()
    {
        transform.Rotate(rotationDirection * speedOfObject * Time.deltaTime);
    }
    /// <summary>
    /// Set color if it will be change
    /// </summary>
    private void ColorOfObject()
    {
        if (gameObject.GetComponent<Renderer>().material.color.Equals(Color.red))
            pointsByCollect = 5;
        else
            pointsByCollect = lastPointsByCollect;
        gameObject.GetComponent<Renderer>().material.color = objectColor;
    }
    /// <summary>
    /// Initializing all variables
    /// </summary>
    private void Initialize()
    {
        nameOfGround = "Ground";
        playerTag = "Player";
        displayManager = GameObject.Find(nameOfGround);
    }
}
