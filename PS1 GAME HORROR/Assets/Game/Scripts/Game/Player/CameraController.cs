using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform player;

        [Header("Configuração de Sensibilidade")]
        public float sensibilidadeX = 200f;
        public float sensibilidadeY = 200f;

        [Header("Limite de Rotação")]
        [SerializeField] Vector2 limiteRotacaoX = new Vector2(-90f, 90f); // Limite para cima/baixo

        private float rotacaoX = 0f; // Armazena a rotação atual no eixo X

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
            Cursor.visible = false; // Esconde o cursor
        }

        void Update()
        {
            // Captura a movimentação do mouse
            float mouseX = Input.GetAxis("Mouse X") * sensibilidadeX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeY * Time.deltaTime;

            // Aplica a rotação no eixo X (vertical), respeitando os limites
            rotacaoX -= mouseY;
            rotacaoX = Mathf.Clamp(rotacaoX, limiteRotacaoX.x, limiteRotacaoX.y);

            // Aplica a rotação na câmera (vertical)
            transform.parent.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);

            // Rotaciona o personagem no eixo Y (horizontal)
            player.Rotate(Vector3.up * mouseX);
        }
    }
}