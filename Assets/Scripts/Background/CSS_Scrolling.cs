using UnityEngine;

public class CSS_Scrolling : MonoBehaviour {

    [SerializeField] float speed = 4f;
    [SerializeField] float lastPosition = -37.10443f;
    Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        //Move sprite frams independent
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        //Once sprite reaches end reset position to start
        if (transform.position.y < lastPosition) {
            transform.position = startPosition;
        }
        
    }
}
