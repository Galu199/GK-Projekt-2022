using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Main.Assets.Scripts
{
	public class Helpers
	{
		public static GameObject BringPrefabToScene(string prefab, GameObject canvas)
		{
			return GameObject.Instantiate(RessourcesLoadPrefab(prefab), canvas.transform);
		}
		public static GameObject RessourcesLoadPrefab(string prefab)
		{
			return Resources.Load("SimpleMessageBox/Prefabs/" + prefab) as GameObject;
		}

		public static GameObject BringPrefabToScene(string prefab, float x, float y)
		{
			var obj = BringPrefabToScene(prefab, null);
			obj.transform.position = new Vector3(x, y, obj.transform.position.z);
			return obj;
		}

		public static MessageBox BringMessageBox(GameObject canvas)
		{
			return BringPrefabToScene(Consts.MessageBox.Prefabs.MessageBox, canvas).GetComponent<MessageBox>();
		}
	}
}
