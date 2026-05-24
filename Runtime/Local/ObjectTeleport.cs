using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon.Local
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class ObjectTeleport : UdonSharpBehaviour
    {
        [Tooltip("Which GameObject should be Teleported when this script runs?")]
        public GameObject targetGameObject;

        [Tooltip("Where do you want the Target Game Object teleported to?")]
        public Transform teleportTarget;

        public void _BluEvent()
        {
            if (targetGameObject != null && teleportTarget != null) targetGameObject.transform.SetPositionAndRotation(teleportTarget.position, teleportTarget.rotation);
        }
    }
}