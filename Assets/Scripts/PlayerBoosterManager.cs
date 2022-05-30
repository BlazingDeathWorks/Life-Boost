using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoosterManager : MonoBehaviour
{
    [SerializeField] private Text _boosterText;
    [SerializeField] private Transform _startPos;
    [SerializeField] private Booster _booster;
    [SerializeField] private int _maxBoosters = 1;
    private int _boosterCount = 0;
    public float TimeSinceLastDeath = 1;

    private void Awake()
    {
        _boosterText.text = _maxBoosters.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (TimeSinceLastDeath < 1) return;
            _boosterCount++;
            TimeSinceLastDeath = 0;
            if (_boosterCount > _maxBoosters)
            {
                //Reload Game
                SceneController.SameScene();
            }
            Destroy(collision.gameObject);
            var thing = Instantiate(_booster, transform.position, Quaternion.identity);
            thing.Direction = collision.GetComponent<Bullet>().Direction;
            transform.position = _startPos.position;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pit"))
        {
            TimeSinceLastDeath = 0;
            transform.position = _startPos.position;
        }
    }

    private void Update()
    {
        TimeSinceLastDeath += Time.deltaTime;
    }
}
