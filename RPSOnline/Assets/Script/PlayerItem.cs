using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] Image AvatarImage;
    [SerializeField] TMP_Text playerName;
    [SerializeField] Sprite[] avatarSprites;

    public void Set(Photon.Realtime.Player player)
    {
        if (player.CustomProperties.TryGetValue(PropertyNames.Player.AvatarIndex, out var value))
            AvatarImage.sprite = avatarSprites[(int)value];

        playerName.text = player.NickName;

        if (player == PhotonNetwork.MasterClient)
            playerName.text += player.NickName + " (Master)";

    }
}
