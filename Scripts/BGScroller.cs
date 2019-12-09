using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    [SerializeField] private GameObject bgSpeedObject;
    private GameController bgSpeed;
    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;

    void Start()
    {
        scrollSpeed = -1;
        startPosition = transform.position;
        bgSpeed = bgSpeedObject.GetComponent<GameController>();
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
        if (bgSpeed.CurrentScore >= 200)
        {
            scrollSpeed = -75;
        }
    }
}