using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Attack AttackValue;
    public CardPlayer player;
    public Vector2 OriginalPos;
    Vector2 OriginalScale;
    Color OriginalColor;
    bool isClickable = true;

    private void Start()
    {
        OriginalPos = this.transform.position;
        OriginalScale = this.transform.localScale;
        OriginalColor = this.GetComponent<Image>().color;

    }

    public void OnClick()
    {
        if (isClickable)
        {
            player.SetchosenCard(this);
        }


    }

    internal void Reset()
    {
        transform.position = OriginalPos;
        transform.localScale = OriginalScale;
    }

    public void SetClikable(bool value)
    {
        isClickable = value;
    }
}
