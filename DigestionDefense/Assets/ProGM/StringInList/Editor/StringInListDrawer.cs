using System;
using UnityEngine;
using UnityEditor;

namespace ProGM
{
  [CustomPropertyDrawer(typeof(StringInList))]
  public class StringInListDrawer : PropertyDrawer {
    // Draw the property inside the given rect
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
      var stringInList = attribute as StringInList;
      var list = stringInList.List;
      if (property.propertyType == SerializedPropertyType.String) {
        if (list == null || list.Length == 0) {
          Debug.LogError(typeof(StringInListDrawer) + ": property=" + property.name + ": List is empty.");
          EditorGUI.Popup (position, property.displayName, -1, list);
          return;
        }
        int index = Mathf.Max (0, Array.IndexOf (list, property.stringValue));
        index = EditorGUI.Popup (position, property.displayName, index, list);
        if (index < 0 || index >= list.Length) {
          Debug.LogError(typeof(StringInListDrawer) + ": String '" + property.stringValue +
            "' not found in list. property=" + property.name + ", ");
          return;
        }
        property.stringValue = list [index];
      } else if (property.propertyType == SerializedPropertyType.Integer) {
        property.intValue = EditorGUI.Popup (position, property.displayName, property.intValue, list);
      } else {
        base.OnGUI (position, property, label);
      }
    }
  }
}
