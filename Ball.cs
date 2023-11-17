using UnityEngine;

public class Ball : MonoBehaviour {
	[Header("References")]
	[SerializeField] private Rigidbody2D rb;//control of an objects physics simulation
	[SerializeField] private LineRenderer lr;//Renders the line for power of the shot
	[SerializeField] private GameObject goalFX;//renders the goalfx for when your put it in the hole
	[SerializeField] private PowerUp powerUpUI;//renders the powerup display

	[Header("Attributes")]
	[SerializeField] private float maxPower = 15f;//max speed ball can travel
	[SerializeField] private float power = 2f;//power mutiplyer
	[SerializeField] private float maxGoalSpeed = 4f;//max speed the ball can enter the hole
	[SerializeField] private int powerUp = 0;//defining powerup at start of the game so you have none
	
	private bool isDragging;//checks if ball is being shot
	private bool inHole;//checks if ball is in hole

	private void Update() {
		PlayerInput();

		if (LevelManager.main.outOfStrokes && rb.velocity.magnitude <= 0.2f && !LevelManager.main.levelCompleted) {
			LevelManager.main.GameOver();//checks if out of strokes, moving and if the level is not completed.
										 //If conditions are met it will display the game over screen.
		}
	}

	private bool IsReady() {
		return rb.velocity.magnitude <= 0.2f;//returns false if the ball is moving faster than 2f
	}

	private void PlayerInput() {

		if (Input.GetKeyDown("space") && powerUp > 0)//defining what key to press when you have the powerup
		{
			LevelManager.main.IncreaseMaxStroke();//increases the max stroke displayed on the UI when pressing the key to use the powerup
			powerUpUI.DisablePowerUpUI();//disabling the powerup so you can reuse it
			powerUp--;
		}

		if (!IsReady()) return;

		Vector2 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//gets the mouse position on the screen as world postion
		float distance = Vector2.Distance(transform.position, inputPos);//gets the postion of the ball to the mouse

		if (Input.GetMouseButtonDown(0) && distance <= 0.5f) DragStart();//if the mouse is close enough the ball, it starts the drag function
		if (Input.GetMouseButton(0) && isDragging) DragChange(inputPos);//calls the drag chance method and passes the mouse postion
		if (Input.GetMouseButtonUp(0) && isDragging) DragRelease(inputPos);//mouse position when realsed is passed
	}

	private void DragStart() {
		isDragging = true;
		lr.positionCount = 2;
	}

	private void DragChange(Vector2 pos) {//sets the postion of the line render to the oppisite of the mouse position
		Vector2 dir = (Vector2)transform.position - pos;

		lr.SetPosition(0, transform.position);
		lr.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2));//that clamps it to the max power
	}

	private void DragRelease(Vector2 pos) {//calulates the distance from the current postion the released postion
		float distance = Vector2.Distance((Vector2)transform.position, pos);
		isDragging = false;
		lr.positionCount = 0;//clears any drawn lines once it registers it is not dragging anymore

		if (distance < 1f) {
			return;//checks if the distance goes above 1, if it doesnt, it will return and start again till it is over 1 and not fire the ball
				   //if it goes above 1 it will fire and complete the 
		}

		LevelManager.main.IncreaseStroke();//tells the level manager to increase stroke

		Vector2 dir = (Vector2)transform.position - pos;//calculates the current postion to the released postion

		rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);//sets the velocitys based on the direction, which is then multiplied by the power factor
																	//and set within a max power speed band so it cant go over.
	}

	private void CheckWinState() {//checks if the ball is in the hole to register a "win"
		if (inHole) return;

		if (rb.velocity.magnitude <= maxGoalSpeed) {//checks if the rigidbody is less than or equal to max goal speed set so it can register a "goal"
			inHole = true;

			rb.velocity = Vector2.zero;//deactivates the ball and velocity of the ball when scored
			gameObject.SetActive(false);

			GameObject fx = Instantiate(goalFX, transform.position, Quaternion.identity);//activates a goal visual fx when registered a "goal"
			Destroy(fx, 2f);

			LevelManager.main.LevelComplete();//calls the level manager to say that the level has been completed
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {//checks if the triggering collider has the tag goal
		if (other.tag == "Goal") CheckWinState();
	}

	private void OnTriggerStay2D(Collider2D other) {//making the triggering collider stay in contact with the tag goal to check the status of the "win"
		if (other.tag == "Goal") CheckWinState();
	}

	public void IncreasePowerUp()//increases the powerup variable
	{
		powerUp++;
	}

}