using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyship_controller : MonoBehaviour
{
    public float speed;
    public float hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
 
     void Update () {
         Vector3 pos = transform.position;
 
         if (Input.GetKey ("w")) {
             pos.y += speed * Time.deltaTime;
         }
         if (Input.GetKey ("s")) {
             pos.y -= speed * Time.deltaTime;
         }
         if (Input.GetKey ("d")) {
             pos.x += speed * Time.deltaTime;
         }
         if (Input.GetKey ("a")) {
             pos.x -= speed * Time.deltaTime;
         }
             
 
         transform.position = pos;
     }
}
