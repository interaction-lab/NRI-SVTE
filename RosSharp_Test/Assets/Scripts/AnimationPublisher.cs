using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class AnimationPublisher : UnityPublisher<MessageTypes.Std.String>
    {
        MessageTypes.Std.String msg;
        bool is_animating = false;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            msg = new MessageTypes.Std.String("");
        }

        IEnumerator PublishIfNothingAnimating()
        {
            if (is_animating)
            {
                yield break;
            }
            is_animating = true;
            Publish(msg);
            yield return new WaitForSeconds(0.75f);
            is_animating = false;
        }

        public void PublishAnim(ANIMATION_CMD anim)
        {
            msg.data = anim.ToString();
            StartCoroutine(PublishIfNothingAnimating());
        }

        public void PublishStringAnim(string command)
        {
            msg.data = command;
            StartCoroutine(PublishIfNothingAnimating());
        }

        public enum ANIMATION_CMD
        {
            // DockingSupportAnimations,
            docking_complete_asleep,
            docking_complete_awake,
            docking_complete_happy,
            docking_waypoint_reached,
            live_undock,
            redock,
            undock,
            undock_from_old_home,
            // CommonAnimations,
            attention_look_around_3,
            capture_start,
            eyes,
            greeting_face_no_sound,
            greeting_face_sound,
            inch,
            no,
            pan,
            pantilt,
            reset_head,
            sad,
            search_user_capture,
            smile,
            stop,
            tap_head_find_face,
            tilt,
            turn_to,
            yes,
            // RomojiAnimations,
            happy_birthday,
            i_love_you,
            lullaby_song,
            night_light,
            // DockingLEDAnimations,
            docking_back_up,
            docking_looking_for_dock,
            // LEDEmotionAnimations,
            breath,
            // ObserverModeAnimations,
            observer_end,
            observer_indicator,
            observer_start,
            // IdleAnimations,
            look_around_adults,
            look_around_common,
            look_around_kids_pets,
            scripted_idle,
            // OnboardingAnimations,
            greeting_first_capture,
            search_first_capture,
            undock_and_scan,
            // BlinkAnimations,
            blink,
            double_blink,
            triple_blink,
            // ReactionAnimations,
            at_attention_look_around,
            at_attention_reset,
            battery_critical,
            battery_low,
            bump,
            dance_done,
            dance_music_detected,
            fart,
            gotit,
            gotit_docked,
            head_touch,
            head_touch_end,
            huh1,
            huh1_docked,
            huh1_offline,
            huh1_offline_docked,
            huh2,
            listening,
            listening_pose,
            lost,
            old_giggle,
            pickup,
            putdown,
            reset_sad,
            sheep,
            tickle,
            tickle_end,
            waypoint_reached,
            // DockingAnimations,
            docking_approaching,
            docking_back_in,
            docking_preparing_to_approach,
            docking_preparing_to_back_in,
            docking_preparing_to_rotate_180,
            docking_reset,
            docking_rotate_180,
            docking_turn_ccw,
            docking_turn_cw,
            // SleepAndWakeAnimations,
            twitch_1,
            twitch_2,
            twitch_3,
            twitch_4,
            wakeup_auto,
            wakeup_fast,
            // SocialAnimations,
            face_detected,
            face_lost,
            reposition,
            // SystemAnimations,
            boot_up,
            critical_battery,
            start_sound,
            // RelocalizeAnimations,
            relocalize_part0,
            relocalize_part1,
            relocalize_part2,
            relocalize_part3,
            relocalize_part4,
            relocalize_part5,
            relocalize_part6,
            relocalize_part7,
            relocalize_part8,
            // TestAnimations,
            seizure,
            test_eyes,
            test_pan,
            test_tilt
        }
    }
}
