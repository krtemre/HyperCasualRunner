using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paint : MonoBehaviour
{
    public Camera mainCam;
    public GameObject brush;
    public Text txt;
    public float brushSize = 1f;
    public bool paint = true;
    int count;
    public GameObject gameManager;
    void Update()
    {
        if(paint == true)
        {
            if (Input.GetMouseButton(0))
            {
                //cast a ray to the plane
                var Ray = mainCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(Ray, out hit) && hit.transform.gameObject.tag == "Paintable")
                {
                    //instanciate a brush
                    var go = Instantiate(brush, hit.point + Vector3.forward * -0.1f,  //Get Visible by moving towards the camera
                        new Quaternion(0, 0, 0, 0), transform);

                    go.transform.localScale = new Vector3(brushSize, 0.1f, brushSize);
                }
            }
        }
        count = (gameObject.transform.childCount / 2);
        if(count < 100)
        {
            txt.text = count.ToString() + "%";
        }
        else
        {
            txt.text =  "100%";
            paint = false;
            gameManager.GetComponent<GameManager>().NextLevel();
        }
    }
}
