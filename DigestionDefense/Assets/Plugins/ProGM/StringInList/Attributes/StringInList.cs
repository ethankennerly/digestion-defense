using System;
using UnityEngine;

namespace ProGM
{
  public class StringInList : PropertyAttribute {
    public delegate string[] GetStringList();
  
    public StringInList(params string [] list) {
      List = list;
    }
  
    public StringInList(Type type, string methodOrFieldName) {
      var method = type.GetMethod (methodOrFieldName);
      if (method != null) {
        List = method.Invoke (null, null) as string[];
        return;
      }
      var field = type.GetField(methodOrFieldName);
      if (field != null) {
        var value = field.GetValue(field);
        if (value is string[]) {
          List = (string[])value;
        }
        return;
      }

      Debug.LogError ("NO SUCH METHOD OR STRING ARRAY " + methodOrFieldName + " FOR " + type);
    }
  
    public string[] List {
      get;
      private set;
    }
  }
}
