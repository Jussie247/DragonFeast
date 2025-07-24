using UnityEngine;

public class destroyDestructionVFX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        Invoke(nameof(destroySelf), 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void destroySelf()
    {
        Destroy(gameObject);
    }
}
