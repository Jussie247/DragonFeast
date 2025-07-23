using UnityEngine;

public class CircleBehindplayer : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_PlayerPos");
    public static int SizeID = Shader.PropertyToID("_Size");

    //public Material wallshader;
    //public Material Mortar;
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
            //wallshader.SetFloat(SizeID, 1);
            //Mortar.SetFloat(SizeID, 1);
        }
        else
        {
            Wallmaterial.SetFloat(SizeID, 0);
            //wallshader.SetFloat(SizeID, 0);
            //Mortar.SetFloat(SizeID, 0);
        }
        var view = Camera.WorldToViewportPoint(transform.position);
        //wallshader.SetVector(PosID, view);
        //Mortar.SetVector(PosID, view);
        Wallmaterial.SetVector(PosID, view);
    }
}
