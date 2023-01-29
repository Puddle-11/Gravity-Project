using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public Vector3 Direction;
    public List<GameObject> ObjectsInFeild;
    public float Mass;
    public float GravityVolume;
    public SphereCollider Feild;
    public float SimSpeed;
    public Rigidbody RB;
    public bool equilibrium;
    public Vector3 orbitalVelocity;
    public float allMasses;
    public float Volume;
    public AnimationCurve Curve;
    public float dist;
    public float totalmass;
    public float massdifferential;
    public float deciDiff;
    public float speed;
    public float intensity;
    public float radius;
    void Start()
    {
        transform.localScale = new Vector3(Volume, Volume, Volume);
    }

    void Update()
    {
        
        RB.mass = Mass;
        if (ObjectsInFeild.Count == 0)
        {
            Direction = Vector3.zero;
        }
        else {
            CalculatePercentDirection();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ObjectsInFeild.Contains(collision.gameObject))
        {
            equilibrium = true;

        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (ObjectsInFeild.Contains(collision.gameObject))
        {
            equilibrium = false;

        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (!other.isTrigger)
        {
            if (!ObjectsInFeild.Contains(other.gameObject))
            {
                if (!other.gameObject.GetComponent<Gravity>().ObjectsInFeild.Contains(gameObject))
                {
                    other.gameObject.GetComponent<Gravity>().ObjectsInFeild.Add(gameObject);
                }
                ObjectsInFeild.Add(other.gameObject);
                    UpdateMasses();
                other.gameObject.GetComponent<Gravity>().UpdateMasses();

            }
        }
    }
    void OnTriggerExit(Collider other)
    {

        if (ObjectsInFeild.Contains(other.gameObject))
        {
            if(other.gameObject.GetComponent<Gravity>().Mass < Mass)
            {
                if (!other.isTrigger)
                {


                    if (other.gameObject.GetComponent<Gravity>().ObjectsInFeild.Contains(gameObject))
                    {

                        other.gameObject.GetComponent<Gravity>().ObjectsInFeild.Remove(gameObject);

                    }

                    ObjectsInFeild.Remove(other.gameObject);
                    UpdateMasses();
                    other.gameObject.GetComponent<Gravity>().UpdateMasses();
                }
            }
            

        }
    }
    public void UpdateMasses()
    {
        allMasses = 0;
        for (int i = 0; i < ObjectsInFeild.Count; i++)
        {

            allMasses = allMasses + ObjectsInFeild[i].GetComponent<Gravity>().Mass;
        }

    }  
  

    public void CalculatePercentDirection()
    {



        foreach (GameObject i in ObjectsInFeild)
        {
           
                float averagescale = (i.transform.localScale.x + i.transform.localScale.y + i.transform.localScale.z) / 3;
                SphereCollider trigCol = new SphereCollider();
                foreach (SphereCollider x in i.GetComponents<SphereCollider>())
                {
                    if (x.isTrigger)
                    {

                        trigCol = x;

                    }

                }

                radius = (dist / (trigCol.radius * averagescale));
                if (radius > 1)
                {
                    radius = 1;

                }
                Debug.Log(Curve.Evaluate(radius) + " " + gameObject.name);
                intensity = Curve.Evaluate(radius);

                float totalWeightPercent = i.GetComponent<Gravity>().Mass / allMasses;
                dist = Vector3.Distance(i.transform.position, transform.position);

                totalmass = Mass + i.GetComponent<Gravity>().Mass;
                massdifferential = 100 * (i.GetComponent<Gravity>().Mass / totalmass);

                deciDiff = massdifferential / 100;
                speed = (totalmass * deciDiff) * SimSpeed * (totalmass / massdifferential);
                Vector3 MoveDir = (i.transform.position - transform.position).normalized;
                Vector3 Direction = new Vector3(((MoveDir.x * totalWeightPercent) * intensity) * speed, ((MoveDir.y * totalWeightPercent) * intensity) * speed, ((MoveDir.z * totalWeightPercent) * intensity) * speed);
                RB.AddForce(Direction);

            
        }
        
    }
}
