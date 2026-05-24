using System;
using UdonSharp;
using UnityEngine;
using UnityEditor;
using VRC.SDKBase;
using VRC.Udon;

// Written by BluWizard - https://github.com/BluWizard10

namespace BluWizard.Udon.Local
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class EnableDisableGameObjectsArgument : UdonSharpBehaviour
    {
        [Header("GameObjects to Enable")]
        [Tooltip("List of GameObjects that should be Enabled when this script runs.")]
        public GameObject[] toEnable;
        
        [Header("GameObjects to Disable")]
        [Tooltip("List of GameObjects that should be Disabled when this script runs.")]
        public GameObject[] toDisable;

        public void _BluEvent()
        {
            if (toEnable != null)
            {
                foreach (GameObject enable in toEnable)
                {
                    if (enable != null) enable.SetActive(true);
                }
            }

            if (toDisable != null)
            {
                foreach (GameObject disable in toDisable)
                {
                    if (disable != null) disable.SetActive(false);
                }
            }
        }
    }  
}
