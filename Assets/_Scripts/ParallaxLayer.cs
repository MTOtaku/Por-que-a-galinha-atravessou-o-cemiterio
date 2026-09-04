using UnityEngine;

// ParallaxLayer.cs
// Adicionamos esta tag de arquitetura. Ela obriga a Unity a garantir que exista 
// um SpriteRenderer no objeto, evitando erros de NullReference na equipe de arte.
[RequireComponent(typeof(SpriteRenderer))]

public class ParallaxLayer : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [Tooltip("Velocidade de movimento da camada. Valores maiores = mais rápido.")]
    [SerializeField] private float speed = 2f;

    private float spriteWidth;
    private Transform cloneTransform;

    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Calculamos a largura real do sprite considerando a escala do objeto
        spriteWidth = spriteRenderer.bounds.size.x;

        // Criamos um clone estrutural na mesma hierarquia para cobrir o fundo
        GameObject clone = Instantiate(gameObject, transform.parent);
        
        // Destruímos o componente de script no clone para evitar uma reação em cadeia de clonagem infinita
        Destroy(clone.GetComponent<ParallaxLayer>());
        
        cloneTransform = clone.transform;
        
        // Posicionamos o clone perfeitamente encostado à direita da imagem original
        cloneTransform.position = transform.position + new Vector3(spriteWidth, 0, 0);
    }

    private void Update()
    {
        float movement = speed * Time.deltaTime;

        // Movemos o original e o clone para a esquerda simultaneamente
        transform.Translate(Vector3.left * movement);
        
        if (cloneTransform != null)
        {
            cloneTransform.Translate(Vector3.left * movement);

            // Se o objeto original saiu completamente da tela pela esquerda, 
            // nós o saltamos para a frente do clone, criando o loop infinito.
            if (transform.position.x <= -spriteWidth)
            {
                transform.position = cloneTransform.position + new Vector3(spriteWidth, 0, 0);
            }

            // Se o clone saiu completamente da tela pela esquerda, 
            // nós o saltamos para a frente do original.
            if (cloneTransform.position.x <= -spriteWidth)
            {
                cloneTransform.position = transform.position + new Vector3(spriteWidth, 0, 0);
            }
        }
    }
}
