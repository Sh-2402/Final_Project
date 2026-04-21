using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject[] platformSections;
    

    public GameObject triggerSection;
    public float zVal = 43;
    [SerializeField] int sectionCount;
    public bool firstinstantiate = false;



    public void OnTriggerEnter(Collider other)
    {
        int sectionCount = Random.Range(0, 2);
        if ( firstinstantiate == false)
        {
            zVal += 1500;
            firstinstantiate = true;
        }else
        {
            zVal += 1000;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(platformSections[sectionCount], new Vector3(-313.1f, 0f, zVal), Quaternion.identity);

            Destroy(triggerSection);
        }
    }

  

}
