using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [Header("Handle Gripper")]
    [SerializeField] private HandleGripper handleGripper;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Button gripperOpenButton;
    [SerializeField] private Button gripperCloseButton;
    [SerializeField] private Slider distanceSlider;
    [SerializeField] private Button customDistanceButton;
    [SerializeField] private TMP_Text distanceSliderText;

    [Header("Move Gripper")]
    [SerializeField] private Button moveGripperUpButton;
    [SerializeField] private Button moveGripperDownButton;
    [SerializeField] private Button moveGripperPos1Button;
    [SerializeField] private Button moveGripperPos2Button;

    [Header("Dice Spawn")]
    [SerializeField] private Transform cadRoot;
    [SerializeField] private Button spawnNewDiceButton;
    [SerializeField] private GameObject smallDicePrefab;
    [SerializeField] private GameObject bigDicePrefab;
    private GameObject dice;


    private void Awake()
    {
        Application.targetFrameRate = 60;

        speedSlider.onValueChanged.AddListener((float value) => handleGripper.speedValue = value);
        speedSlider.onValueChanged?.Invoke(speedSlider.value);

        gripperOpenButton.onClick.AddListener(() => {
            handleGripper.moveValueLeft = handleGripper.openMoveValue;
            handleGripper.moveValueRight = handleGripper.openMoveValue;
        });

        gripperCloseButton.onClick.AddListener(() => {
            handleGripper.moveValueLeft = handleGripper.closeMoveValue;
            handleGripper.moveValueRight = handleGripper.closeMoveValue;
        });

        distanceSlider.onValueChanged.AddListener((float value) => distanceSliderText.text = distanceSlider.value.ToString("F1") + "°");
        distanceSlider.onValueChanged?.Invoke(0);
        customDistanceButton.onClick.AddListener(() =>
        {
            handleGripper.moveValueLeft = distanceSlider.value;
            handleGripper.moveValueRight = distanceSlider.value;
        });

        moveGripperDownButton.onClick.AddListener(() => handleGripper.targetYPos = 1.127f);
        moveGripperUpButton.onClick.AddListener(() => handleGripper.targetYPos = 1.327f);
        moveGripperPos1Button.onClick.AddListener(() => handleGripper.targetXPos = 0f);
        moveGripperPos2Button.onClick.AddListener(() => handleGripper.targetXPos = 0.3f);
        moveGripperDownButton.onClick?.Invoke();
        moveGripperPos1Button.onClick?.Invoke();

        spawnNewDiceButton.onClick.AddListener(() =>
        {
            if (dice != null) DestroyImmediate(dice);
            dice = Instantiate(Random.Range(0,2) == 0 ? smallDicePrefab : bigDicePrefab, cadRoot);
            dice.transform.localPosition = new Vector3(Random.Range(-0.035f, -0.148f), -0.12299f, -0.0035f);
        });
    }
}
