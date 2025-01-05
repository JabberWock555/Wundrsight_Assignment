using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlacement : MonoBehaviour
{
    [SerializeField] Food foodPrefab;
    private TilePlacement tilePlacement;

    private void Start()
    {
        tilePlacement = this.GetComponent<TilePlacement>();
        StartCoroutine(SpawnFood(3f));
    }

    private IEnumerator SpawnFood(float startDelay)
    {

        yield return new WaitForSeconds(startDelay);

        Food food = Instantiate(foodPrefab, this.transform);
        food.gameObject.SetActive(false);

        while (true)
        {
            Vector3 spawnPosition = tilePlacement.GetRandomTilePosition();

            if (spawnPosition == Vector3.zero)
            {
                Debug.Log("No more tiles to spawn food on.");
                yield break;
            }

            food.transform.position = spawnPosition;
            food.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            food.gameObject.SetActive(false);
            yield return new WaitForSeconds(Random.Range(2, 5));

        }
    }
}
