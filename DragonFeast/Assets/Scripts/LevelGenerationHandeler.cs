using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}

public enum BossType
{
    Ballista,
    Knight,
    Lancer,
    Random
}

public class LevelGenerationHandeler : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] int RoomsPerLevel;

    [SerializeField] GameObject NavMesh;
    [SerializeField] public Difficulty difficulty;
    [SerializeField] public BossType bossType;

    public List<DragonType> dragonTypes;
    public List<GameObject> Rooms;
    public List<GameObject> BossRooms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if boss type ist set to random it will pick a random one
        if (bossType == BossType.Random)
        {
            bossType = GetRandomEnumValue<BossType>();
        }

        // make a List with the possible dragon types for this level
        if (bossType == BossType.Ballista)
        {
            dragonTypes = new List<DragonType> { DragonType.Kamikaze };
        }
        else if (bossType == BossType.Knight)
        {
            dragonTypes = new List<DragonType> { DragonType.Heal, DragonType.Attack };
        }
        else if (bossType == BossType.Lancer)
        {
            dragonTypes = new List<DragonType> { DragonType.Heal, DragonType.Attack };
        }

        //--------------------------------------------------------------------------------World Generation
        float RoomCount = Rooms.Count;

        //get the start position
        Vector3 lastExit = start.position;
        Quaternion lastRotation = start.rotation;
        GameObject Instance = new GameObject();
        //attach each room to the last ones exit
        for (int i = 0; i < RoomsPerLevel; i++)
        {
            //get a random Module from the Level Modules
            GameObject room = Rooms[(int)UnityEngine.Random.Range(0, RoomCount)];
            //Instance the module at the last ones Exit
            Instance = Instantiate(room, lastExit, lastRotation);
            //update the Last exit Vector
            lastExit = Instance.transform.Find("Out").position;
            lastRotation = Instance.transform.Find("Out").localRotation;
            Instance.transform.parent = transform;
            //spawn the enemies
            spawnEnemies(Instance);
        }

        //Placeholder because we only have one Boss Room for nowy
        GameObject bossRoom = BossRooms[0];

        switch (bossType)
        {
            case BossType.Ballista:
                //get a random Module from the Level Modules
                GameObject room = bossRoom;//BossRooms[0];
                //Instance the module at the last ones Exit
                Instance = Instantiate(room, lastExit, lastRotation);
                Instance.transform.parent = transform;
                //spawn the enemies
                spawnEnemies(Instance);
                break;
            case BossType.Knight:
                //get a random Module from the Level Modules
                room = bossRoom; //BossRooms[1];
                //Instance the module at the last ones Exit
                Instance = Instantiate(room, lastExit, lastRotation);
                Instance.transform.parent = transform;
                //spawn the enemies
                spawnEnemies(Instance);
                break;
            case BossType.Lancer:
                //get a random Module from the Level Modules
                room = bossRoom; //BossRooms[2];
                //Instance the module at the last ones Exit
                Instance = Instantiate(room, lastExit, lastRotation);
                Instance.transform.parent = transform;
                //spawn the enemies
                spawnEnemies(Instance);
                break;

        }

        //bake NavMesh for enemy AI
        bakeNavMesh();

        transform.AddComponent<RoomManager>();
    }

    private void bakeNavMesh()
    {
        NavMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void spawnEnemies(GameObject instance)
    {
        EnemySpawnerManager[] spawners = instance.transform.GetComponentsInChildren<EnemySpawnerManager>();
        foreach (EnemySpawnerManager spawner in spawners)
        {
            spawner.spawnEnemy();
        }
    }
    public static T GetRandomEnumValue<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        int randomIndex = UnityEngine.Random.Range(0, values.Length - 1);
        return (T)values.GetValue(randomIndex);
    }
}
