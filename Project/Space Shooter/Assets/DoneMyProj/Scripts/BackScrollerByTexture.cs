using UnityEngine;
using System.Collections;

public class BackScrollerByTexture : MonoBehaviour
{
    #region firstBackGroundMethod  variables
    private Vector2 offsetOfTexture;
    private Vector2 savedOffsetRepeaterMethod;
    private Renderer rendererComponentOfTextureMethod;
    private bool isRepeater;

    public GameObject backGround;
    public Vector3 backGroundPosition;

    public float backGroundScrollSpeed;
    public float yTextureOffset;
    #endregion

    #region secondBackGroundMethod variables
    private Vector3 startPosition;
    private Vector2 offsetTextureOfRelocateMethod;
    private Vector2 savedOffsetRelocateMethod;
    private Renderer rendererComponentOfRelocateMethod;

    private float newPositionOfbackGround;
    private bool isRelocator;
    private float xTextureOffset;

    public GameObject secondBackgroundImage;

    public float backGroundRelocateScrollSpeed;
    public float tileSizeZ;
    #endregion

    private void Start()
    {
        InitializeByTextureMethod();
        InitializeByRelocatorMethod();
    }

    private void Update()
    {
        SwitchScrollingMethod();
    }

    private void SwitchScrollingMethod()
    {
        if (isRepeater)
        {
            TextureRepeater();
        } else if (isRelocator)
        {
            RelocateScroller();
        }

        if (Input.GetKeyDown(KeyCode.C) && isRepeater)
        {
            methodChooser(false, true, true);
        }
        else if (Input.GetKeyDown(KeyCode.C) && isRelocator)
        {
            methodChooser(true, false, false);
        }
    }

    private void methodChooser(bool repeater, bool relocator, bool secondBackImage)
    {
        isRepeater = repeater;
        isRelocator = relocator;
        backGround.transform.localPosition = backGroundPosition;
        secondBackgroundImage.SetActive(secondBackImage);
    }

    private void TextureRepeater()
    {
        yTextureOffset = Mathf.Repeat(Time.time * backGroundScrollSpeed, 1);
        offsetOfTexture = new Vector2(savedOffsetRepeaterMethod.x, yTextureOffset);
        rendererComponentOfTextureMethod.sharedMaterial.SetTextureOffset("_MainTex", offsetOfTexture);
    }

    private void RelocateScroller()
    {
        xTextureOffset = Mathf.Repeat(Time.time * backGroundRelocateScrollSpeed, tileSizeZ);
        xTextureOffset = xTextureOffset / tileSizeZ;
        xTextureOffset = Mathf.Floor(xTextureOffset);
        offsetTextureOfRelocateMethod = new Vector2(xTextureOffset, savedOffsetRelocateMethod.y);
        rendererComponentOfRelocateMethod.sharedMaterial.SetTextureOffset("_MainTex", offsetTextureOfRelocateMethod);
        newPositionOfbackGround = Mathf.Repeat(Time.time * backGroundRelocateScrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.back * newPositionOfbackGround;
    }

    private void InitializeByTextureMethod()
    {
        rendererComponentOfTextureMethod = GetComponent<Renderer>();
        backGroundPosition = backGround.transform.localPosition;
        savedOffsetRepeaterMethod = rendererComponentOfTextureMethod.sharedMaterial.GetTextureOffset("_MainTex");
        backGroundScrollSpeed = 0.1f;
        isRepeater = true;
    }

    private void InitializeByRelocatorMethod()
    {
        rendererComponentOfRelocateMethod = GetComponent<Renderer>();
        startPosition = transform.position;
        savedOffsetRelocateMethod = rendererComponentOfRelocateMethod.sharedMaterial.GetTextureOffset("_MainTex");
        isRelocator = false;
    }
}
