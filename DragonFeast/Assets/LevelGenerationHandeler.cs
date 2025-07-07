using Unity.AI.Navigation;
using UnityEngine;

public class LevelGenerationHandeler : MonoBehaviour
{
    public Transform start;
    public GameObject Modules;
    public int RoomsPerLevel;
    public int RoomCount;

    public GameObject NavMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the start position
        Vector3 lastExit = start.position;
        Quaternion lastRotation = start.rotation;
        //attach each room to the last ones exit
        for(int i = 0; i < RoomsPerLevel; i++)
        {
            //get a random Module from the Level Modules
            GameObject room = Modules.transform.GetChild(Random.Range(0, RoomCount)).gameObject;
            //Instance the module at the last ones Exit
            GameObject Instance = Instantiate(room, lastExit, lastRotation);
            //update the Last exit Vector
            lastExit = Instance.transform.Find("Out").position;
            lastRotation = Instance.transform.Find("Out").localRotation;
        }

        NavMesh.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
