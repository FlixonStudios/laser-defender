using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{

    [SerializeField] Vector3 offsetFromPlayer = new Vector3(0, -1.5f, 0);

    [SerializeField] TextMeshProUGUI healthText;
    Player player;
    float playerMoveSpeed;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame



    void Update()
    {
        CheckPlayerExists();
        UpdatePlayerHealth();
        UpdateHealthTextLocation();
    }
    public void UpdateHealthTextLocation()
    {
        Vector3 HealthTextPos = Camera.main.WorldToScreenPoint(this.transform.position);
        healthText.transform.position = HealthTextPos + offsetFromPlayer;

    }
    private void UpdatePlayerHealth()
    {
        healthText.text = player.GetHealth().ToString();
    }

    private void CheckPlayerExists()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

    }
    private void CheckHealthTextExists()
    {
        if (healthText == null)
        {
            //healthText = FindObjectOfType<Tex>
        }
    }
    public void DestroyHealthText()
    {
        healthText.text = "";
        //healthText.gameObject.SetActive(false);
    }
    

}
