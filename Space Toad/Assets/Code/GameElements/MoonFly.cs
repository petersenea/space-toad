using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonFly : MonoBehaviour
{
    private float amplitude = 0.3f;
    private float frequency = 0.8f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    public float destX;
    public float destY;

    private bool reachedDest = false;
    // Start is called before the first frame update
    void Start()
    {
        destY = Random.Range(-2.7f, 1.5f);
        destX = Random.Range(-10f, 27f);
        float startX = Random.Range(-10f, 27f);
        posOffset = new Vector3(destX, destY, 0);
        transform.position = new Vector3(startX, 10f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedDest)
        {
            transform.position += (posOffset - transform.position).normalized * 4f * Time.deltaTime;
            if (Mathf.Abs(transform.position.x - posOffset.x) < 0.5f &&
                Mathf.Abs(transform.position.y - posOffset.y) < 0.5f)
            {
                // stay at this position:
                posOffset = transform.position;
                reachedDest = true;
            }
        }
        else
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }
}
