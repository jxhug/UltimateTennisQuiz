using UnityEngine;
using UnityEditor;
using System.Collections;

public static class CustomDuplicate
{
	[MenuItem("GameObject/Custom Duplicate", false, 0)]
	static public void DuplicateObject (MenuCommand menuCommand)
	{
		var obj = menuCommand.context as GameObject;
		
		if(obj == null)
			return;
		
		Debug.Log (string.Format("Duplicating object: '{0}'. Hello copy/paste world! Duplicating without matrix mess issue! Yuhu!", obj.name));
		
		GameObject newObject = Object.Instantiate<GameObject> (obj);

		newObject.transform.localRotation = obj.transform.localRotation;
		newObject.transform.SetParent (obj.transform.parent, false);
		
		//apply exact values from original
		newObject.transform.localPosition.FromVector(obj.transform.localPosition);
		newObject.transform.localEulerAngles.FromVector(obj.transform.localEulerAngles);
		newObject.transform.localScale.FromVector(obj.transform.localScale);
		
		Undo.RegisterCreatedObjectUndo(newObject, "Custom Duplicate");
		
		Selection.activeGameObject = newObject;
		
		//--- just keeping the duplicate name policy, use your own at will

		newObject.name = newObject.name.Replace ("(Clone)", "");
		
		int firtsIndex = newObject.name.LastIndexOf ('(');
		int lastIndex = newObject.name.LastIndexOf (')');
		
		if (firtsIndex > 0 && lastIndex > 0)
		{
			//try get number
			string possibleNumber = newObject.name.Substring(firtsIndex + 1, lastIndex - firtsIndex - 1);
			
			if(string.IsNullOrEmpty(possibleNumber))
			{
				newObject.name = newObject.name + " (1)";
				return;
			}
			
			int number = 0;
			int.TryParse(possibleNumber, out number);
			
			if(number <= 0)
				newObject.name = newObject.name + " (1)";
			else
			{
				string newName = newObject.name.Remove (firtsIndex - 1, lastIndex - firtsIndex + 2);
				string baseName = newName;
				newName = baseName + " (" + (++number) + ")";
				
				Transform objParent = newObject.transform.parent;
				
				//search in parent
				if(objParent != null)
				{
					while(objParent.Find(newName) != null)
						newName = baseName + " (" + (++number) + ")";
				}
				else //search name in hierarchy
				{
					while(GameObject.Find("/" + newName) != null)
						newName = baseName + " (" + (++number) + ")";
				}
				
				newObject.name = newName;
			}
		}
		else
		{
			string newName =  newObject.name + " (1)";
			string baseName = newObject.name;
			
			int number = 1;
			
			Transform objParent = newObject.transform.parent;
			
			//search in parent
			if(objParent != null)
			{
				while(objParent.Find(newName) != null)
					newName = baseName + " (" + (++number) + ")";
			}
			else //search name in hierarchy
			{
				while(GameObject.Find("/" + newName) != null)
                    newName = baseName + " (" + (++number) + ")";
            }
            
            newObject.name = newName;
        }        
    }
}
