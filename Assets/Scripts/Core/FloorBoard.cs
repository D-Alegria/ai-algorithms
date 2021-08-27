using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBoard : MonoBehaviour
{
    public int value;
    public Color neon;
    public Color purp;
    private Renderer _boardRenderer;
    
    void Awake()
    {
        _boardRenderer = gameObject.GetComponent<Renderer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        switch (value)
        {
            case 0:
                _boardRenderer.material.color = purp;
                _boardRenderer.material.DisableKeyword("_EMMISION");
                break;
            case 9:
                _boardRenderer.material.color = neon;
                _boardRenderer.material.EnableKeyword("_EMMISION");
                break;
            default:
                _boardRenderer.material.color = Color.white;
                _boardRenderer.material.DisableKeyword("_EMMISION");
                break;
        }
    }
}
