using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line;
    private Vector3 start;
    private Vector3 end;

    [SerializeField]
    private Text distanceText;
    private float distance;
    private MonsterAI monster;
    public GameObject other;

    // Start is called before the first frame update
    void Start()
    {
     
        line.positionCount = 2;
        monster = (MonsterAI)GameObject.Find("myMonster").GetComponent<MonsterAI>();

    }

    // Update is called once per frame
    void Update()
    {

        if (monster != null)
        {
            start = monster.transform.position;



            end = other.transform.position;

            line.SetPosition(0, start);
            line.SetPosition(1, end);

            if (distanceText != null)
            {
                distance = (end - start).magnitude;
                distanceText.text = distance.ToString("F2") + "meters";
            }
           
        }
        else {
            if (distanceText != null)
            {
                distanceText.text = "NULL OTHER MONSTER POINTER";
            }
        }
        
    }
}
