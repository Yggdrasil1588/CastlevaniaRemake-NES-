using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class LaneChange
{

    public void ChangeLanes(GameObject toMove, Transform lane)
    {
        Vector3 lanetemp = new Vector3(toMove.transform.position.x, toMove.transform.position.y, lane.position.z);
        if (Input.GetAxis("Vertical")!=0)
            toMove.transform.position = lanetemp;
    }



}
