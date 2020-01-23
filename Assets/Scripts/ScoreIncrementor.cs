using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreIncrementor : MonoBehaviour
{
    public int Value = 1;
    private bool _triggered = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_triggered && !GameControl.Instance.GameOver && collision.GetComponent<Bird>() != null)
        {
            GameControl.Instance.IncrementScore(Value);
            _triggered = true;
        }
    }

    public void Reset()
    {
        _triggered = false;
    }
}
