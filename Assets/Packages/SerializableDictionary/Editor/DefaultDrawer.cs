using Cinemachine;
using MonoScripts;
using SOs;
using UnityEditor;

namespace Packages.SerializableDictionary.Editor
{
    public class DefaultDrawer
    {
        [CustomPropertyDrawer(typeof(DirectionCameraDictionary))]
        public class DictDrawer : SerializableDictionaryPropertyDrawer {}
    }
}