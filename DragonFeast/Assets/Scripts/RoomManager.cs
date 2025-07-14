using System.Linq;
using UnityEditor;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // all rooms have to be children of this object
    Transform[] rooms;
    int current = 0;
    void Start()
    {
        rooms = transform.GetComponentsInChildren<Transform>()
                         .Where(t => t != transform) // drop parent
                         .ToArray();
    }

    void Update()
    {
        if (current >= rooms.Length) return; // all done

        Transform room = rooms[current];
        Transform spawns = room.Find("Spawns");

        if (spawns != null && spawns.childCount == 0)
        {
            Transform door = room.Find("door");
            if (door != null) Destroy(door.gameObject);

            current++;
        }
    }
}
