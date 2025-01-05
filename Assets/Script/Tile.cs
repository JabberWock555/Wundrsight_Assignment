using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private TilePlacement tilePlacement;
    Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        tilePlacement = this.GetComponentInParent<TilePlacement>();
        _collider.enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<SnakeMovement>() != null)
        {
            tilePlacement.RemoveTile(this);
            AudioManager.Instance.PlayTileSFX();
            Destroy(gameObject);
        }
    }
}
