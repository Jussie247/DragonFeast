using UnityEngine;

public class CircleBehindplayer : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_PlayerPos");
    public static int SizeID = Shader.PropertyToID("_Size");

    //public Material wallshader;
    //public Material MortarMaterial;
    public Material Wallmaterial;
    public Camera Camera;
    public LayerMask Mask;

    // Update is called once per frame
    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);

        if (Physics.Raycast(ray, 3000, Mask))
        {
            Wallmaterial.SetFloat(SizeID, 1);
        }
        else
        {
            Wallmaterial.SetFloat(SizeID, 0);
        }
        var view = Camera.WorldToViewportPoint(transform.position);
        //wallshader.SetVector(PosID, view);
        //MortarMaterial.SetVector(PosID, view);
        Wallmaterial.SetVector(PosID, view);
    }
}
