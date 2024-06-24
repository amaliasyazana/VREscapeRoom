// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PickUp : MonoBehaviour
// {
//     GameObject startTrans;
//     Transform Controller;
//     static public bool pickedUp = false;

//     // Start is called before the first frame update
//     void Start()
//     {
//         startTrans = new GameObject();

//         startTrans.Transform.position = transform.position;
//         startTrans.Transform.rotation = transform.rotation;
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (pickedUp == true)
//         {
//             transform.position = controller.position;
//             transform.rotation = controller.rotation;
//         }
//         else
//         {
//             transform.position = startTrans.transform.position;
//             transform.rotation = startTrans.transform.rotation;
//         } 
//     }

//     private void onTriggerEnter(Collider other)
//     {
//         pickedUp = true;
//         Controller = other.gameObject.Transform;
//     }
// }
