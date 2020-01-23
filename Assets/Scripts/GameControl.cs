using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
	public GameObject GameOverText;
    public Text ScoreText;
    public Vector2 ScrollVelocity = new Vector2(-1.5f, 0);

    public static GameControl Instance
	{
        get
		{
			return _instance;
		}
	}
    private static GameControl _instance;

    public bool GameOver
	{
        get
		{
			return _gameOver;
		}
        private set
		{
            _gameOver = value;
            GameOverText.SetActive(value);
            if(_gameOver)
            {
                ScrollVelocity = Vector2.zero;
            }
		}
	}
	private bool _gameOver = false;

    private int _score = 0;
    private string _scoreTextFormat;

    void Awake()
    {
        if(_instance == null)
		{
			_instance = this;
		} else if (_instance != this)
		{
			Destroy(gameObject);
        }
    }

    private void Start()
    {
        _scoreTextFormat = ScoreText.text;
        IncrementScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver && Input.GetKeyDown(KeyCode.UpArrow))
		{
            SceneManager.LoadScene("Main");
		}
    }

    public void BirdDied()
	{
		GameOver = true;
	}

    public void IncrementScore(int value)
    {
        _score += value;
        ScoreText.text = string.Format(_scoreTextFormat, _score);
    }
}
