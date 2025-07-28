using UnityEngine;
using System.Collections.Generic;

public class CutoutCircle : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;

    [SerializeField]
    private GameObject LevelGenerator;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Vector3 direction = targetObject.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, wallMask);

        // Reset all materials on LevelGenerator
        Renderer[] allRenderers = LevelGenerator.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in allRenderers)
        {
            foreach (Material mat in r.materials)
            {
                if (mat.HasProperty("_Cutout_Position") &&
                    mat.HasProperty("_CutoutSize") &&
                    mat.HasProperty("_FalloffSize"))
                {
                    mat.SetFloat("_CutoutSize", 0f);
                    mat.SetFloat("_FalloffSize", 0f);
                }
            }
        }

        // Apply cutout to hit walls
        foreach (RaycastHit hit in hits)
        {
            Renderer r = hit.collider.GetComponent<Renderer>();
            if (r != null)
            {
                foreach (Material mat in r.materials)
                {
                    if (mat.HasProperty("_Cutout_Position") &&
                        mat.HasProperty("_CutoutSize") &&
                        mat.HasProperty("_FalloffSize"))
                    {
                        mat.SetVector("_Cutout_Position", cutoutPos);
                        mat.SetFloat("_CutoutSize", 0.2f);
                        mat.SetFloat("_FalloffSize", 0.1f);
                    }
                }
            }
        }
    }
}