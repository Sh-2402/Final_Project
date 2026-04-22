using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    public GameObject[] platformSections;
    

    public GameObject triggerSection;
    public GameObject Portal;
    public float zVal = 43;
    [SerializeField] int sectionCount;
    public bool firstinstantiate = false;
    public int counter = 0;


    public void OnTriggerEnter(Collider other)
    {
        counter++;
        int sectionCount = Random.Range(0, 2);
        if ( firstinstantiate == false)
        {
            zVal += 1500;
            firstinstantiate = true;
        }else
        {
            zVal += 1000;
        }
        if(counter > 2)
        {
          Portal.GetComponent<MeshRenderer>().enabled = true;
          Portal.GetComponent<BoxCollider>().enabled = true;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if(counter > 2)
            {
                Instantiate(platformSections[1], new Vector3(-313.1f, 0f, zVal), Quaternion.identity);

            }
            else
            {
                Instantiate(platformSections[sectionCount], new Vector3(-313.1f, 0f, zVal), Quaternion.identity);

                Destroy(triggerSection);
            }
               
        }
        
    }

  

}
