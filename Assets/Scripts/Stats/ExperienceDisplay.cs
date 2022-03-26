using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Expierence expierence;
        private void Awake()
        {
             expierence = GameObject.FindWithTag("Player").GetComponent<Expierence>();
        }

        void Update()
        {
            GetComponent<Text>().text = String.Format("{0}", expierence.GetExperiencePoints());
            
        }
    }
}
