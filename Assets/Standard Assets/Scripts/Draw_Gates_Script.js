    function OnDrawGizmos () {

    var waypoints = gameObject.GetComponentsInChildren( Transform );
    for ( var waypoint : Transform in waypoints ) {
    Gizmos.color = Color (1,0,0,.45);
    Gizmos.DrawCube( waypoint.position, Vector3 (8,8,8));
    }

    }
