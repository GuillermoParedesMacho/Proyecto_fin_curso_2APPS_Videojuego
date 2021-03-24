using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;

public class LoadSceneAsync : MonoBehaviour
{
   
    // Start is called before the first frame update
    public string sceneToLoad;
	AsyncOperation loadingOperation;
	public Image progressBar;
	public Text percentLoaded;

   void Start()
   {
     //   loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
   }

   // Update is called once per frame
   void Update()
   {
       // progressBar.fillAmount += Mathf.Clamp01(loadingOperation.progress / 0.9f);
   		//float progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
       // percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
   }
}
