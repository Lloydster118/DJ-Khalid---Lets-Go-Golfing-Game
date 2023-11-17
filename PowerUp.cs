using UnityEngine;
using TMPro;
using System.Diagnostics;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class PowerUp : MonoBehaviour
{
    [SerializeField] private GameObject powerUp;//storing the powerup game object

    [SerializeField] private Ball ball;//referencing the ball script

    [SerializeField] private GameObject powerUpUI;//storing the powerup UI

    private void OnTriggerEnter2D(Collider2D collision)//when ball hits powerup it registers
    {
        ball.IncreasePowerUp();//registers the powerup in the inventory

        powerUpUI.SetActive(true);//when activing the power up it registers

        Destroy(powerUp);//removes the powerup from inventory as it has been used
    }

    public void DisablePowerUpUI()
    {
        powerUpUI.SetActive(false);//disables the powerup after it being set true
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}