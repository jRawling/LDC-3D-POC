using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CameraMenuController : MonoBehaviour {

    LinkedListNode<Position> currentPosition;
    LinkedList<Position> positions = new LinkedList<Position>();
    public Transform target;
    bool isMoving = false;

    public Vector3 axis = Vector3.up;
    public float radius = 2.0f;
    public float radiusSpeed = 1.0f;
    public float rotationSpeed = 80.0f;
    public float speed;


    public void TogglePerspective()
    {
        Camera.main.orthographic = !Camera.main.orthographic;
    }

    public void Start()
    {
        Position lside = new Position(new Vector3(0.0f, 5.5f, 13.0f), new Vector3(0.0f, 4.0f, 0.0f));
        positions.AddFirst(lside);

        Position front = new Position(new Vector3(18.0f, 3.0f, 0.0f), new Vector3(0.0f, 3.0f, 0.0f));
        positions.AddLast(front);

        Position rside = new Position(new Vector3(0.0f, 5.5f, -13.0f), new Vector3(0.0f, 4.0f, 0.0f));
        positions.AddLast(rside);

        //    Position top = new Position(new Vector3(0.0f, 15.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f));
        //   positions.AddLast(top);

        Position back = new Position(new Vector3(-18.0f, 5.5f, 0.0f), new Vector3(0.0f, 2.5f, 0.0f));
        positions.AddLast(back);





        currentPosition = positions.First;
    }

    void Update()
    {
        if (isMoving)
        {
            Camera camera = Camera.main;
          //   camera.transform.position = Vector3.Lerp(camera.transform.position, currentPosition.Value.vector, Time.deltaTime * 5);
            //camera.transform.LookAt(new Vector3(0.0f, 4.0f, 0.0f));

            //    camera.transform.RotateAround(target.position, axis, rotationSpeed * Time.deltaTime);
            //    var desiredPosition = (camera.transform.position - target.position).normalized * radius + target.position;
            //    camera.transform.position = Vector3.MoveTowards(camera.transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
            float step = speed * Time.deltaTime;
            Camera.main.transform.position = Vector3.RotateTowards(Camera.main.transform.position, currentPosition.Value.vector, step, 1.0f);
            camera.transform.LookAt(currentPosition.Value.lookAt);
        //    Vector3 pos = Camera.main.transform.position;
         //   Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 5.0f, Camera.main.transform.position.z);
       //     Vector3 pos2 = Camera.main.transform.position;




        }
    }

    public void NextPosition()
    {
        SetPosition(true);
    }

    public void PreviousPosition()
    {
        SetPosition(false);
    }

    void SetPosition(bool isNextPosition)
    {
        if(isNextPosition)
        {
            currentPosition = (currentPosition.Next == null ? positions.First : currentPosition.Next);
        }
        else
        {
            currentPosition =(currentPosition.Previous == null ? positions.Last : currentPosition.Previous);
        }

        isMoving = true;
    }

    private void MoveToPosition(Vector3 position)
    {
        if(isMoving) { return; }
        Camera.main.transform.LookAt(target);
    }

    internal class Position
    {
        public Vector3 vector;
        public Vector3 lookAt;

        public Position(Vector3 vector, Vector3 lookAt)
        {
            this.vector = vector;
            this.lookAt = lookAt;
        }
    }
}
