using UnityEngine;

namespace ProGM
{
    public class ExampleBehaviour : MonoBehaviour {
        public static string[] s_ContextNames = new string[]{ "Game", "Input" };

        [Header("This will store the string value")]
        [StringInList("Cat", "Dog")] public string Animal;
        [Header("This will store the index of the array value")]
        [StringInList("John", "Jack", "Jim")] public int PersonID;
        
        [Header("Method returns an array of loaded scenes")]
        [StringInList(typeof(PropertyDrawersHelper), "AllSceneNames")] public string SceneName;
        [Header("Queries a public static array")]
        [StringInList(typeof(ExampleBehaviour), "s_ContextNames")] public string ContextName;
    }
}
