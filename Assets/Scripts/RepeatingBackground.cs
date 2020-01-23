using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D _groundCollider;
    private float GroundHorizontalLength
    {
        get
        {
            return _groundCollider.size.x;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _groundCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -GroundHorizontalLength)
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        Vector2 offset = new Vector2(GroundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
