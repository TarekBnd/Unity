using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{

    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI dash;

    private static CanvasManager _instance;
    public static CanvasManager Instance
    {
        get {return _instance;}
    }

    public void Awake()
    {
        if(_instance !=null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else
        {
            _instance = this;
        }
    }

    public void UpdateHealth(int healthValue){
        health.text = healthValue.ToString() + "%";
    }

    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString() + "%";
    }

    public void UpdateDash(int dashCd)
    {
        
    }


}
