using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

	/*Esta clase define el control de la serpiente. Realmente solo movemos la cabeza, ya que el resto del cuerpo
	depende siempre del movimiento de la pieza anterior*/

	public SnakeFollower BodyPrefab;
	public List<SnakeFollower> Body;
	public float TimeBetweenMoves = 0.5f;
	public Vector3 Direction;
	public float Step = 0.48f;
	public int Score = 0;

	private float LastTime = 0f;
	private Bounds Bounds;

	// Use this for initialization
	void Start () {
		Bounds = Camera.main.OrthographicBounds();
		Direction = new Vector3(0,0,0); //Empezar quieto
		Body = new List<SnakeFollower>();
	}
	
	// Update is called once per frame
	void Update () {
		/*La "direccion deseada" se procesa todo el rato, pero solo se 
		aplica a la hora de moverse*/
		Vector3 NewDirection = CalculateDirection(Direction);

		/*Solo se aplica la logica de movimiento cuando se cumple esta condicion.
		Esto provoca la sensacion de movimiento "a golpes" de los juegos antiguos.*/
		if (Time.time - LastTime >= TimeBetweenMoves) {
			/*Para poder procesar inputs de ultimo segundo. Recordemos, el objetivo final de CalculateDirection es prohibir ciertos movimientos*/
			NewDirection = CalculateDirection(Direction);
			
			Direction = NewDirection; 
			LastTime = Time.time;

			FaceSprite();
			Move();
			KeepSnakeInBounds();
			MoveFollowers(); 
			FaceFollowersSprite(); //Esto se tiene que ejecutar DESPUES de mover el cuerpo, para que adquieran el grafico correcto

		}
	}

	void OnCollisionEnter2D(Collision2D Collision)  {
		/*Aqui se gestionan dos logicas: 
		- Comer: sumar un punto y crecer en una unidad
		- Chocar contra el cuerpo: game over*/
		if (Collision.gameObject.tag == "Food") {
			Score++;
			AddBodySection();
		} else {
			Time.timeScale=0;
			Text ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
			ScoreText.enabled=true;
			ScoreText.text+=" "+Score;
		}
	}

	/*Private*/

	private Vector3 CalculateDirection(Vector3 OldDirection) {
		float Horizontal = Input.GetAxis(Utils.HORIZONTAL);
		float Vertical = Input.GetAxis(Utils.VERTICAL);
		Vector3 NewDirection = OldDirection;

		if (Horizontal != 0) {
			NewDirection = Vector3.right*Mathf.Sign(Horizontal);
		}
		/*El último eje comprobado tiene prioridad. Si pulso "abajo" e "izquierda" a la vez,
		como "vertical" es la ultima comprobacion, me muevo hacia abajo */
		if (Vertical !=0)
			NewDirection = Vector3.up*Mathf.Sign(Vertical);
		if (OldDirection==-NewDirection)
			return OldDirection;
		return NewDirection;
	}

	private void KeepSnakeInBounds() {
		/*Garantiza que la serpiente no se salga del mundo,
		teleportando la cabeza al extremo opuesto en caso de que se
		salga de la pantalla*/
		float MinX = Bounds.min.x;
		float MinY = Bounds.min.y;
		float MaxX = Bounds.max.x;
		float MaxY = Bounds.max.y;
		Vector3 Pos = transform.position;
		if (Pos.x < MinX) {
			transform.position = new Vector3(MaxX,Pos.y,Pos.z);
		} else if(Pos.x>MaxX) {
			transform.position = new Vector3(MinX,Pos.y,Pos.z);
		} else if(Pos.y<MinY) {
			transform.position = new Vector3(Pos.x,MaxY,Pos.z);
		} else if(Pos.y>MaxY) {
			transform.position = new Vector3(Pos.x,MinY,Pos.z);
		}
		
	}
	private void FaceSprite() {
		//Orientacion del sprite de la cabeza
		transform.eulerAngles=Utils.DirectionToEulerAngles(Direction);
	}

	private void Move() {
		transform.position += Step*Direction;
	}

	private void MoveFollowers() {
		/*En lugar de usar el Update de las piezas,
		se mueven aqui de forma secuencial para garantizar
		la coherencia del conjunto. Recordemos que, para moverse,
		cada parte del cuerpo necesita que el padre este situado
		en su posicion actualizada.*/
		foreach (SnakeFollower sf in Body) {
			sf.Follow();
		}
	}

	private void FaceFollowersSprite() {
		/*Similar al metodo anterior. Separado porque, en el caso de los graficos,
		se necesita que el padre Y el hijo esten en sus posiciones adecuadas.*/
		foreach (SnakeFollower sf in Body) {
			sf.ChooseSprite();
		}
	}

    private void AddBodySection() {
		/*Se anhade siempre por el final. Esto es, cada nueva pieza es la nueva cola*/
		SnakeFollower NewTail = Instantiate(BodyPrefab);
		SnakeFollower Tail = null;
		if (Body.Count == 0) {
			/*Si es la primera pieza que anhadimos, el padre es SnakeHEad*/
			NewTail.Leader = gameObject;
		} else {
			Tail = Body[Body.Count-1];
			NewTail.Leader = Tail.gameObject;
			Tail.Follower=NewTail.gameObject;
		}
		/*La nueva cola aparece en la direccion en la que nos moviamos. Por sumar puntos de estilo, vaya*/
		NewTail.transform.position = NewTail.Leader.transform.position - Direction*Step;
		NewTail.LastLeaderPosition = NewTail.transform.position;

		Body.Add(NewTail);

		/*Estos dos updates son necesarios para conservar la coherencia del conjunto.
		El movimiento coordinado de la cabeza y las diferentes piezas se resuelve en el Update,
		pero las nuevas partes del cuerpo se anhaden via colision. 

		Con este update aseguramos que todo se coloque de forma adecuada, y con los graficos necesarios*/
		NewTail.Follow();
		NewTail.ChooseSprite();

		if (Tail != null) {
			Tail.Follow();
			Tail.ChooseSprite();
		}

	}


}
