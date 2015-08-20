using UnityEngine;
using System.Collections;


public class Managers : SingletonSystem<Managers> 
{
   


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy (this);
            return;
        }
        DontDestroyOnLoad (this.gameObject);
        
      
    }


    ~Managers()
    {
      
    }
        
	public void SceneChage(int sceneNumber)
	{
		switch(sceneNumber)
		{
		case 0:
			Application.LoadLevel(0);
			break;
		case 1:
			Application.LoadLevel(1);
			break;
		
		}

	}
	
	public void LoggedIn ()
	{
		PlayerPrefs.SetInt ("LoggedIn", 1);
	}
	
	public  void LoggedOut ()
	{
		PlayerPrefs.SetInt ("LoggedIn", 0);
	}
	
	public bool IsLoggedIn ()
	{
		if (PlayerPrefs.GetInt ("LoggedIn") == 1)
			return true;
		else
			return false;
	}
	public void ExitOnBackButton()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) { 
			
		
			Application.Quit(); 
		}
	}
	void Update()
	{
//		ExitOnBackButton();
	}
}
