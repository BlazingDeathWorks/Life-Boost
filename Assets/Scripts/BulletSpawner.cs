using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right }

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Direction _direction;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _spawnRate = 0.8f;
    private float _timeSinceLastBullet = 0;
    private Vector2 dir;

    private void Awake()
    {
        switch (_direction)
        {
            case Direction.Up:
                dir = Vector2.up;
                break;
            case Direction.Down:
                dir = Vector2.down;
                break;
            case Direction.Left:
                dir = Vector2.left;
                break;
            default:
                dir = Vector2.right;
                break;
        }
    }

    private void Update()
    {
        _timeSinceLastBullet += Time.deltaTime;
        if (_timeSinceLastBullet >= _spawnRate)
        {
            _timeSinceLastBullet = 0;
            var thing = Instantiate(_bullet, transform.position, Quaternion.identity);
            thing.Direction = dir;
        }
    }
}
