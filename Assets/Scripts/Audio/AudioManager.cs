using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Class <c>AudioManager</c> facilitates the use of sound 
    /// in the scene
    /// </summary>
    /// <remarks>
    /// Only one is needed per scene
    /// </remarks>
    public static class AudioManager
    {

        public enum Sound
        {
            NoSound,
            DonationMoney,
            SpendMoney,
            dogDeath,
            dogBark,
            catDeath,
            catMeow,
            Soundtrack,
            AskingNpc,
            AngryNpc,
            MainMenuSoundtrack
        }


        /// <summary>
        /// This method plays the sound that is passed in
        /// </summary>
        /// <param name="sound">The sound to play.</param>
        public static void PlaySound(Sound sound)
        {
            if (sound == Sound.NoSound) return;
            int index = GetAudioIndex(sound);
            AudioSource audioSource = GameAssets.Assets.SoundAudioClips[index].source == null ?
                                        CreateAudioSource(index, sound) :
                                        GameAssets.Assets.SoundAudioClips[index].source;

            if (GameAssets.Assets.SoundAudioClips.Length > 1) SwitchAudioClips(audioSource, GameAssets.Assets.SoundAudioClips[index].clips);
            if (sound == Sound.Soundtrack)
            {
                audioSource.volume = 0.1f;
                audioSource.loop = true;
            }
            else audioSource.volume = 0.3f;

            audioSource.Play();
        }

        /// <summary>
        /// This method stops the sound that is passed in
        /// </summary>
        /// <param name="sound">The sound to stop.</param>
        public static void StopSound(Sound sound)
        {
            int index = GetAudioIndex(sound);
            if (GameAssets.Assets.SoundAudioClips[index].source == null) CreateAudioSource(index, sound);
            GameAssets.Assets.SoundAudioClips[index].source.Stop();
        }

        /// <summary>
        /// This method returns the index where the sound is located in the 
        /// GameAssets' SoundAudioClips' list
        /// </summary>
        /// <param name="sound">The sound to look for.</param>
        public static int GetAudioIndex(Sound sound)
        {
            for (int i = 0; i < GameAssets.Assets.SoundAudioClips.Length; i++)
            {
                if (GameAssets.Assets.SoundAudioClips[i].sound == sound) return i;
            }
            return -1;
        }

        /// <summary>
        /// This method creates a new object, along
        /// with a new audio source
        /// </summary>
        /// <param name="sound">The sound to create the audio source for</param>
        /// <param name="index">The index of th sound in the SoundAudioClips' list</param>
        static AudioSource CreateAudioSource(int index, Sound sound)
        {
            SoundAudioClip soundAudio = GameAssets.Assets.SoundAudioClips[index];
            soundAudio.soundObject = new GameObject("Sound" + sound);
            soundAudio.source = soundAudio.soundObject.AddComponent<AudioSource>();
            soundAudio.source.volume = 0.5f;
            if(soundAudio.clips.Length > 0)
            {
                int randomClip = Random.Range(0,soundAudio.clips.Length-1);
                soundAudio.source.clip = soundAudio.clips[randomClip];
                return soundAudio.source;
            }
            Debug.LogWarning("You added no sound clips to the game assets!");
            return null;
        }

        private static void SwitchAudioClips(AudioSource audioSrc, AudioClip[] listOfSounds)
        {
            int randomSound = Random.Range(0,listOfSounds.Length);
            audioSrc.clip = listOfSounds[randomSound];
        }


        /// <summary>
        /// This method creates checks if a sound is playing
        /// </summary>
        /// <param name="sound">The sound to check for</param>
        public static bool IsPlaying(Sound sound)
        {
            int index = GetAudioIndex(sound);
            if (GameAssets.Assets.SoundAudioClips[index].source == null) CreateAudioSource(index, sound);
            return GameAssets.Assets.SoundAudioClips[index].source.isPlaying;
        }
    }
}
