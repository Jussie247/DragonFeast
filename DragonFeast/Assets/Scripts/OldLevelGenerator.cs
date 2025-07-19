using System.Collections.Generic;
using System;
using Unity.AI.Navigation;
using UnityEngine;

public class OldLevelGenerator : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] GameObject Modules;
    [SerializeField] int RoomsPerLevel;
    [SerializeField] int RoomCount;

    [SerializeField] GameObject NavMesh;
    [SerializeField] public Difficulty difficulty;
    [SerializeField] public BossType bossType;

    public List<DragonType> dragonTypes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the start position
        Vector3 lastExit = start.position;
        Quaternion lastRotation = start.rotation;
        //attach each room to the last ones exit
        for (int i = 0; i < RoomsPerLevel; i++)
        {
            //get a random Module from the Level Modules
            GameObject room = Modules.transform.GetChild(UnityEngine.Random.Range(0, RoomCount)).gameObject;
            //Instance the module at the last ones Exit
            GameObject Instance = Instantiate(room, lastExit, lastRotation);
            //update the Last exit Vector
            lastExit = Instance.transform.Find("Out").position;
            lastRotation = Instance.transform.Find("Out").localRotation;
            Instance.transform.parent = transform;
        }

        NavMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
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

    // Update is called once per frame
    void Update()
    {

    }

    public static T GetRandomEnumValue<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        int randomIndex = UnityEngine.Random.Range(0, values.Length - 1);
        return (T)values.GetValue(randomIndex);
    }
}
