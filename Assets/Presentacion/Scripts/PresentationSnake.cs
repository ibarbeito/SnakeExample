using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationSnake : MonoBehaviour {

	/*Esta clase define el control de la serpiente. Realmente solo movemos la cabeza, ya que el resto del cuerpo
	depende siempre del movimiento de la pieza anterior*/

	public SnakeFollower BodyPrefab;
	public List<SnakeFollower> Body;
	public Vector3 Direction;
	public float Step = 0.48f;
	private Bounds Bounds;

	// Use this for initialization
	void Start () {
		Bounds = Camera.main.OrthographicBounds();
		Direction = new Vector3(0,0,0); //Empezar quieto
		Body = new List<SnakeFollower>();
	}
	
	// Update is called once per frame
	void Update () {
		Direction = CalculateDirection();
		if (Direction!=Vector3.zero) {
			FaceSprite();
			Move();
			KeepSnakeInBounds();
			MoveFollowers(); 
			FaceFollowersSprite();
		}
	
		if (Input.GetKeyDown(KeyCode.Q)) {
			AddBodySection();
		}
	}

	/*Private*/

	private Vector3 CalculateDirection() {
		/**/
		float Horizontal = Input.GetAxis(Utils.HORIZONTAL);
		float Vertical = Input.GetAxis(Utils.VERTICAL);
		Vector3 NewDirection = Vector3.zero;

		if (Horizontal != 0 && Input.GetButtonDown("Horizontal")) {
				NewDirection = Vector3.right*Mathf.Sign(Horizontal);
		}

		if (Vertical !=0 && Input.GetButtonDown("Vertical"))
				NewDirection = Vector3.up*Mathf.Sign(Vertical);

		return NewDirection;
	}

	private void KeepSnakeInBounds() {
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
		transform.eulerAngles=Utils.DirectionToEulerAngles(Direction);
	}

	private void Move() {
		transform.position += Step*Direction;
	}

	private void MoveFollowers() {
		foreach (SnakeFollower sf in Body) {
			sf.Follow();
		}
	}

	private void FaceFollowersSprite() {
		foreach (SnakeFollower sf in Body) {
			sf.ChooseSprite();
		}
	}

    private void AddBodySection() {
		SnakeFollower NewTail = Instantiate(BodyPrefab);
		SnakeFollower Tail = null;
		if (Body.Count == 0) {
			NewTail.Leader = gameObject;
		} else {
			Tail = Body[Body.Count-1];
			NewTail.Leader = Tail.gameObject;
			Tail.Follower=NewTail.gameObject;
		}
		NewTail.transform.position = NewTail.Leader.transform.position - Direction*Step;
		NewTail.LastLeaderPosition = NewTail.transform.position;

		Body.Add(NewTail);

		NewTail.Follow();
		NewTail.ChooseSprite();

		if (Tail != null) {
			Tail.Follow();
			Tail.ChooseSprite();
		}

	}


}
