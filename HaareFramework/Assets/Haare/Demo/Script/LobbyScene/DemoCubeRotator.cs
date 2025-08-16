using Haare.Client.Routine;
using UnityEngine;

namespace Demo.LobbyScene
{
    public class DemoCubeRotator : MonoRoutine
    {
        public float rotationSpeed = 50f;

        // 매 프레임마다 호출되는 Update 함수입니다.
        protected override void UpdateProcess()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Destroy(gameObject);
            }
        }
        
        
        
    }
}