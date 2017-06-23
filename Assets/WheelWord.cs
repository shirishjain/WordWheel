using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;
public class WheelWord : MonoBehaviour 
{
	public VectorObject2D vectorObject;
	public GameObject textParent;
	public Text centerText;
    public static WheelWord instance;
	public void Awake()
	{
        instance = this;
	}

	public void DrawWheelWord(List<string> letters)
	{
		float radius = 140f;
		transform.GetComponent<RectTransform> ().sizeDelta = Vector2.one * radius * 2;
		List<Vector2> points = new List<Vector2> ();
		centerText.text = letters [0].ToUpper();	
		for (int i = 0; i < 360f; i += 45) {
			points.Add (Vector2.zero);
			points.Add (PointAtDirection(Vector2.zero, i, radius ));
			int index = i / 45;
			textParent.transform.GetChild (index).GetComponent<RectTransform> ().anchoredPosition = PointAtDirection (Vector2.zero, i +22.5f , radius * 0.7f) ;
			textParent.transform.GetChild (index).GetComponent<Text> ().text = letters [index + 1].ToUpper();
		}
		vectorObject.vectorLine.points2 = points;
		vectorObject.vectorLine.Draw ();


	}

	public Vector2 PointAtDirection(Vector2 origin,float angle,float radius)
	{
		return new Vector2(origin.x+radius * Mathf.Cos(Mathf.Deg2Rad * angle), origin.y+radius*Mathf.Sin(Mathf.Deg2Rad*angle));
	}



}
