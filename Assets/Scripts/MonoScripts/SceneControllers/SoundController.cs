using System;
using Packages.SerializableDictionary;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;

namespace MonoScripts.SceneControllers
{
    [CreateAssetMenu(fileName = "SoundController", menuName = "SOs/SoundController", order = 0)]
    public class SoundController : ScriptableObject
    {
        [SerializeField] private AudioSource audioSource;
        
        public void PlaySound(AudioClip clip)
        {
            AudioSource source = Instantiate(audioSource);
            DontDestroyOnLoad(source.gameObject);
            
            source.clip = clip;
            source.Play();
            Destroy(source.gameObject, clip.length);
        }
    }
}