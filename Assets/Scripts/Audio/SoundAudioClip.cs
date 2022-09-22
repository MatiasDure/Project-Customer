using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Class <c>SoundAudioClip</c> is used to group sound types 
    /// and their audio clip/audio source/game object together
    /// </summary>
    /// <remarks>
    /// It is serializable to be able to modify the whole class in the 
    /// inspector
    /// </remarks>
    [System.Serializable]
    public class SoundAudioClip
    {
        public AudioManager.Sound sound;
        public AudioClip[] clips;
        public AudioSource source;
        public GameObject soundObject;
    }
}
