using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    
    public bool isBoss = false;
    int clearCount = 0;
    GameObject[] Rooms;
    GameObject bossRoom;

    void Start()
    {
        //get all rooms
        Rooms = GameObject.FindGameObjectsWithTag("room");
        bossRoom = GameObject.FindGameObjectWithTag("bossRoom");
    }

    void Update()
    {
        for(int i = 0; i < Rooms.Length; i++)
        {
            Transform door;
            if (door = Rooms[i].transform.Find("door"))
            {
                if (Rooms[i].transform.Find("Spawns").childCount == 0)
                {
                    //room is cleared, open door
                    Destroy(door.gameObject);
                }
            }
            else
            {
                //room is already cleared
                clearCount++;
            }
        }
        //check if all rooms are cleared
        if(clearCount == Rooms.Length)
        {
            isBoss = true;
        }

        //check if boss room is cleared
        if(bossRoom.transform.Find("Spawns").childCount < 0)
        {
            //player won, load main menu
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        }
    }
}
