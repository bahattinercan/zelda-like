using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public Text placeText;
    private float textTime = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPos += cameraChange;
            cam.maxPos += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(PlaceNameCo());
            }
        }
    }

    private IEnumerator PlaceNameCo()
    {
        placeText.gameObject.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(textTime);
        placeText.gameObject.SetActive(false);
    }
}