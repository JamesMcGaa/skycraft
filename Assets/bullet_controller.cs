using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{

    public float bullet_velocity;
    public const float BULLET_SURVIVAL_TIME = 2f;

    // Start is called before the first frame update
    void Start()
    {
      Destroy(gameObject, BULLET_SURVIVAL_TIME);
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 pos = transform.position;
      pos.y += bullet_velocity;
      transform.position = pos;
    }
}
