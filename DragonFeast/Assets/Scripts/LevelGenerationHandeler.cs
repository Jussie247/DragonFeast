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
    [SerializeField] GameObject modules;

    [SerializeField] GameObject NavMesh;
    [SerializeField] public Difficulty difficulty;
    [SerializeField] public BossType bossType;

    public List<DragonType> dragonTypes;
    public List<GameObject> Rooms;
    public List<GameObject> BossRooms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get all rooms
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("room");
        //GameObject[] bossRooms = GameObject.FindGameObjectsWithTag("bossRoom");
        foreach (GameObject o in rooms)
        {
            Rooms.Add(o);
        }

        //get the start position
        Vector3 lastExit = start.position;
        Quaternion lastRotation = start.rotation;
        //attach each room to the last ones exit
        GameObject Instance = new GameObject();
        for (int i = 0; i < RoomsPerLevel; i++)
        {
            //get a random Module from the Level Modules
            GameObject room = Rooms[(UnityEngine.Random.Range(0, Rooms.Count))];
            //Instance the module at the last ones Exit
            Instance = Instantiate(room, lastExit, lastRotation);
            //update the Last exit Vector
            lastExit = Instance.transform.Find("Out").position;
            lastRotation = Instance.transform.Find("Out").localRotation;
            Instance.transform.parent = transform;
            //spawn the enemies
            spawnEnemis(Instance);
        }
        //instance the boss room
        switch (bossType)
        {
            case BossType.Ballista:
                //get a random Module from the Level Modules
                GameObject room = BossRooms[0];
                //Instance the module at the last ones Exit
                Instance = Instantiate(room, lastExit, lastRotation);
                Instance.transform.parent = transform;
                //spawn the enemies
                spawnEnemis(Instance);
                break;
            case BossType.Knight:
                //get a random Module from the Level Modules
                room = BossRooms[1];
                //Instance the module at the last ones Exit
                Instance = Instantiate(room, lastExit, lastRotation);
                Instance.transform.parent = transform;
                //spawn the enemies
                spawnEnemis(Instance);
                break;
            case BossType.Lancer:
                //get a random Module from the Level Modules
                room = BossRooms[2];
                //Instance the module at the last ones Exit
                Instance = Instantiate(room, lastExit, lastRotation);
                Instance.transform.parent = transform;
                //spawn the enemies
                spawnEnemis(Instance);
                break;

        }

        Invoke(nameof(bakeNavMesh), 1);

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
    }

    private void bakeNavMesh()
    {
        NavMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void spawnEnemis(GameObject instance)
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
        int randomIndex = UnityEngine.Random.Range(0, values.Length-1);
        return (T)values.GetValue(randomIndex);
    }
}
