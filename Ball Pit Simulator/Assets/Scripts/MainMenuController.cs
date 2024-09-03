using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public GameObject holder;

    public TMP_InputField spawnInputField;
    public Slider spawnSlider;
    public Button playButton;

    public int ballsToSpawnCount;

    private string currentInputFieldValue;

    public void SetVisibilitiy(bool isVisible) {
        holder.SetActive(isVisible);
    }

    #region Initialization
    public void Initialize() {
        SetVisibilitiy(true);

        currentInputFieldValue = string.Empty;
        spawnInputField.placeholder.GetComponent<TextMeshProUGUI>().text = $"1-{GameConfig.PoolSize}";
        spawnInputField.SetTextWithoutNotify(currentInputFieldValue);
        spawnSlider.SetValueWithoutNotify(1);
        spawnSlider.maxValue = GameConfig.PoolSize;
        playButton.gameObject.SetActive(false);

        SetupEvents();
    }

    public void DeInitialize() {
        RemoveEvents();

        SetVisibilitiy(false);
    }
    #endregion

    #region EVENTS
    private void SetupEvents() {
        spawnInputField.onValueChanged.AddListener(OnInputValueChanged);
        spawnSlider.onValueChanged.AddListener(OnSliderValueChanged);
        playButton.onClick.AddListener(OnClickPlayButton);
    }

    private void RemoveEvents() {
        spawnInputField.onValueChanged.RemoveAllListeners();
        spawnSlider.onValueChanged.RemoveAllListeners();
        playButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region Event Callbacks
    private void OnInputValueChanged(string value) {
        if (int.TryParse(value, out int outValue)) {
            if(outValue < 1 || outValue > GameConfig.PoolSize) {
                spawnInputField.SetTextWithoutNotify(currentInputFieldValue);
                playButton.gameObject.SetActive(false);
            } else {
                currentInputFieldValue = value;
                spawnSlider.SetValueWithoutNotify(outValue);
                playButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnSliderValueChanged(float value) {
        string stringValue = value.ToString();
        spawnInputField.SetTextWithoutNotify(stringValue);
        currentInputFieldValue = stringValue;
        playButton.gameObject.SetActive(true);
    }

    private void OnClickPlayButton() {
        if (int.TryParse(spawnInputField.text, out int outValue)) {
            ballsToSpawnCount = outValue;
        }
            
        GameManager.GameManagerStateMachine.SetState<GameplayState>();
    }
    #endregion
}
