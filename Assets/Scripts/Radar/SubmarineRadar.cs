using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineRadar : MonoBehaviour
{
    public GameObject blip;
    public float sweepAngle = 0.0f;
    public float sweepSpeed = 30.0f;
    public float sweepRange = 90.0f;

    private HashSet<GameObject> seen = new HashSet<GameObject>();

    void Update()
    {
        sweepAngle += sweepSpeed * Time.deltaTime;
        this.sweepAngle = sweepAngle % 360;
    }

    private void createGameObjectAtPositionDelta(Vector3 delta, GameObject obj) {
        Instantiate(obj, delta + this.gameObject.transform.position, Quaternion.identity);
    }

    public void CollisionDetected(Collider collider, GameObject submarine)
    {
        Vector3 positionDelta = collider.transform.position - submarine.transform.position;
        float angle = Vector3.Angle(positionDelta, transform.forward) * (positionDelta.x < 0 ? -1 : 1);
        float sweepAngleModded = this.sweepAngle - 180;
        GameObject seenObject = collider.gameObject;

        if(angle < sweepAngleModded && angle > sweepAngleModded - this.sweepRange && !seen.Contains(seenObject)) {
            seen.Add(seenObject);

            createGameObjectAtPositionDelta(positionDelta, blip);
        }
        else if(!(angle < sweepAngleModded && angle > sweepAngleModded - this.sweepRange)) {
            seen.Remove(seenObject);
        }

    }
}
