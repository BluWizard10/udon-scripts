using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon.Local
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class DisableGameObjectsArgument : UdonSharpBehaviour
    {
        [Header("GameObjects to Disable")]
        [Tooltip("List of GameObjects that should be Disabled when this script runs.")]
        public GameObject[] toDisable;

        public void _BluEvent()
        {
            if (toDisable == null) return;
            foreach (GameObject disable in toDisable)
            {
                if (disable != null) disable.SetActive(false);
            }
        }
    }
}
