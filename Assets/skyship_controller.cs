using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyship_controller : MonoBehaviour
{
    public float acceleration = 4f;
    public float hp = 100f;
    public Vector3 velocity = new Vector3(0f, 0f, 0f);
    public float max_velocity = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
 
     void Update () {
         Vector3 pos = transform.position;

         int count = 0;

         if(!Input.GetKey("w") || !Input.GetKey("a") || !Input.GetKey("s") || !Input.GetKey ("d")){
             velocity /= 2f;
         }

         if (Input.GetKey ("w")) {
             velocity.y += acceleration * Time.deltaTime;
             if(velocity.y > max_velocity){
                 velocity.y = max_velocity;
             }
             count++;
         }
         if (Input.GetKey ("s")) {
             velocity.y -= acceleration * Time.deltaTime;
             if(velocity.y < -max_velocity){
                 velocity.y = -max_velocity;
             }
             count++;
         }
         if (Input.GetKey ("d")) {
             velocity.x += acceleration * Time.deltaTime;
             if(velocity.x > max_velocity){
                 velocity.x = max_velocity;
             }
             count++;
         }
         if (Input.GetKey ("a")) {
             velocity.x -= acceleration * Time.deltaTime;
             if(velocity.x < -max_velocity){
                 velocity.x = -max_velocity;
             }
             count++;
         }   
 
        if(count > 1){
         transform.position += velocity / 1.4f;
        }
        else{
         transform.position += velocity;
        }
     }
}
