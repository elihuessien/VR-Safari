using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
    public float health;
    public string food;
    public float maxspeed;
    public float maxtorque;
    public float awarenessRange;
    public float eatrange;

    // Use this for initialization
    void Start () {
        food = "veg";
        maxspeed = 10;
        maxtorque = 0.1f;
        awarenessRange = 10;
        eatrange = 1;

        //set radius
        SphereCollider c = GetComponent<SphereCollider>();
        c.radius = awarenessRange;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        //ANIMATION maybe
        GameObject.Destroy(this);
    }
}
