using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement 
{
    float speed;
    Transform targetWp;
    Transform transform;


    public WaypointMovement(float _speed, Transform _transform, Transform target)
    {
        speed = _speed;
        transform = _transform;
        targetWp = target;
    }
    public void LookAtFirstWp()
    {
        transform.LookAt(FirstWaypointAssigner.firstWaypointAssigner.GetFirstWaypoint());
    }

    public void OnUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void NextWpOnTrigger(Collider other)
    {
        if (other.tag == "Waypoint")
        {
            int rngPoint = Random.Range(0, other.gameObject.GetComponent<NextWaypoint>().nextPoint.Length);
            targetWp = other.gameObject.GetComponent<NextWaypoint>().nextPoint[rngPoint];
            transform.LookAt(new Vector3(targetWp.position.x, transform.position.y, targetWp.position.z));
        }
    }
}
