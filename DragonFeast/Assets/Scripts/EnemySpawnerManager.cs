using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    Difficulty difficulty;
    int spawnCount;

    [SerializeField] EnemyType enemyType;

    [SerializeField] GameObject knightPrefab, archerPrefab, lancerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        difficulty = GameObject.Find("LevelGenerator").GetComponent<LevelGenerationHandeler>().difficulty;
        //set the spawn count based of the level difficulty
        if (difficulty == Difficulty.Easy)
        {
            spawnCount = 1;
        }
        else if (difficulty == Difficulty.Normal)
        {
            spawnCount = 2;
        }
        else if (difficulty == Difficulty.Hard)
        {
            spawnCount = 3;
        }

        //instance the correct amount of the desired enemy
        if (enemyType == EnemyType.Knight)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(knightPrefab, transform);
            }
        }
        if (enemyType == EnemyType.Archer)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(archerPrefab, transform);
            }
        }
        if (enemyType == EnemyType.Lancer)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Instantiate(lancerPrefab, transform);
            }
        }
    }
}
