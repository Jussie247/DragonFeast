using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // all rooms have to be children of this object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform[] childs = transform.GetComponentsInChildren<Transform>();

        //iterate through all rooms
        for (int i = 0; i < childs.Length;)
        {
            //check if all enemies got defeated
            if (childs[i].transform.Find("Spawns").childCount == 0)
            {
                //destroy the door
                Destroy(childs[i].transform.Find("door"));
                i++;
            }
        }
    }
}
