using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelectTrain : MonoBehaviour {

    const float highVelocity = 150;//prędkość normalna pociągu
    const float lowVelocity = 80;// prędkość do jakiej pociąg zwalnia jeśli ma jakiś przed sobą
    const float trainDistance = 0.7f;//minimalny dystans między pociągami

    public float velocity = highVelocity;

    public bool move = true;
    public stickPath path;
    Vector3 nextPosition;
    int iterator = 0;

    public Collider[] hitColliders;//tablica elementów kolizyjnych pociągu
    // Use this for initialization
    void Start () {
        transform.localPosition = path.waypoints[iterator++].waypoint;
        nextPosition = path.waypoints[iterator].waypoint;

    }
	
	void Update () {
        //jeśli pociąg może jechać, to go przemieszczamy
        if(move) transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, nextPosition, Time.deltaTime * velocity );

        //jeśli pociąg dojechał do ostatniego waypointa
        Vector2 position = transform.localPosition;
        if (position == path.waypoints[path.waypoints.Count - 1].waypoint)
        {
            GameObject.Find("Canvas").GetComponent<levelSelectTrainGenerator>().licznik--;
            Destroy(gameObject);
        }

        //jeśli pociąg dojechał do obecnego next waypointa, to dajemy mu nowy next waypoint
        if(position != path.waypoints[path.waypoints.Count - 1].waypoint && transform.localPosition == nextPosition) nextPosition = path.waypoints[++iterator].waypoint;


        //wykrywanie pociągów obok
        hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach(Collider obiekt in hitColliders)
        {
            float odlegloscX = obiekt.transform.position.x - transform.position.x;
            float odlegloscY = obiekt.transform.position.y - transform.position.y;
            if (obiekt.GetComponent<levelSelectTrain>() != null && Mathf.Abs(transform.position.y - obiekt.transform.position.y) < 0.2 && odlegloscX > 0  && odlegloscX < trainDistance) velocity = lowVelocity;
            else velocity = highVelocity;
        }
    }
}
