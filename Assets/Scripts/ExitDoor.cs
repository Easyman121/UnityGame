using UnityEditor;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public Transform Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
	{
		if (Player)
		{
			float dist = Vector3.Distance(Player.position, transform.position);
			if (dist < 5f)
			{
				if (Input.GetMouseButtonDown(0))
				{
					#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
                    #else
                    Application.Quit();
                    #endif
				}
			}
		}
	}	
}

