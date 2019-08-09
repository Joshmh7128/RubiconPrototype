using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{

	public int hp;
	public GameObject healthBar;
	public Animator healthAnim;
	public Animator flashAnim;
	private int maxHP = 6;

	// Start is called before the first frame update
	void Start()
    {
        hp = maxHP;
        healthBar.transform.localScale = new Vector3(1, 1, 1);
    }

    public void TakeDamage(int taken)
    {
        hp -= taken;
        healthAnim.Play("HPbump");
        if(hp < 0)
        {
            hp = 0;
        }
        flashAnim.Play("redFlash");
        healthBar.transform.localScale = new Vector3((float)hp / maxHP, 1, 1);
    }
}
