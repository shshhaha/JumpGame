using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerChangeRotation : MonoBehaviour
{
    public FloatingJoystick Rjoy;
    private float BoundaryValue=0;
    private bool attackAllow = false;
    // Start is called before the first frame update
    void Update()
    {
        if(Rjoy.Vertical != 0 || Rjoy.Horizontal !=0)
        {
            this.transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(Rjoy.Horizontal, Rjoy.Vertical)*Mathf.Rad2Deg, 0f);
            BoundaryValue = Mathf.Abs(Rjoy.Horizontal) + Mathf.Abs(Rjoy.Vertical);

            if(BoundaryValue>1)
            {
                attackAllow=true;
                Debug.Log("공격true");
            }
            else
            {
                attackAllow=false;
            }
        }
    }
}
