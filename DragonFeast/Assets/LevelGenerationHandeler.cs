using UnityEngine;

public class LevelGenerationHandeler : MonoBehaviour
{
    public Transform start;
    public GameObject Modules;
    public int RoomsPerLevel;
    public int RoomCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the start position
        Vector3 lastExit = start.position;
        //attach each room to the last ones exit
        for(int i = 0; i < RoomsPerLevel; i++)
        {
            //get a random Module from the Level Modules
            GameObject room = Modules.transform.GetChild(Random.Range(0, RoomCount)).gameObject;
            //Instance the module at the last ones Exit
            GameObject Instance = Instantiate(room, lastExit, new Quaternion());
            //update the Last exit Vector
            lastExit = Instance.transform.Find("Out").position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
