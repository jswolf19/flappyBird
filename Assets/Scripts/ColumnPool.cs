using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int PoolSize = 5;
    public float SpawnRate = 4f;
    public Vector2 SpawnXRange = new Vector2(10f, 10f);
    public Vector2 TopYRange;
    public Vector2 GapRange;
    public Vector2 SpeedRange;
    public Vector2Int ScoreRange = new Vector2Int(1, 4);
    public float OffscreenX = -1f;

    public GameObject Original;

    private GameObject[] _pool;
    private int _curIdx = 0;
    private float _timeSinceLastSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _pool = new GameObject[PoolSize];
        for(int idx = 0; idx < PoolSize; idx++)
        {
            _pool[idx] = Instantiate<GameObject>(Original);
            _pool[idx].SetActive(false);
        }

        _timeSinceLastSpawn = Random.value * SpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameControl.Instance.GameOver) return;

        _timeSinceLastSpawn += Time.deltaTime;
        if(_timeSinceLastSpawn >= SpawnRate && TryGetNextObject(out GameObject obj))
        {
            _timeSinceLastSpawn = 0;

            Vector2 spawnPosition = new Vector2(Random.Range(SpawnXRange.x, SpawnXRange.y), 0);
            obj.transform.position = spawnPosition;
            ColumnAdjustor ca = obj.GetComponent<ColumnAdjustor>();
            ca.TopY = Random.Range(TopYRange.x, TopYRange.y);

            float gap = Random.Range(GapRange.x, GapRange.y);
            ca.BottomY = ca.TopY - gap;

            float speed = Random.Range(SpeedRange.x, SpeedRange.y);
            ca.Speed = speed;
            ca.Range.min = TopYRange.x;
            ca.Range.max = TopYRange.y;

            ScoreIncrementor si = obj.GetComponent<ScoreIncrementor>();
            si.Reset();
            si.Value = Mathf.RoundToInt(GetWeight(speed, SpeedRange, false) * GetWeight(gap, GapRange, true) * (ScoreRange.y - ScoreRange.x)) + ScoreRange.x;
        }
    }

    private float GetWeight(float value, Vector2 range, bool invert)
    {
        float ret = (value - range.x) / (range.y - range.x);
        if(invert)
        {
            ret = 1 - ret;
        }
        return ret;
    }

    private bool TryGetNextObject(out GameObject obj)
    {
        GameObject candidate = _pool[_curIdx];
        if(!candidate.activeInHierarchy || candidate.transform.position.x < OffscreenX)
        {
            obj = _pool[_curIdx];
            obj.SetActive(true);

            _curIdx = (_curIdx + 1) % _pool.Length;

            return true;
        } else
        {
            obj = null;
            return false;
        }
    }
}
