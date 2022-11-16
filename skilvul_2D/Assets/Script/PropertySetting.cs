using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PropertySetting : MonoBehaviourPunCallbacks
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] string propertyKey;

    [SerializeField] float initialValue = 50;
    [SerializeField] float minValue = 0;
    [SerializeField] float maxValue = 100;
    // [SerializeField] bool wholeNumbers = true;

    private void Start()
    {
        slider.interactable = PhotonNetwork.IsMasterClient;
        inputField.interactable = PhotonNetwork.IsMasterClient;

        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(propertyKey, out var value))
        {
            UpdateSliderInputField((int)value);
        }
        else
        {
            UpdateSliderInputField(initialValue);

            UpdateSliderInputField(initialValue);
        }

        slider.minValue = minValue;
        slider.maxValue = maxValue;

    }

    public void InputFromSlider(float value)
    {
        UpdateSliderInputField(value);
    }

    public void InputFromField(string stringValue)
    {
        if (int.TryParse(stringValue, out var intValue))
        {
            intValue = Mathf.Clamp(intValue, (int)minValue, (int)maxValue);
            UpdateSliderInputField(intValue);
            SetCustomProperty(intValue);
        }
    }

    private void SetCustomProperty(float value)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        Hashtable property = new Hashtable();
        property.Add(propertyKey, value);
        PhotonNetwork.CurrentRoom.SetCustomProperties(property);

    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.TryGetValue(propertyKey, out var value) && PhotonNetwork.IsMasterClient == false)
        {
            UpdateSliderInputField((float)value);
        }

    }

    private void UpdateSliderInputField(float value)
    {
        slider.value = value;
        inputField.text = (Mathf.RoundToInt(value)).ToString("D");
    }
}
