using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_gun_proj_controller : MonoBehaviour
{
    public float bullet_velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position; 
        pos.y += bullet_velocity;
        transform.position = pos;
    }
}
