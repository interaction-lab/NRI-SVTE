using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

namespace NRISVTE
{
    public class AnimationManager : Singleton<AnimationManager>
    {
        //private AnimationPublisher.ANIMATION_CMD currentAnim;
        //private AnimationPublisher.ANIMATION_CMD prevAnim;

        void Start()
        {
            //currentAnim = AnimationPublisher.ANIMATION_CMD.wakeup_auto;
            //prevAnim = currentAnim;
        }

        void Update()
        {
            //if(currentAnim != prevAnim)
            //{
                //animPub.PublishAnim(currentAnim);
                //prevAnim = currentAnim;
            //}
        }

        public void greet()
        {
            //currentAnim = AnimationPublisher.ANIMATION_CMD.greeting_face_sound;
        }
    }
}
