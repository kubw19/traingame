// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;

public class TouchCamera : MonoBehaviour {

    BoxCollider2D BoundsBox;
    Vector3 MaxBounds;
    Vector3 MinBounds;

    float HalfHeight;
    float HalfWidth;
    float ClampedX;
    float ClampedY;

	Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	float oldTouchDistance;


    void Start()
    {
        BoundsBox = GameObject.Find("floor").GetComponent<BoxCollider2D>();
        MinBounds = BoundsBox.bounds.min;
        MaxBounds = BoundsBox.bounds.max;

    }

    void LimitCamera()
    {
        HalfHeight = this.GetComponent<Camera>().orthographicSize;
        HalfWidth = HalfHeight * Screen.width / Screen.height;
        ClampedX = Mathf.Clamp(transform.position.x, MinBounds.x + HalfWidth, MaxBounds.x - HalfWidth);
        ClampedY = Mathf.Clamp(transform.position.y, MinBounds.y + HalfHeight, MaxBounds.y - HalfHeight);
        transform.position = transform.TransformDirection(new Vector3(ClampedX, ClampedY, transform.position.z));

    }
    void Update() {
        if (!TrainGame.Generator().PreventFromZoom) { 
            if (Input.touchCount == 0) {
                oldTouchPositions[0] = null;
                oldTouchPositions[1] = null;
            }
            else if (Input.touchCount == 1) {//Jeśli jedno dotknięcie
                if (oldTouchPositions[0] == null || oldTouchPositions[1] != null) {
                    oldTouchPositions[0] = Input.GetTouch(0).position;
                    oldTouchPositions[1] = null;
                }
                else {
                    Vector2 newTouchPosition = Input.GetTouch(0).position;
                    transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * this.GetComponent<Camera>().orthographicSize / this.GetComponent<Camera>().pixelHeight * 2f));
                    LimitCamera();
                    oldTouchPositions[0] = newTouchPosition;                    
                }
            }
            else {
                if (oldTouchPositions[1] == null) {
                    oldTouchPositions[0] = Input.GetTouch(0).position;
                    oldTouchPositions[1] = Input.GetTouch(1).position;
                    oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
                    oldTouchDistance = oldTouchVector.magnitude;
                }
                else {
                    Vector2 screen = new Vector2(this.GetComponent<Camera>().pixelWidth, this.GetComponent<Camera>().pixelHeight);

                    Vector2[] newTouchPositions = {
                    Input.GetTouch(0).position,
                    Input.GetTouch(1).position
                };


                    Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
                    float newTouchDistance = newTouchVector.magnitude;

                    transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * this.GetComponent<Camera>().orthographicSize / screen.y));
                    this.GetComponent<Camera>().orthographicSize *= oldTouchDistance / newTouchDistance;
                    if (this.GetComponent<Camera>().orthographicSize > 12) this.GetComponent<Camera>().orthographicSize = 12;
                    if (this.GetComponent<Camera>().orthographicSize < 1) this.GetComponent<Camera>().orthographicSize = 1;
                    transform.position -= transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * this.GetComponent<Camera>().orthographicSize / screen.y);

                    LimitCamera();

                    oldTouchPositions[0] = newTouchPositions[0];
                    oldTouchPositions[1] = newTouchPositions[1];
                    oldTouchVector = newTouchVector;
                    oldTouchDistance = newTouchDistance;
                }
            }

    }
	}
}
