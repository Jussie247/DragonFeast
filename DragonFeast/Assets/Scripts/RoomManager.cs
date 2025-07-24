using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    
    public bool isBoss = false;
    int clearedRoomsCount = 0;
    int spawnCount = 0;
    int clearedSpawnsCount = 0;
    GameObject[] Rooms;
    GameObject bossRoom;

    void Awake()
    {

    }

    void Start()
    {
        //get all rooms
        Rooms = GameObject.FindGameObjectsWithTag("room");
        bossRoom = GameObject.FindGameObjectWithTag("bossRoom");
    }

    void Update()
    {
        //reset so it can be counted up again
        clearedRoomsCount = 0;

        // iterate through all rooms and check if they are cleared to open their door
        for (int i = 0; i < Rooms.Length; i++)
        {
            Transform door;
            if (door = Rooms[i].transform.Find("door"))
            {
                //reset counters
                spawnCount = 0;
                clearedSpawnsCount = 0;

                // find all Spawns in the current room and check if they got cleared
                Transform[] transforms = Rooms[i].GetComponentsInChildren<Transform>();
                foreach (Transform t in transforms)
                {
                    if (t.name.Contains("Spawn"))
                        {
                        spawnCount++;
                        if (t.childCount == 0)
                        {
                            //spawn is cleared
                            clearedSpawnsCount++;
                        }
                    }
                }
                //check if all spawns have been cleared
                if(spawnCount == clearedSpawnsCount)
                {
                    //all spawns cleared, open door
                    //door.gameObject.GetComponent<doorManager>().openDoor();
                    //print("openDoor");
                    if (door.gameObject)
                    {
                        //play door sound effect
                        print("play door sound");
                        transform.GetComponent<playDoorOpenSound>().playDoorSound(door.gameObject);
                        Destroy(door.gameObject);
                    }
                    //door.GetComponent<doorManager>().openDoor();
                }
            }
            else
            {
                //room is already cleared
                clearedRoomsCount++;
                
            }
        }

        //check if all rooms are cleared
        if(clearedRoomsCount == Rooms.Length)
        {
            isBoss = true;
        }

        //check if boss room is cleared
        if(bossRoom.transform.Find("Spawn").childCount == 0)
        {

            //player won, load main menu
            
        }
    }
}
