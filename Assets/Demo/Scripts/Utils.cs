using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

	/*Clase miscelanea de metodos utiles*/

	/*Constantes*/
	public static float QUARTER_CIRCLE_EULER = 90;
	public static float HALF_CIRCLE_EULER = 180;
	public static string HORIZONTAL = "Horizontal";
	public static string VERTICAL = "Vertical";
	public static int RIGHT_UP_CORNER = 0;
	public static int RIGHT_DOWN_CORNER = 3;
	public static int LEFT_DOWN_CORNER = 2;
	public static int LEFT_UP_CORNER = 1;
	
	/*Convierte un vector de direcciones, de los usados en SnakeHead, en angulos de Unity (sentido antihorario).*/
	public static Vector3 DirectionToEulerAngles(Vector3 Direction) {
		/* 	Ejemplos:
			(1,0): 180 (derecha)
			(-1,0): 0 (izquierda)
			(0,-1): 90 (abajo)
			(0,1): 270 (arriba)
		*/
		float horizontal = Mathf.Sign(Direction.x)*HALF_CIRCLE_EULER;
		float vertical = -Mathf.Sign(Direction.y)*QUARTER_CIRCLE_EULER; //recordemos, 0º=izquierda=eje x negativo
		Vector3 EulerAngles = Vector3.zero;
		if (Direction.x != 0) {
			EulerAngles = Vector3.forward*Mathf.Clamp(horizontal, 0, 180);
		}
		if (Direction.y !=0)
			EulerAngles = Vector3.forward*vertical;
		return EulerAngles;
	}

	public static Vector2 RandomLocation(Vector2 Min, Vector2 Max) {
		//Devuelve una posicion en el espacio 2D, situada entre los dos puntos dados
		float RandomX = Random.Range(Min.x, Max.x);
		float RandomY = Random.Range(Min.y, Max.y);
		return new Vector2(RandomX, RandomY);
	}
}
