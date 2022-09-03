using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform objectPooling;
    private readonly Stack<GameObject> _objectPool = new Stack<GameObject>();
    private readonly Queue<GameObject> _balls = new Queue<GameObject>();
    private float _minValue = -4f;
    private float _maxValue = 4f;
    private const int MaxObjectCount = 10;

    private void Start()
    {
        StartCoroutine(StartObjectPooling());
    }
    
    private IEnumerator StartObjectPooling()
    {
        while (_objectPool.Count < MaxObjectCount)
        {
            Vector3 ballPosition = new Vector3(Random.Range(_minValue, _maxValue), 7.5f, Random.Range(_minValue, _maxValue));
            GameObject newObject = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
            newObject.transform.parent = objectPooling;
            AddObjectToThePool(newObject);
            yield return new WaitForSeconds(.1f);
        }
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject objectTakenFromStack = TakeObjectFromPool();
            Vector3 ballNewPosition = new Vector3(Random.Range(_minValue + 11f, _maxValue + 11f), 7.5f, Random.Range(_minValue, _maxValue));
            objectTakenFromStack.transform.position = ballNewPosition;
            _balls.Enqueue(objectTakenFromStack);
        }
        
        else if (Input.GetMouseButtonDown(1))
        {
            if (_balls.Count != 0)
            {
                Vector3 ballPosition = new Vector3(Random.Range(_minValue, _maxValue), 7.5f, Random.Range(_minValue, _maxValue));
                GameObject firstBall = _balls.Dequeue();
                firstBall.transform.position = ballPosition;
                firstBall.transform.parent = objectPooling;
                AddObjectToThePool(firstBall);
            }
        }
    }
    
    private void AddObjectToThePool(GameObject newObject)
    {
        _objectPool.Push(newObject);
    }
    
    private GameObject TakeObjectFromPool()
    {
        if (_objectPool.Count > 0)
        {
            GameObject ball = _objectPool.Pop();
            return ball;
        }
        return Instantiate(ballPrefab, objectPooling.transform);
    }
}
