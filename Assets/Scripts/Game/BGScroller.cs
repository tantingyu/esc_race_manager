using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    private float tileSizeX;

    private Vector2 startOffset;
    private GameObject camParent;
    private Vector2 camOffset;

    void Start()
    {
        camParent = transform.parent.gameObject;
        tileSizeX= GetComponent<SpriteRenderer>().bounds.size.x;
        startOffset = transform.position;
        camOffset = camParent.transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
        transform.position = camOffset + Vector2.right * newPosition;
    }
}