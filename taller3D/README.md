# Unity

## Taller de unity 3D

### Game engine

[TODO]

### Principales características de unity

* Existen distintas versiones de unity
  * Personal
  * Plus
  * Pro 
  * Enterprise

* Es multiplataforma
* Es versatil, podemos exportar a diferentes plataformas (nativo/desktop o para dispositivos móviles android y iOS)
* Podemos elegir como lenguaje de desarrollo C# o JS.
* Podemos elegir entre un juego 3D y 2D pero esto se puede cambiar posteriormente.
* Los componentes permiten que nuestros objetos hagan distintas cosas.

### Explorando la interfaz

Las ventanas de básicas de un proyecto de unity son

* Hierarchy. Aquí se muestran todos los objetos que tenemos en una escena
* Proyect panel. Aquí se muestran nuestros *game assets* 
* Scene
* Inspector. Inspecciona las propiedades o compoenetes del objeto seleccionado.

Podemos abrir más *ventanas* en el menú *Window*.

### Objetos

Todo lo que se encuentra en la *escena* es un objeto, la cámara y todo objeto que nosotros incorporemos.

### Componentes

Los componentes le permiten tener nuestros objetos diferentes acciones.

#### RigidBody

Le agrega propiedades de la física a nuestro objeto.

![rigidbody](img/taller3D/rigidbody.png)

#### Script

[TODO]

### Material

[TODO]

### Empecemos

1. **Crear un objeto 3D (un cubo)**

2. **Seleccionar el cubo**

   1. Reiniciar la transformación 

   2. Escalarlo (15,1,100)

      ![scale](img/taller3D/scale.png)

   3. Renombrar objeto como "ground"
   4. Moverlo para que empiece donde la cámara.

3. **Crear otro cubo en 3D**

4. **Crear un *material* en la sección de asset. Se llamará *playerMat* 1**

5. **Asignarle el material al segundo cubo 3D. Se logra arrastrando el material al objeto en la escena.**

   ![material](img/taller3D/material.png)

5. **Agregar el componente *RigidBody* que se encuentra en *Physics***

   1. Probar con el botón de play.
   2. Rotar el cubo para ver las propiedades físicas que le agrega el componente.
   3. Crear otro cubo (Ctrl+D) y ver como interactuan entre ellos.
      1. Mesh Renderer
      2. Box Colliders. Recordar que sirve para cubos no para esferas.s
      3. Mesh (Forma del objeto)
   4. **Es posible ajustar los componentes muestras esta en *modo juego*, pero todo regresará a la normalidad cuando se detenga el juego.**

6. **Ir a la cámara y cambiar el color de fondo.** 

   ![camera](img/taller3D/camera.png)

7. **Guardar la escena.**

### Programando en unity

1. **Seleccionamos un objecto y le agregamos un script de la misma manera que hemos agregado componentes. Lo llamaremos *player movement*** 

   ![script](img/taller3D/script.png)

   1. La función start se ejecuta solo una vez .
   2. La función update se ejecuta infinitamente.

   ```c#
   using UnityEngine;
   
   public class PlayerMovement : MonoBehaviour{
      
       int count = 0;
      // Use this for initialization
       void Start(){
           Debug.Log("Hello,world");
       }
       // User this for calle one per frame
       void Update(){
           Debug.Log("Hello,world");
       }
   }
   ```

2. **Podemos referenciar objetos o componentes del exterior de la siguiente forma**

   ```c#
   using UnityEngine;
   
   public class PlayerMovement : MonoBehaviour{
      	public Rigidbody rb;
       int count = 0;
      // Use this for initialization
       void Start(){
           Debug.Log("Hello,world");
           //rb.useGravity = false;
           rb.AddForce(0,200,500);
       }
       // Use this for calle one per frame
       void Update(){
           Debug.Log("Hello,world");
           count++;
           Debug.Log(count);
       }
   }
   ```

3. **El cubo Debe avanzar uniformemente por que lo que la fuerza se debe aplicar continuamente a lo largo del eje Z.**

   ```c#
   using UnityEngine;
   
   public class PlayerMovement : MonoBehaviour{
      	public Rigidbody rb;
      	public float fowardForce = 2000f;
       void Update(){
           //rb.AddForce(0,200,500);
           //rb.AddForce(0,0,200*Time.deltaTime);
           rb.AddForce(0,0,fowardForce*Time.deltaTime);
       }
   }
   ```

4. **Se observa algo de fricción entre el piso y el cubo rojo lo cual se puede solucionar de 2 formas**

   1. Agregando constraints al rigidbody

   2. Creando un componente *physic material*, aplicarlo al suelo y quitarle la fricción estática y fricción dinámica.

      ![splipery](img/taller3D/splipery.png)

### Inputs

1. **Hay dos maneras de obtener las entradas de usuario, por simplicidad, haremos la más fácil.**

    ```c#
    using UnityEngine;

    public cass PlayerMovement : MonoBehaviour{
        public Rigidbody rb;
        public float fowardForce = 2000f;
        public float sidewaysForce = 500f;
        void FixedUpdate(){
            rb.AddForce(0,0,fowardForce*Time.deltaTime);

            if(Input.GetKey("d")){
                rb.AddForce(sidewaysForce*Time.deltaTime,0,0);
            }
            if(Input.GetKey("a")){
                rb.AddForce(-sidewaysForce*Time.deltaTime,0,0);
            }
        }
    }
    ```

2. **Ahora la cámara debe seguir al jugador, para lo cual se puden hacer 2 cosas.**

   1. Hacer a la cámara un hijo del jugador

      ![camara](img/taller3D/follow.png)

   2. Crear un script

      ```c#
      using UnityEngine;
      
      public class FollowPlayer : MonoBehaviour
      {
          public Transform player;
          public Vector3 offset;
          void Update()
          {
              transform.position = player.position + offset;
          }
      }
      ```

### Colisiones

0. **Se debe crear un obstáculo, que será un cubo. Además al obstáculo se le debe agregar un material. NO olvidar el RIGIDBODY (mass=2) Posteriormente en el paso 3, se le debe agregar un tag llamado 'Obstacle'**

1. **Como se agrega una colisión**

   ```c#
   using UnityEngine;
   
   public class PlayerCollision : MonoBehaviour
   {
       void OnCollisionEnter(){
           Debug.Log("We hit something");
       }
   }
   ```

2. **Crear colisión y detectar objeto.**

   ```c#
   using UnityEngine;
   
   public class PlayerCollision : MonoBehaviour
   {
       void OnCollisionEnter(Collision collisionInfo){
           //Debug.Log(collisionInfo.collider.name);
           if (collisionInfo.collider.name == "Obstacle"){
               Debug.Log("We hit obstacle");
           }
       }
   }
   ```

3. **Refactor con tag**

   ```c#
   using UnityEngine;
   
   public class PlayerCollision : MonoBehaviour
   {
       public PlayerMovement movement;
   
       void OnCollisionEnter(Collision collisionInfo){
           
           if (collisionInfo.collider.tag == "Obstacle"){
               Debug.Log("We hit obstacle");
               movement.enabled = false;
           }
       }
   }
   ```

### Prebfabs

[E06]

### Lighting

### UI

