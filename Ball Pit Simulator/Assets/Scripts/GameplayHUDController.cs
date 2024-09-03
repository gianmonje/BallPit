        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayHUDController : MonoBehaviour {
    [SerializeField] private GameObject holder;
    [SerializeField] private Image crosshairImage;

    [SerializeField] private Color hasTargetColor, noTargetColor;

    public void SetVisibilitiy(bool isVisible) {
        holder.SetActive(isVisible);
    }

    public void SetTarget(bool hasTarget) {
        crosshairImage.color = hasTarget ? hasTargetColor : noTargetColor;
    }
}
