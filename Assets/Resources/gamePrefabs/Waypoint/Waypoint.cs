using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
[System.Serializable]
public class Waypoint : MonoBehaviour
{

    public List<Waypoint> Ways;
    public bool End;
    public bool EndRight;
    public Train train;//zmienna do semaforu
    private Waypoint temp;
    public string Name;
    public string ObjectName => this.name;
    public bool TrackGenerated = false;
    public bool IsPlatform = false;


    public bool IsJunction => Ways.Where(x => x != null).Count() > 2;
    public bool HasReachedRailsLimit => Ways.Where(x => x != null).Count() >= 2;

    private void Awake()
    {

    }

    public Waypoint GetWay(int index)
    {
        if (index + 1 > Ways.Count())
        {
            return null;
        }

        return Ways[index];
    }

    public void setGlobalPosition(Vector3 pozycja)
    {
        transform.position = pozycja;
    }
    void CleanUpSemaphoreTrain()
    {
        if (train != null && train.semafor != this) train = null;
    }

    public void Toggle(Waypoint junction)
    {
        if (!transform.parent.GetComponent<JunctionFail>().Damaged)
        {
            var currentWay = Ways[1];
            var newWay = Ways[2];
            Ways[1] = newWay;
            Ways[2] = currentWay;

            currentWay.Ways.Remove(junction);
            newWay.Ways.Add(junction);
        }
    }

    private void FixedUpdate()
    {
        CleanUpSemaphoreTrain();
    }


}
