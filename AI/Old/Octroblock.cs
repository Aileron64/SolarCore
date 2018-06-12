using UnityEngine;
using System.Collections;

public class Octroblock : BaseEnemy
{

    SpringJoint joint;

    void SetJoint(Vector3 vec) { joint.connectedAnchor = vec; }

    override protected void Start()
    {

        joint = GetComponent<SpringJoint>();

        base.Start();
    }






}

