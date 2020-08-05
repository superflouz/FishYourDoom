using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootingManager : MonoBehaviour
{
    private Transform progressBar;
    private Transform mask;

    private bool lootingInProgress = false;
    private Lootable currentLoot;
    private Action currentCallback;

    private PlayerInput playerInput;

    void Awake()
    {
        progressBar = GameObject.Find("LootingUI").transform.GetChild(0);
        mask = progressBar.GetChild(1);

        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
    }

    public void Loot(Lootable loot, Action callback)
    {
        if (!lootingInProgress) {
            lootingInProgress = true;
            playerInput.SwitchCurrentActionMap("NoControls");

            currentLoot = loot;
            currentCallback = callback;

            SetPercentage(0);
            StopCoroutine("Looting");
            StartCoroutine("Looting");
        }
    }

    public IEnumerator Looting()
    {
        float timeCounter = 0;

        if (currentLoot.timeToLoot > 0) {
            DisplayBar();
        }

        while (timeCounter < currentLoot.timeToLoot) {
            timeCounter += Time.deltaTime;
            SetPercentage(timeCounter / currentLoot.timeToLoot);
            yield return null;
        }
        playerInput.SwitchCurrentActionMap("Player");
        lootingInProgress = false;

        currentCallback();

        HideBar();
    }

    public void Interrupt()
    {
        StopCoroutine("Looting");
        HideBar();

        lootingInProgress = false;
        currentLoot = null;
        currentCallback = null;

        playerInput.SwitchCurrentActionMap("Player");
    }

    private void SetPercentage(float percent)
    {
        float newWidth = percent * 100;
        mask.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
    }

    private void DisplayBar()
    {
        progressBar.gameObject.SetActive(true);
    }

    private void HideBar()
    {
        progressBar.gameObject.SetActive(false);
    }
}
