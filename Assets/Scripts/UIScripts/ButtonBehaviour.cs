using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour
{
    Button pb;
    Image img;
    public Sprite newImage;

    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        img = img.GetComponent<Image>();
        pb.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        //img.sprite = newImage;
        Debug.Log("You have clicked the button!");
    }
}
/*public class ButtonBehaviour : MonoBehaviour
{ 
    Image m_Image;
    //Set this in the Inspector
    public Sprite m_Sprite;

    void Start()
    {
        //Fetch the Image from the GameObject
        m_Image = GetComponent<Image>();
    }

    void Update()
    {
        //Press space to change the Sprite of the Image
        if (Input.GetKey(KeyCode.Mouse0))
        {
            m_Image.sprite = m_Sprite;
        }
    }
}*/
