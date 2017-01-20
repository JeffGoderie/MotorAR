using UnityEngine;
 using System;
 
 public class Target: MonoBehaviour {
 
     SpriteRenderer   sprite;
     CircleCollider2D circCollider;
     public Color onColor, offColor;
 
     Vector2  position;
     Vector3  touchPoint, localTouchPoint;

     public float m_HalfDistance = 0.5f;
 
  
     private Vector3 m_BoardInitPos;

     public void Reset(Vector3 pos)
     {
         m_BoardInitPos = pos;
         transform.position = m_BoardInitPos;
     }
 }