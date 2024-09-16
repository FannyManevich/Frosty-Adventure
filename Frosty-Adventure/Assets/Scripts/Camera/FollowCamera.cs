using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform leftWall;
    public Transform rightWall;
    public Transform bottomWall;
    public Transform topWall;
    public float FollowSpeed = 2f;
 
    public Transform target;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 newPos = ClampTargetPosition(target.position + offset);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
    Vector3 ClampTargetPosition(Vector3 targetPos)
    {
        targetPos.x = Mathf.Clamp(targetPos.x, leftWall.position.x + 0f, rightWall.position.x - 0f);
        targetPos.y = Mathf.Clamp(targetPos.y, bottomWall.position.y + 6f, topWall.position.y - 5f);
        return targetPos;
    }
}