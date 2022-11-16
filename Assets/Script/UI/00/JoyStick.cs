using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class JoyStick : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public float JoyStickMoveRadius=100f;

    private float distance;

    private Vector3 originPos;

    private Vector3 dir;

    public float speed=6f;

    private Transform player;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        dir = Input.mousePosition - originPos;
        distance = dir.magnitude;

        if(distance<JoyStickMoveRadius)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            transform.position = dir.normalized*JoyStickMoveRadius + originPos;
        }       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originPos;
        dir = Vector3.zero;
    }

    // Start is called before the first frame update
    void Awake()
    {
        originPos = transform.position;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }
    public void PlayerControl()
    {
        player.position += new Vector3(dir.x,0,dir.y).normalized * Time.deltaTime * speed;
    }
}
