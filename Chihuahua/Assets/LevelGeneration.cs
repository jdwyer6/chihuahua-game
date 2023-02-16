using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int maxRooms = 10;
    public int roomsGenerated = 0;
    public bool bossSpawned = false;

    public GameObject[] tops;
    public GameObject[] bottoms;
    public GameObject[] lefts;
    public GameObject[] rights;

    public List<Vector2> usedSpawnLocations;
    public GameObject[] enemies;
    public GameObject[] collectibles;

    private void Awake() {
        usedSpawnLocations.Add(new Vector2(0, 0));
    }
}
