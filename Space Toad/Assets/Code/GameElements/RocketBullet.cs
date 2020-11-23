using UnityEngine;
using System.Collections;

namespace Assets.Code.RocketBullet
{

    public class RocketBullet : MonoBehaviour
    {
        internal void Start()
        {

        }

        internal void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime * 2f);
        }
    }
}