// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{

    public class LSSubscriber : UnitySubscriber<MessageTypes.Sensor.LaserScan>
    {
        public Transform SubscribedTransform;
        public GameObject prefab;
        private GameObject[] prefab_array;

        // Start is called before the first frame update
        protected override void Start()
        {
        base.Start();

        // initiate prefab_array of 180 LineRenderer prefabs
        prefab_array = new GameObject[180];
        for (int i = 0; i < 180; i++)
        {
            prefab_array[i] = Instantiate(prefab);
        }
     }

    protected override void ReceiveMessage(MessageTypes.Sensor.LaserScan message)
    {
        // linearVelocity = ToVector3(message.linear).Ros2Unity();
        // angularVelocity = -ToVector3(message.angular).Ros2Unity();
        // isMessageReceived = true;
    }

    // Update is called once per frame
    void Update()
        {

        }
    }

}