using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] float stamina;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public float getStamina()
    {
        return stamina;
    }
    public float getLife()
    {
        return life;
    }
    public void setLife(float life)
    {
        this.life = life;
    }
    public void setStamina(float stamina)
    {
        this.stamina = stamina;
    }
}
