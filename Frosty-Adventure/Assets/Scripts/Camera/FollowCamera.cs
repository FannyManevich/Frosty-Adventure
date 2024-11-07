using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] SpriteRenderer background;
    public Transform leftWall;
    public Transform rightWall;
    public Transform bottomWall;
    public Transform topWall;
    public Vector2 xBounds;
    public Vector2 yBounds;
    public float FollowSpeed = 2f;

    Camera cam;
    public Transform target;
    public Vector3 offset;
    
    private void Start()
    {
        cam = GetComponent<Camera>();
        var cameraHeight = cam.orthographicSize;
        var cameraWidth = cameraHeight * cam.aspect;
        xBounds = new Vector2(background.bounds.min.x + cameraWidth, background.bounds.max.x - cameraWidth);
        yBounds = new Vector2(background.bounds.min.y + cameraHeight, background.bounds.max.y - cameraHeight);
    }
    void LateUpdate()
    {
        Vector3 newPos = ClampTargetPosition(target.position);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
    Vector3 ClampTargetPosition(Vector3 targetPos)
    {
        targetPos.x = Mathf.Clamp(targetPos.x, xBounds.x, xBounds.y);
        targetPos.y = Mathf.Clamp(targetPos.y, yBounds.x, yBounds.y);
        return targetPos;
    }
}