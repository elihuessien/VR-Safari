using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAI : MonoBehaviour
{
    public Animal a;
    float rotationSpeed;
    
    public List<GameObject> foods;
    public List<GameObject> dangers;
    public List<WeightedSteer> desiredDirections;
    CharacterController controller;
    public float awarenessRange;

    Vector3 acceleration = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        a = GetComponent<Animal>();
        controller = GetComponent<CharacterController>();
        foods = new List<GameObject>();
        dangers = new List<GameObject>();
        rotationSpeed = 3f;


        //set radius
        awarenessRange = 20;
        SphereCollider c = GetComponent<SphereCollider>();
        c.radius = awarenessRange;
    }

    // Claculating movements
    void FixedUpdate()
    {
        //clean out lists
        foreach(GameObject g in foods)
        {
            if(g == null)
            {
                foods.Remove(g);
            }
        }
        foreach (GameObject g in dangers)
        {
            if (g == null)
            {
                dangers.Remove(g);
            }
        }
        //initialize fresh desired dirrections list
        desiredDirections = new List<WeightedSteer>();

        //tell all associated scripts to do thier behaviours and populate the list
        BroadcastMessage("DoAIBehaviour", SendMessageOptions.DontRequireReceiver);


        //calculate direction based on list
        Vector3 direction = Vector3.zero;
        foreach(WeightedSteer ws in desiredDirections)
        {
            direction += ws.steer * ws.weight;
        }


        //rotate character to look in the dirrection it wants to move
        if (direction != null)
        {
            Vector3 rot = new Vector3(direction.normalized.x, 0, direction.normalized.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rot), Time.deltaTime * rotationSpeed);
        }

        //add dirrection
        acceleration += direction.normalized * a.speed;
        Vector3.ClampMagnitude(acceleration, a.speed);

        //Move in dirrection
        if (acceleration.magnitude > 1)
            controller.Move(acceleration * Time.deltaTime);

        //consume energy of force
        acceleration = Vector3.Lerp(acceleration, Vector3.zero, Time.deltaTime);

        /*original idea to move the character with normalized output
        //TODO: Lerp acceleration
        acceleration = Vector3.Lerp(acceleration, direction, Time.deltaTime);
        Vector3 velocity = (controller.velocity + acceleration * Time.deltaTime);
        velocity = Vector3.ClampMagnitude(velocity, a.maxspeed);

        controller.SimpleMove(velocity);*/
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.IsChildOf(this.transform))
            return;
        else if (other.name.Contains(this.name))
            return;
        else if (other.name.Contains("Tile_"))
            return;
        else if (other.gameObject.tag == a.food)
            foods.Add(other.gameObject);
        else
            dangers.Add(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Tile_"))
            return;
        else if (other.name == this.name)
            return;
        else if (other.gameObject.tag == a.food)
            foods.Remove(other.gameObject);
        else
            dangers.Remove(other.gameObject);
    }
}
