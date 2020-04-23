using System;
using System.Collections.Generic;
using UnityEngine;
public class WeaponPointer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight;
    [SerializeField]
    private Sprite[] Sprites;

    [SerializeField]
    private Transform pos;

    private int symetryAngle = 0;
    private void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();

        symetryAngle = 360 / Sprites.Length;
    }
    void Update ()
    {
        transform.position = pos.position;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)transform.parent.position).normalized;

        if (transform.parent.position.x < mousePos.x)
            isFacingRight = true;
        else
            isFacingRight = false;

        float angle = Vector2.Angle (Vector2.up, dir);

        int index = Mathf.RoundToInt (angle / symetryAngle);
        if(!isFacingRight)
        spriteRenderer.sprite = Sprites[index];
        else
        {
            spriteRenderer.sprite = Sprites[Sprites.Length-index];
        }
    }
}
