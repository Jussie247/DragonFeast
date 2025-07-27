using UnityEngine;
using UnityEngine.UI;

public class cutoutcircle : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        var dir = transform.position - targetObject.position;
        var ray = new Ray(targetObject.position, dir.normalized);
        
        if (Physics.Raycast(ray, dir.magnitude, wallMask))
        {
            for(int i = 0; i < hitObjects.Length; i++)
            {
                Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

                for (int j = 0; j < materials.Length; j++)
                {
                    materials[j].SetVector("_Cutout_Position", cutoutPos);
                    materials[j].SetFloat("_CutoutSize", 0.2f);
                    materials[j].SetFloat("_FalloffSize", 0.1f);
                }
            }
        }
        else
        {
            for (int i = 0; i < hitObjects.Length; i++)
            {
                Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

                for (int j = 0; j < materials.Length; j++)
                {
                    materials[j].SetVector("_Cutout_Position", cutoutPos);
                    materials[j].SetFloat("_CutoutSize", 0.0f);
                    materials[j].SetFloat("_FalloffSize", 0.0f);
                }
            }
        }   
    }
}
