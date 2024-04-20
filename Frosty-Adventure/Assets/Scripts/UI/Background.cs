using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Background : MonoBehaviour
{
    [SerializeField]
   // private float parallaxSpeed;

    private float length, startPositionX;
    public RectTransform cameraRectTransform;
    public float parallaxEffect;
    private float spriteSizeX;


    void Start()
    {
        cameraRectTransform = Camera.main.GetComponent<RectTransform>();
        startPositionX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        spriteSizeX = GetComponent<SpriteRenderer>().bounds.size.x;

        //parallaxSpeed = 1.0f;
}


void FixedUpdate()
    {
        float temp = (cameraRectTransform.position.x * (1 - parallaxEffect));
        float distance = (cameraRectTransform.anchoredPosition.x * parallaxEffect);

        transform.position = new Vector3(startPositionX + distance, transform.position.y, transform.position.z);

        if (temp > startPositionX + length)
            startPositionX += length;
        else if (temp < startPositionX - length)
            startPositionX -= length;

       float relativeCameraDist = cameraRectTransform.position.x;
    }
}
