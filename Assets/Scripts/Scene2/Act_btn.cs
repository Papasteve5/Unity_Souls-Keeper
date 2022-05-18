using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Act_btn : MonoBehaviour
{

    public Button Act_button;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = Act_button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() {
        Debug.Log("Ha! Your mom gae!");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
