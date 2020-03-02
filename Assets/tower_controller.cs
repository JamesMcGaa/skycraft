using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_controller : MonoBehaviour
{
    private bool placed; // will become a better data structure representing what towers have been placed and where
    private GameObject placedTower; // will become a better data structure containing placed towers

    public GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
      placed = false;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyUp("space") && !placed) {
        placedTower = Instantiate(tower, transform.position, Quaternion.identity);
      }
    }
}
