using UnityEngine;

public class CSS_SpriteScroller : MonoBehaviour {

    [SerializeField] Vector2 moveSpeed;

    Vector2 offset;
    Material material;

    void Awake() {
        //Get material
        material = GetComponent<SpriteRenderer>().material;
    }

    void Update() {
        //Set offset of material frame independent
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
