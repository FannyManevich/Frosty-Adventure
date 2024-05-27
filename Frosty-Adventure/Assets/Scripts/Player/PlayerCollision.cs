using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCollisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
    // if (collision.transform.tag == "Enemy")
    // {
    //   HealthManager.health--;
    // if (HealthManager.health <= 0)
    //  {
    UIManager.isGameOver = true;
    gameObject.SetActive(false);
    }
    //  else
    // {
    // StartCoroutine(GetHurt());
    // }
    // }
    // }


}
