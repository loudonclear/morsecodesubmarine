using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOrientation : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line;
    private Vector3 start;
    private Vector3 end;

    private MonsterAI monster;

    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 2;
        monster = (MonsterAI)GameObject.FindGameObjectWithTag("monster").GetComponent<MonsterAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (monster != null)
        {
            start = monster.transform.position;
        }
    }
}
