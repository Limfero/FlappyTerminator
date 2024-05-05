using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float _amountOfParallax;
    [SerializeField] private Camera _mainCamera;

    private float _startingPos;
    private float _lengthOfSprite;

    private void Start()
    {
        _startingPos = transform.position.x;
        _lengthOfSprite = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position.x = _startingPos + _mainCamera.transform.position.x * _amountOfParallax;
        transform.position = position;

        float temp = _mainCamera.transform.position.x * (1 - _amountOfParallax);

        if (temp > _startingPos + (_lengthOfSprite / 2))
            _startingPos += _lengthOfSprite;
        else if (temp < _startingPos - (_lengthOfSprite / 2))
            _startingPos -= _lengthOfSprite;
    }
}
