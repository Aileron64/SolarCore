using UnityEngine;
using System.Collections;

public class Bubble : BaseAttack 
{
    float maxSize;
    float size;
    bool isClosing = false;

    public Turq owner;

    protected override void Start() 
    {
        //damage = 800;

        maxSize = transform.localScale.x;
        transform.localScale = new Vector3(1, 1, 1);

        base.Start();
	}

    protected override void Update()
    {
	    if (size < maxSize && !isClosing)
        {
            size += Time.deltaTime * 500;  
        }
        else if (!isClosing)
        {
            size = maxSize;
        }
        else
        {
            size -= Time.deltaTime * 500;

            if (size <= 1)
            {
                //owner.EndBubble();

                size = 1;
                isClosing = false;
                gameObject.SetActive(false);
            }
        }

        transform.localScale = new Vector3(size, size, size);
	}

    public void Close()
    {
        isClosing = true;
    }
}
