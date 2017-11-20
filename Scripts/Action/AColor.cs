using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AColor : MonoBehaviour {
	public Color colorBy = new Color(1,1,1,1);
	public Color startColor = new Color(1,1,1,1);
	public SpriteRenderer spriteRenderer = null;

	Color speed;
	Color max;
	Color min;
	public float time = 1;
	float direction = 1;
    // Use this for initialization
    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }
    }
    void Start () {
		
        UpdateStatus();
	}
	public void SetColorData(Color _colorBy,Color _startColor)
    {
		colorBy = _colorBy;
		startColor = _startColor;
        UpdateStatus();
	}
    public void SetTime(float _time)
    {
        time = _time;
        UpdateStatus();
    }
	public void UpdateStatus(){
        if (spriteRenderer != null)
        {
            spriteRenderer.color = startColor;
            speed = colorBy / time;
            max = colorBy + spriteRenderer.color;
            min = spriteRenderer.color;
        }
	}
	// Update is called once per frame
	void Update () {
		Color c = spriteRenderer.color + speed * Time.deltaTime *direction;
		if (c.r > max.r){
			direction = -direction;
			spriteRenderer.color = max;

		}else if(c.r < min.r){
			direction = -direction;
			spriteRenderer.color = min;
		}
		else{
			spriteRenderer.color = c;
		}
	}

}
