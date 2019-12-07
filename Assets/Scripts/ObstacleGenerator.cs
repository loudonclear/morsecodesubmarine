using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleGenerator : MonoBehaviour
{
    public const float innerRange = 14.0f;
    public const float outerRange = 25.0f;
    public const int minimumInRange = 7;

    public float maxXSeen;
    public float maxZSeen;
    public float minXSeen;
    public float minZSeen;

    private List<GameObject> obstacles;
    public GameObject obstaclePrefab;

    // Start is called before the first frame update
    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle").ToList<GameObject>();

        maxXSeen = this.transform.position.x + innerRange;
        maxZSeen = this.transform.position.z + innerRange;

        minXSeen = this.transform.position.x - innerRange;
        minZSeen = this.transform.position.z - innerRange;

        InvokeRepeating("GenerateObstacles", 1.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        maxXSeen = this.transform.position.x + innerRange;
        maxZSeen = this.transform.position.z + innerRange;

        minXSeen = this.transform.position.x - innerRange;
        minZSeen = this.transform.position.z - innerRange;
    }

    void GenerateObstacles() {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle").ToList<GameObject>();
        List<GameObject> withinRange = obstacles.Where<GameObject>(gO =>
        {
            return Mathf.Abs(gO.transform.position.x - this.transform.position.x) <= outerRange &&
                    Mathf.Abs(gO.transform.position.z - this.transform.position.z) <= outerRange;
        }).ToList<GameObject>();

        for(int i = withinRange.Count; i < minimumInRange; i++) {
            float randX = Random.Range(this.transform.position.x - outerRange, this.transform.position.x + outerRange);
            float randZ = Random.Range(this.transform.position.z - outerRange, this.transform.position.z + outerRange);
            //bool xChanged = false;
            //bool zChanged = false;

            int count = 0;
            while ((randX <= maxXSeen && randX >= minXSeen) && (randZ <= maxZSeen && randZ >= minZSeen) && count < 20) {
                if (randX <= maxXSeen && randX >= minXSeen)
                    randX = Random.Range(this.transform.position.x - outerRange, this.transform.position.x + outerRange);
                if (randZ <= maxZSeen && randZ >= minZSeen)
                    randZ = Random.Range(this.transform.position.z - outerRange, this.transform.position.z + outerRange);
                count++;
            }

            /*if(this.transform.position.x - outerRange < minXSeen) {
                randX = Random.Range(
                    this.transform.position.x - outerRange,
                    minXSeen);
                xChanged = true;
            }
            if(this.transform.position.x + outerRange > maxXSeen && (!xChanged || Random.Range(0, 2) == 0)) {
                randX = Random.Range(
                    this.transform.position.x + outerRange,
                    maxXSeen);
                xChanged = true;
            }

            if (this.transform.position.z - outerRange < minZSeen)
            {
                randZ = Random.Range(
                    this.transform.position.z - outerRange,
                    minZSeen);
                zChanged = true;
            }
            if (this.transform.position.z + outerRange > maxZSeen && (!zChanged || Random.Range(0, 2) == 0))
            {
                randZ = Random.Range(
                    this.transform.position.z + outerRange,
                    innerRange, maxZSeen);
                zChanged = true;
            }*/

            if (count != 20) {
                Instantiate(obstaclePrefab, new Vector3(randX, 0, randZ), Quaternion.identity);
            }
        }
    }
}
