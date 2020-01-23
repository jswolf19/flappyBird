using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnAdjustor : MonoBehaviour
{
    public float TopY
    {
        get
        {
            return gameObject.transform.Find("UpperColumn").position.y;
        }
        set
        {
            SetY(gameObject.transform.Find("UpperColumn"), value);
        }
    }
    public float BottomY
    {
        get
        {
            return gameObject.transform.Find("LowerColumn").position.y;
        }
        set
        {
            SetY(gameObject.transform.Find("LowerColumn"), value);
        }
    }

    public (float min, float max) Range;
    public float Speed = 0f;

    private Rigidbody2D _body;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Speed > 0 && TopY >= Range.max) || (Speed < 0 && TopY <= Range.min))
        {
            Speed = -Speed;
        }
        _body.velocity += new Vector2(0, Speed);
    }

    private void SetY(Transform t, float y)
    {
        t.localPosition = new Vector2(t.localPosition.x, y);
    }
}
