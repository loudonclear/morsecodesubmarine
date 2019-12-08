using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineRadar : MonoBehaviour
{
    public GameObject blip;
    public float sweepAngle = 0.0f;
    public float sweepSpeed = 30.0f;
    public float sweepRange = 90.0f;
    //private Vector3 monsterRadarSize = new Vector3.z(3.5f, 3.5f, 3.5f);
    private Vector3 monsterRadarSize = Vector3.zero;

    private HashSet<GameObject> seen = new HashSet<GameObject>();

    void Update()
    {
        sweepAngle += sweepSpeed * Time.deltaTime;
        this.sweepAngle = sweepAngle % 360;
    }

    private void createGameObjectAtPositionDelta(Vector3 delta, GameObject obj, Vector3 scale) {
        GameObject newObject = Instantiate(obj, delta + this.gameObject.transform.position, Quaternion.identity);
        newObject.transform.localScale = scale;
    }

    public void CollisionDetected(Collider collider, GameObject submarine)
    {
        Vector3 positionDelta = collider.transform.position - submarine.transform.position;
        float angle = Vector3.Angle(positionDelta, transform.forward) * (positionDelta.x < 0 ? -1 : 1);
        float sweepAngleModded = this.sweepAngle - 180;
        GameObject seenObject = collider.gameObject;

        if(angle < sweepAngleModded && angle > sweepAngleModded - this.sweepRange && !seen.Contains(seenObject)) {
            seen.Add(seenObject);
            if (seenObject.tag == "Monster")
            {
                createGameObjectAtPositionDelta(positionDelta, blip, this.monsterRadarSize);
            }
            else {
                createGameObjectAtPositionDelta(positionDelta, blip, Vector3.one);
            }
            
        }
        else if(!(angle < sweepAngleModded && angle > sweepAngleModded - this.sweepRange)) {
            seen.Remove(seenObject);
        }

    }

    public void SetMonsterRadarSize()
    {
        this.monsterRadarSize = new Vector3(3.5f, 3.5f, 3.5f);
    }

    public void UnSetMonsterRadarSize()
    {
        this.monsterRadarSize = Vector3.zero;
    }
}
