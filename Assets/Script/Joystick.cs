using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joysticker : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    public float m_speed = 50f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    public Image circle;
    public Image outerCircle;
    private ControllerJoystck controllerJoystck;

    public float lenghtZero;

    private Touch touch;

    private void Start()
    {
        controllerJoystck = gameObject.GetComponent<ControllerJoystck>();
        circle.GetComponent<Image>();
        outerCircle.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position).x > 0.5f
        if (Input.touchCount <= 0 )
            return;
        touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            // pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            pointA = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);

            circle.transform.position = pointA;// * -1;
            outerCircle.transform.position = pointA;// * -1;
            circle.enabled = true;
            outerCircle.enabled = true;
        }
        if (touch.phase == TouchPhase.Moved)
        {
            touchStart = true;
           // pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            pointB = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
        }
        else
        {
          //  touchStart = false;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            touchStart = false;
            circle.GetComponent<RectTransform>().rect.Set(0,0,0,0); ;
        }
    }
    private void FixedUpdate()
    {
        //Camera.main.transform.position = new Vector3(transform.position.x + 5, 0, -10);

        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 100.0f);
            //direction *= -1;
            //Vector3 dir = new Vector3(1 * m_speed, Mathf.Clamp(direction.y,-0.1f,0.1f) * speed);
            if (direction.y > lenghtZero)
            {
                controllerJoystck.Press_Up(true);
                controllerJoystck.Press_Down(false);
            }
            else if (direction.y < -lenghtZero)
            {
                controllerJoystck.Press_Up(false);
                controllerJoystck.Press_Down(true);
            }
            else
            {
                controllerJoystck.Press_Up(false);
                controllerJoystck.Press_Down(false);
            }
            circle.transform.position = new Vector2(pointA.x , pointA.y + direction.y);// * -1;
        }
        else
        {
            controllerJoystck.Press_Up(false);
            controllerJoystck.Press_Down(false);
            // Vector3 dir = new Vector3(1 * m_speed, 0);
            circle.enabled = false;
            outerCircle.enabled = false;
            
        }
        //player.Translate(Vector2.right * m_speed * Time.deltaTime);
    }
    //void moveCharacter(Vector2 direction)
    //{
    //    player.Translate(direction * Time.deltaTime);
    //    //transform.position = new Vector3(0,direction.y * speed * Time.deltaTime);
    //}
}