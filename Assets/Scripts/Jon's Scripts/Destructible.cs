using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int destroyValue;

    private float forceToDestroy;
    public static float destroyCountdown = 2.5f;

    ScoreHandler scoreHandler;


    private void Awake()
    {
        scoreHandler = FindObjectOfType<ScoreHandler>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= forceToDestroy)
        {
            Invoke("DestroyMe", 2.5f);
        }
    }



    void DestroyMe()
    {
        Destroy(this.gameObject);
        scoreHandler.UpdateScore(destroyValue);
    }
}
