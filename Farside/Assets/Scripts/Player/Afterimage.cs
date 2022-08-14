using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    //implements dash & afterimage (utilises pooling)

    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet;
    private float alphaMultiplier = 0.85f;

    private Transform playerTransform;
    private SpriteRenderer sr;
    private SpriteRenderer playersr;

    //[SerializeField]
    //private PlayerMain player;

    private Color color;

    private void OnEnable() 
    {
        sr = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<PlayerMain>().transform;
        playersr = playerTransform.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        sr.sprite = playersr.sprite;
        transform.position = playerTransform.position;
        transform.rotation = playerTransform.rotation;
        timeActivated = Time.time;

    }

    private void Update() 
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        sr.color = color;

        if (Time.time >= (timeActivated + activeTime)) {
            //instead of destroying gameobject, add to pool
            AfterimagePool.Instance.AddToPool(gameObject);
        }
    }
}
