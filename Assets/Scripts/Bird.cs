using UnityEngine;

public class Bird : MonoBehaviour
{
    public float VForce = 200f;
    public float HForce = 200f;

    private bool _isDead;
    private Rigidbody2D _body;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _isDead = false;
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isDead)
        {
            HandleInput();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("you died!");

        _body.velocity = Vector2.zero;
        _isDead = true;
        _animator.SetTrigger("Die");
        GameControl.Instance.BirdDied();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Flap(new Vector2(-HForce, VForce/2));
        } else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Flap(new Vector2(HForce, VForce/2));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Flap(new Vector2(0, VForce));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Flap(new Vector2(0, -VForce/2));
        }

    }

    private void Flap(Vector2 force)
    {
        _body.velocity = Vector2.zero;
        _body.AddForce(force);
        _animator.SetTrigger("Flap");
    }
}
