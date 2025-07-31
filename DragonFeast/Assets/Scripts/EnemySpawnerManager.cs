using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    Difficulty difficulty;
    int spawnCount;

    [SerializeField] EnemyType enemyType;

    [SerializeField] GameObject knightPrefab, archerPrefab, lancerPrefab, ballistaBoss;

    [SerializeField] bool isBoss = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemy()
    {
        if (isBoss)
        {
            GameObject boss = Instantiate(ballistaBoss, transform.position, transform.rotation);
        }
        else
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

            GameObject enemy = new GameObject();
            //instance the correct amount of the desired enemy
            if (enemyType == EnemyType.Knight)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    enemy = Instantiate(knightPrefab, transform);
                    enemy.transform.position = transform.position + new Vector3(0, i * 2, 0);
                }
            }
            if (enemyType == EnemyType.Archer)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    enemy = Instantiate(archerPrefab, transform);
                    enemy.transform.position = transform.position + new Vector3(0, i * 2, 0);
                }
            }
            if (enemyType == EnemyType.Lancer)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    enemy = Instantiate(lancerPrefab, transform);
                    enemy.transform.position = transform.position + new Vector3(0, i * 2, 0);
                }
            }

            //check if the Spawns are in the first Room -> keep enemies active

            if (transform.parent == GameObject.Find("LevelGenerator").transform.GetChild(0))
            {

            }
            else
            {
                //deactivate Enemy
                enemy.SetActive(false);
            }

            
        }
    }
}
