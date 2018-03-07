using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    Animal a;
    public float biteDamage;
    public float chewTime;
    public float turnRate;
    float time;
    
    Vector3 velocity;
    List<GameObject> foods;
    List<GameObject> dangers;
    CharacterController controller;


    // Use this for initialization
    void Awake()
    {
        a = GetComponent<Animal>();
        controller = GetComponent<CharacterController>();
        foods = new List<GameObject>();
        dangers = new List<GameObject>();
        biteDamage = 10;
        chewTime = 2;
        time = 0;
        turnRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //holds the closest things to watch out for
        float record = Mathf.Infinity;
        GameObject r = null;


        //handle foods
        foreach (GameObject g in foods)
        {
            Debug.Log("Checked " + g.name + " In Foods: " + foods.Count);
            Vector3 dir = g.transform.position - transform.position;

            //find the closest one
            if (dir.magnitude < record)
            {
                record = dir.magnitude;
                r = g;
            }
        }

        //if within range eat else seek
        if (r != null)
        {
            Vector3 dir = r.transform.position - transform.position;
            if (dir.magnitude < a.eatrange)
                Eat(r);
            else
                Seek(r.transform.position);
        }





        //for handling damgers
        foreach (GameObject d in dangers)
        {

            Debug.Log("Checked " + d.name + " In dangers: " + dangers.Count);
            //if it is the player
            if (d.name == "player")
            {
                //player code goes here
            }
            else
            {
                Flee(d.transform.position);
            }
        }


        //time from last bite
        if (time > 0)
            time -= Time.deltaTime;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "plane") //if the floor
            return;
        else if (other.tag == a.food)
            foods.Add(other.gameObject);
        else
            dangers.Add(other.gameObject);
        Debug.Log("Saw " + other.name);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name == "plane") //if the floor
            return;
        else if (other.tag == a.food)
            foods.Remove(other.gameObject);
        else
            dangers.Remove(other.gameObject);
        Debug.Log("Left " + other.name);
    }


    //seek behaviour
    private void Seek(Vector3 target)
    {
        Debug.Log("Seeking");
        Vector3 dir = target - transform.position;
        float speed;

        /*
        dir = dir.normalized * a.maxspeed;

        Vector3 steer = dir - velocity;
        steer = steer.normalized * a.maxtorque;
        controller.Move(steer);
        */

        //I want it to rotate and then move based on it's rotation
        //So I built this based on a mixture with the arive function
        if(dir.magnitude < 15)
            speed = Map(dir.magnitude, 0, 15, 0, a.maxspeed);
        else
            speed = a.maxspeed;
        Quaternion angle = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, turnRate);
        controller.Move(transform.forward * speed);
    }


    //eat behaviour
    private void Eat (GameObject g)
    {
        if (time <= 0)
        {
            //bite
            g.GetComponent<Animal>().health -= biteDamage;
            time = chewTime;
        }
    }


    private void Flee (Vector3 target)
    {
        Vector3 dir = transform.position - target;

        
        
        Quaternion angle = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, turnRate);
        controller.Move(transform.forward * a.maxspeed);
    }

    //My map function
    public float Map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) / OldRange) * NewRange) + NewMin;

        return (NewValue);
    }
}
