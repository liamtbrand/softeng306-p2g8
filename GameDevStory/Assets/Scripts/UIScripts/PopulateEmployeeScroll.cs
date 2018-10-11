using UnityEngine;

public class PopulateEmployeeScroll : MonoBehaviour {

    public GameObject Content;
    public GameObject EmployeeItemTemplate;

    public void Start()
    {
        Reload();
    }

    public void Reload()
    {
        for (int i = 0; i < 10; i++)
        {
            var copy = Instantiate(EmployeeItemTemplate);
            copy.transform.parent = Content.transform;
        }
    }

}
