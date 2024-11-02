using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Link.HouseStack
{
    public class Control : MonoBehaviour
    {
        private static Control instance;    
        public static Control Instance
        {
            get {
                if (instance == null){
                    instance = FindObjectOfType<Control>();
                }
                return instance; 
            }
        }

        public enum State { Building, Droping, Stoping }
        private State state = State.Building;    

        [SerializeField] House housePrefab;
        [SerializeField] House houseLast;
        [SerializeField] House housePreLast;
        [SerializeField] Transform crane;
        [SerializeField] Transform housePoint;
        [SerializeField] float moveSpeed = 1;
        [SerializeField] float space = 3.5f;

        private void Update()
        {
            if (state == State.Building && Input.GetMouseButtonDown(0))
            {
                SpawnHouse(housePoint.position, Quaternion.identity);
                DropBuilding();
            }
            if(state == State.Stoping && housePreLast != null && transform.position.y < housePreLast.transform.position.y + space) 
            {
                transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            }
        }

        private int[] craneAngle = new[] { -30, -20, -10, 10, 20, 30 };

        private void StartBuilding()
        {
            state = State.Building;
            housePoint.gameObject.SetActive(true);
            crane.eulerAngles = Vector3.forward * craneAngle[Random.Range(0, craneAngle.Length)];
        }
        private void DropBuilding()
        {
            state = State.Droping;
            housePoint.gameObject.SetActive(false);
            Invoke(nameof(StopBuilding), 1f);
        }
        private void StopBuilding()
        {
            state = State.Stoping;
            Invoke(nameof(StartBuilding), 1.5f);
        }

        private void SpawnHouse(Vector2 pos, Quaternion rot)
        {
            housePreLast = houseLast;
            houseLast = Instantiate(housePrefab, pos, rot);
        }

        public void CheckHouseBuilding(House house)
        {
            if (house == houseLast || house == housePreLast)
            {
                Debug.Log("Lose");
            }
        }
    }
}
