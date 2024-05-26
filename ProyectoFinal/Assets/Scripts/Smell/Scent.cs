using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scent : MonoBehaviour
{
    public float timeToLive;
    public float decreaseFactor = 0.000001f;
    public Renderer renderer;
    private float elapsedTime;
    private bool alive = true;
    void Start()
    {
        elapsedTime = 0;
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (gameObject.transform.localScale.x <= 0 || elapsedTime >= timeToLive) {
                Destroy(gameObject);
                alive = false;
            }
            else
            {
                elapsedTime += Time.deltaTime;
                gameObject.transform.localScale -= new Vector3(decreaseFactor/ timeToLive, 0, decreaseFactor / timeToLive) * Time.deltaTime;
                Color color = renderer.material.color;
                color.g += 0.1f * Time.deltaTime;
                renderer.material.color = color;
            }
        }
    }
}
