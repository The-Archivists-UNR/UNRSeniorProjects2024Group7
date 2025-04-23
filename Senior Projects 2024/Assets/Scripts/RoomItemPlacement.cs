using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemPlacement : MonoBehaviour
{
    public GameObject prefab; // The prefab to place
    public int numberOfPrefabs; // Number of prefabs to place
    public Vector3 roomPosition; // Bottom-left corner of the room
    public Vector3 roomSize; // Width, height, and depth of the room
    public float minDistance; // Minimum distance between prefabs
    public Transform decorationsParent;
    public List<Vector3> placedPositions = new List<Vector3>(); // Store placed prefab positions

    public itemMgr items; //Fenn's item placement

    void Start()
    {

    }

    public void PlacePrefabs()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 newPosition;
            int attempts = 0;
            bool validPosition = false;

            do
            {
                newPosition = GetRandomPosition();
                validPosition = IsPositionValid(newPosition);
                attempts++;

                // Prevent infinite loop by limiting attempts
                if (attempts > 100)
                {
                    Debug.LogWarning("Could not find a valid position after 100 attempts.");
                    return;
                }

            } while (!validPosition);

            // Instantiate the prefab at the chosen position
            GameObject newPrefab = Instantiate(prefab, newPosition, Quaternion.identity);
            newPrefab.transform.eulerAngles = new Vector3 (newPrefab.transform.eulerAngles.x, Random.Range(0,360), newPrefab.transform.eulerAngles.z);
            newPrefab.transform.parent = decorationsParent;
            placedPositions.Add(newPosition);
        }
    }

    //Fenn's item placement code
    public GameObject PlaceItems()
    {

            Vector3 newPosition;
            int attempts = 0;
            bool validPosition = false;

            do
            {
                newPosition = GetRandomPosition();
                validPosition = IsPositionValid(newPosition);
                attempts++;

                // Prevent infinite loop by limiting attempts
                if (attempts > 100)
                {
                    Debug.LogWarning("Could not find a valid position after 100 attempts.");
                    return null;
                }

            } while (!validPosition);

            // Instantiate the prefab at the chosen position
            GameObject instItem = items.ChooseItem();
            GameObject newPrefab = Instantiate(instItem, new Vector3(newPosition.x, newPosition.y+5, newPosition.z), Quaternion.identity);
            newPrefab.tag = "item";
            newPrefab.transform.eulerAngles = new Vector3 (newPrefab.transform.eulerAngles.x, Random.Range(0,360), newPrefab.transform.eulerAngles.z);
            newPrefab.transform.parent = decorationsParent;
            placedPositions.Add(new Vector3(newPosition.x, newPosition.y+5, newPosition.z));
            items.spawnedItems.Add(newPrefab);
            newPrefab.SetActive(false);
            return newPrefab;
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(roomPosition.x+minDistance, roomPosition.x + roomSize.x - minDistance);
        float y = 0;
        float z = Random.Range(roomPosition.z+minDistance, roomPosition.z + roomSize.z - minDistance);
        return new Vector3(x, y, z);
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (Vector3 placedPosition in placedPositions)
        {
            if (Vector3.Distance(position, placedPosition) < minDistance)
            {
                return false; // Too close to another prefab
            }
        }
        return true;
    }
}
