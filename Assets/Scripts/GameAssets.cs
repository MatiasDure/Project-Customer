using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Class <c>GameAssets</c> contain all the game assets of the
    /// scene
    /// </summary>
    /// <remarks>
    /// Currently it only contains the sound effects of the game,
    /// but can be expanded
    /// </remarks>
    public class GameAssets : MonoBehaviour
    {
        [SerializeField] private static GameAssets _assets;
        [SerializeField] private SoundAudioClip[] soundAudioClips;

        public static GameAssets Assets { get => _assets; }
        public SoundAudioClip[] SoundAudioClips { get => soundAudioClips; }

        private void Awake()
        {
            if (_assets == null)
            {
                _assets = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}