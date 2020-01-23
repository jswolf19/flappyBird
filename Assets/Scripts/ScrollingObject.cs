using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
	private Rigidbody2D _body;

    // Start is called before the first frame update
	void Start()
    {
		_body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
    {
        _body.velocity = GameControl.Instance.ScrollVelocity;
    }
}
