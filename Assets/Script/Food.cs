using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<SnakeMovement>() != null)
        {
            gameObject.SetActive(false);
            AudioManager.Instance.PlayPizzaSFX();
            UIManager.AddScore?.Invoke();
        }
    }
}
