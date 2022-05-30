using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public Vector2 Direction;
    [SerializeField] private float _power = 20;
    PlayerMovementController thing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            thing = collision.GetComponent<PlayerMovementController>();
            thing.Speed = _power;
            collision.GetComponent<PlayerBoosterManager>().TimeSinceLastDeath = 0;
            StartCoroutine(ResetSpeed());
        }
    }

    private IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(0.8f);
        thing.Speed = thing.OriginalSpeed;
    }
}
