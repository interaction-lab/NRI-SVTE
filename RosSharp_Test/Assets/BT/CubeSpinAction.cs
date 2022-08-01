using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;


public class CubeSpinAction : ActionNode {
    public float degPerSec = 360;
    public bool isClockwise = true;

    private float startRotation, accumulatedRotation;
    protected override void OnStart() {
        // get the start and end rotation
        startRotation = context.transform.rotation.eulerAngles.y;
        accumulatedRotation = 0;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        // spin the cube in the direction of the isClockwise bool
        if (isClockwise) {
            context.transform.Rotate(Vector3.up, degPerSec * Time.deltaTime);
        }
        else {
            context.transform.Rotate(Vector3.up, -degPerSec * Time.deltaTime);
        }
        accumulatedRotation += degPerSec * Time.deltaTime;
        // // check if the cube has reached the end rotation
        if (accumulatedRotation >= 360) {
            // rotate to exactly 360 degrees from start rotation
            context.transform.rotation = Quaternion.Euler(
                context.transform.eulerAngles.x,
                startRotation + 360,
                context.transform.eulerAngles.z);
            return State.Success;
        }

        return State.Running;
    }
}


