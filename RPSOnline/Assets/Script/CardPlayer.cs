using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPlayer : MonoBehaviour
{
    [SerializeField] Card chosenCard;
    public Transform atkPosRef;
    // ==============================

    // =========== Health ===========
    [SerializeField] private TMP_Text nameText;
    public TMP_Text healthText;
    public HealthBar healthBar;
    public float Health;
    public PlayerStats stats = new PlayerStats
    {
        MaxHealth = 100,
        RestoreValue = 5,
        DamageValue = 10
    };
    // ==============================

    private Tweener animationTweener;
    public TMP_Text NickName { get => nameText; }
    public bool IsReady = false;

    private void Start()
    {
        Health = stats.MaxHealth;
    }

    public void SetStats(PlayerStats newStats, bool restorFullHealth = false)
    {
        this.stats = newStats;
        if (restorFullHealth)
            Health = stats.MaxHealth;

        UpdateHealthBar();

    }

    public Attack? AttackValue
    {
        // get => chosenCard == null ? null : chosenCard.AttackValue;
        get
        {
            if (chosenCard == null)
            {
                return null;
            }
            else
                return chosenCard.AttackValue;
        }
    }


    // =================================================
    // script for logic Card
    public void Reset()
    {
        Debug.Log(chosenCard);

        if (chosenCard != null)
        {
            Debug.Log("Reset");
            chosenCard.Reset();

        }
        chosenCard = null;
    }

    public void SetchosenCard(Card newCard)
    {
        if (chosenCard != null)
        {
            chosenCard.transform.DOKill();
            chosenCard.Reset();
        }
        chosenCard = newCard;
        chosenCard.transform.DOScale(chosenCard.transform.localScale * 1.2f, 0.2f);
    }
    // =================================================

    // =================================================
    // script for logic Health
    public void ChangeHealth(float amount)
    {
        Health += amount;
        Health = Mathf.Clamp(Health, 0, stats.MaxHealth);
        UpdateHealthBar();

    }

    public void UpdateHealthBar()
    {
        // HealthBar
        healthBar.UpdateBar(Health / stats.MaxHealth);
        // Text health
        healthText.text = Health + "/" + stats.MaxHealth;
    }
    // =================================================


    // ====================================================
    // Script For Animation
    public void AnimateAttack()
    {
        animationTweener = chosenCard.transform.DOMove(atkPosRef.position, 1);
    }

    public void AnimateDamage()
    {
        var image = chosenCard.GetComponent<Image>();
        animationTweener = image.DOColor(Color.red, 0.1f).SetLoops(3, LoopType.Yoyo).SetDelay(0.5f);
    }

    public void AnimateDraw()
    {
        animationTweener = chosenCard.transform
            .DOMove(chosenCard.OriginalPos, 1)
            .SetEase(Ease.InBack)
            .SetDelay(0.2f);
    }
    public bool IsAnimating()
    {
        return animationTweener.IsActive();
    }

    public void isClickable(bool value)
    {
        Card[] cards = GetComponentsInChildren<Card>();
        foreach (var card in cards)
        {
            card.SetClikable(value);
        }
    }
}
