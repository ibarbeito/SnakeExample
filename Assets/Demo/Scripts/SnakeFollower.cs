using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFollower : MonoBehaviour {

	public SpriteContainer Tail;
	public SpriteContainer Corner;
	public SpriteContainer Body;

	[HideInInspector] public GameObject Leader;
	[HideInInspector] public GameObject Follower = null;
	[HideInInspector] public Vector3 LastLeaderPosition;

	private SpriteContainer CurrentSprite;
	private SpriteRenderer Renderer;

	/*Public*/

	public void Start() {
		CurrentSprite = Tail;
	}

	public void Follow() {
		if (Leader != null && LeaderHasMoved()) {
			FollowThe(Leader);
		}
	}

	public void ChooseSprite() {
		/*Hay 3 posibles sprites, cada uno con varias orientaciones posibles:
		- Cola: solo si es el ultimo Sprite. Puede estar en horizontal y vertical.
		- Cuerpo: cualquier parte intermedia que este alineada con el Leader y el Follower.
		Puede estar en horizontal y vertical.
		- Esquina: Si el Leader y el Follower se situan en lados adyacentes de este objeto.
		Hay cuatro orientaciones posibles, que se calculan con el metodo IdentifyCorner*/
		InitRenderer();
		if (Follower == null) {
			CurrentSprite = Tail;
			transform.eulerAngles = Utils.DirectionToEulerAngles(LastLeaderPosition-transform.position);
		}
		else {
			Vector3 Dir = LastLeaderPosition-Follower.transform.position;
			if (Dir.x!= 0 && Dir.y!=0) {
				CurrentSprite = Corner;
				transform.eulerAngles=Vector3.forward*IdentifyCorner()*Utils.QUARTER_CIRCLE_EULER;
			} else {
				CurrentSprite = Body;
				transform.eulerAngles = Utils.DirectionToEulerAngles(LastLeaderPosition-transform.position);
			}
		}
		/*Asignamos el sprite concreto, y anahdimos el desfase.
		Con el desfase permitimos definir una orientacion estandar de los sprites,
		y una forma de anhadir una correccion.

		En este ejemplo, la esquina base se considera la que apunta a la izquierda y abajo,
		pero anhadiendo un desfase de 90 se podrian usar graficos que apuntaran a la derecha en su lugar.*/
		Renderer.sprite=CurrentSprite.Sprite;
		transform.eulerAngles += CurrentSprite.GapRotation;
	}

	/*Private*/
	private void InitRenderer() {
		if (Renderer == null)
			Renderer = gameObject.GetComponent<SpriteRenderer>();
	}

	private bool LeaderHasMoved() {
		return (Leader.transform.position != LastLeaderPosition);
	}

	private void FollowThe(GameObject Leader) {
		transform.position = LastLeaderPosition;
		LastLeaderPosition = Leader.transform.position;	
	}

	public int IdentifyCorner() {
		/*La idea subyacente para este metodo es crear un convenio de nombrado para las esquinas.
		A cada esquina se le asigna un entero, de 0 a 3, de tal forma que esten ordenados en sentido
		antihorario. De este modo, al multiplicar este numero por 90f (un cuarto de circulo), obtenemos la rotacion que hay
		que aplicar para que el sprite de esquina apunte a donde queremos.

		Primeramente, se calcula un vector director unitario que representa la esquina, usando el vector
		director entre este objeto y su lider y este objeto y su seguidor. Con este calculo, se obtienen
		cuatro posibles resultados: (1,1), (1, -1), (-1, 1) y (-1, -1). */

		if (Leader == null || Follower == null)
			return -1;
		Vector3 Reference = transform.position;
		Vector3 LeaderDirection = Reference-Leader.transform.position;
		Vector3 FollowerDirection = Reference-Follower.transform.position;
		Vector3 CornerDirection = LeaderDirection+FollowerDirection;

		if (CornerDirection.magnitude == 0)
			return -1;

		float CornerX = Mathf.Sign(CornerDirection.x);
		float CornerY = Mathf.Sign(CornerDirection.y);

		if (CornerX > 0 && CornerY > 0)
			return Utils.RIGHT_UP_CORNER;
		if (CornerX > 0 && CornerY < 0)
			return Utils.RIGHT_DOWN_CORNER;
		if (CornerX < 0 && CornerY < 0)
			return Utils.LEFT_DOWN_CORNER;
		if (CornerX < 0 && CornerY > 0)
			return Utils.LEFT_UP_CORNER;
		return -1;
	}
	
}
