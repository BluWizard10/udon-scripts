using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon.Local
{
    [RequireComponent(typeof(Collider)), UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class PlayerColliderTrigger : UdonSharpBehaviour
    {
        [Header("Enter")]
        [Tooltip("Assign an Udon Behaviour that should run when a Player enters this Collider.")]
        public UdonBehaviour enterReceiver;

        [Tooltip("Change this if the target Script uses a different Custom Event name.")]
        public string customEnterName = "_BluEvent";
        
        [Header("Exit")]
        [Tooltip("Assign an Udon Behaviour that should run when a Player leaves this Collider's boundaries.")]
        public UdonBehaviour exitReceiver;

        [Tooltip("Change this if the target Script uses a different Custom Event name.")]
        public string customExitName = "_BluEvent";

        public override void OnPlayerTriggerEnter(VRCPlayerApi playerApi)
        {
            if (Networking.LocalPlayer != null)
            {
                if (playerApi.isLocal)
                {
                    if (enterReceiver != null) enterReceiver.SendCustomEvent(customEnterName);
                }
            }
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi playerApi)
        {
            if (Networking.LocalPlayer != null)
            {
                if (playerApi.isLocal)
                {
                    if (exitReceiver != null) exitReceiver.SendCustomEvent(customExitName);
                }
            }
        }
    }
}