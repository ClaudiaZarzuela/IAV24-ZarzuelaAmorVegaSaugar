# IAV-Propuesta final: WILDLIFE SIMULATOR

## Autores
- Claudia Zarzuela Amor: Claudia Zarzuela - https://github.com/ClaudiaZarzuela
- Andrea Vega Saugar: AndreaVegaSaugar - https://github.com/AndreaVegaSaugar
 
## Propuesta
Este documento refleja la propuesta de proyecto final para la asignatura de Inteligencia Artificial para Videojuegos del Grado en Desarrollo de Videojuegos de la UCM.
![portada](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/c11f77eb-25d6-45cf-862f-99abc7fde9f9)


Nos hemos inspirado en el comportamiento animal salvaje según su puesto en la cadena alimenticia y en el uso de sus diferentes sentidos para sobrevivir.

Wildlife Simulator será un simulador en el que distintos tipos de animales convivirán juntos, manteniendo sus niveles de energía y hambre para seguir con vida. Nos centraremos específicamente en la **Generacion procedural por Perlin** y en el desarrollo del agudo **Sentido del olfato de los carnívoros**.

- Los herbívoros se alimentarán de plantas distribuidas por el escenario mediante el ruido de perlin y cuyo número es personalizable en cada partida.
- El carnívoro merodeará por el terreno y, cuando comience a tener hambre, tratará de cazar algún animal usando su agudo sentido del olfato, teniendo en cuenta su preferencia de Ciervos por encima de conejos y la intensidad de los rastros que encuentra.

Para dar vida a la simulación contaremos con tres tipos de animales. Para representar a los herbívoros usaremos ciervos, a los carnívoros un lobo y añadiremos conejos para equilibrar el hábitat y evitar que el lobo acabe con los ciervos rápidamente. Estos conejos simplemente se usarán como cebo y se encargarán exclusivamente de merodear. En caso de oler a ambos animales, el lobo preferirá cazar a los ciervos. 

Cada animal tendrá que tener cuidado de no dejar bajar sus niveles demasiado, ya que de agotarse, morirán. Ambos comenzarán merodeando tranquilamente por el bosque, bajando constantemente poco a poco su nivel de energía y rápidamente su nivel de hambre. 

![Animales](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/a18be436-1821-4cbc-96f9-c168641cff51)

## Terreno
El hábitat consta de distintas zonas valiosas:
- A los extremos encontramos los hogares de ambos animales, la derecha siendo de los carnívoros y la izquierda de herbívoros. En estas áreas, los animales podrán resguardarse con seguridad de cualquier animal ya que solo los de su misma especie pueden entrar en dicho lugar. Aquí los animales descansarán hasta que su barra de energía esté completa. Una vez recuperados, continuarán con sus rutinas normales.

- Esparcidos por el terreno, podremos encontrar arbustos con frutos para alimentar a los herbívoros. Estos arbustos se posicionarán de manera pseudoaleatoria utilizando **Perlin Noise**. Después de que un ciervo se pare a comer, deberán transcurrir unos segundos (20) antes de que salgan nuevos frutos.

- Al principio de la partida aparecerán en posiciones aleatorias del terreno unos conejos. Estos sólo tienen un comportamiento de merodeo y sirven para evitar que el lobo acabe con los ciervos de inmediato. 

![Diagrama terreno](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/49333fbf-4258-43d3-bc89-e521ca535d88)

## Generación procedural
### Perlin
> [!NOTE]
> El ruido de Perlin es un algoritmo desarrollado por Ken Perlin en 1983 usado para generación procedural. Hay varios tipos de algoritmos especializados pero, tras informarnos del resto de posibilidades, decidimos usar Perlin debido a su aspecto orgánico conseguido mediante ruido basado en gradientes. Esto es ideal para crear paisajes naturales ya que, en la naturaleza, las transiciones entre diferentes características del terreno como las densidades de vegetación suelen ser suaves.

```
class InstanciateBushes extends MonoBehavior:

//Definimos el área donde queremos generar los arbustos
scaleX : float = ancho del objeto rectangular padre
scaleZ : float = alto del objeto rectangular padre

//Guardamos el número de arbustos generados por Perlin
instanciatedBushes : int

// Número máximo de arbustos que queremos generar por Perlin
maxNumBushes : int = 50

// Número total de arbustos deseados
bushesNum : int = 10

//Doble bucle for para recorrer todo el área y generar arbustos
//Se generar más arbustos de los deseados para lugeo borrar el exceso y darle mucha más aleatoriedad
desde x : float = 0 hasta  x = scaleX
   desde z : float = 0 hasta z = scaleZ

      //Se calcula el valor del ruido de Perlin para las coordenadas actuales (x, z) usando el método propio de Unity  Mathf.PerlinNoise()
      // e introduciendo una variación aleatoria adicional (0.5f, 1.5f) para evitar patrones repetitivos.
      noiseValue : float -> Mathf.PerlinNoise()

      //Solo se instancian arbustos si el valor del ruido de Perlin supera 0.75 y si el número de arbustos instanciados es menor al máximo permitido
      if noiseValue > 0.75 && instanciatedBushes < maxNumBushes :
          Instanciamos un arvusto y lo guardamos
          instanciatedBushes++

//Bucle while para borrar el exceso de arbustos, de manera aleatoria para darle una forma más orgánica
 mientras (instanciatedBushes > bushesNum)
    Elegimos un arbusto generado de forma aleatoria y lo borramos

```

### Demostración visual
https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/945d60fc-976d-40ba-8dd5-958d33381220

## Estructura de los comportamientos
Para el comportamiento de los animales se optó por una **FSM (Finite State Machine)** general, utilizando Behavior Trees como estados, permitiendo comprender las acciones globales para todos cómo:
| Estado | Función |
|:-:|:--|
| **WANDER** | Estado controlado por un Behavoir Tree en el que el animal merodea por el hábitat, escogiendo y dirigiéndose a puntos aleatorios. Se trata del estado predeterminado pero en caso de detectar que el hambre o energía se encuentran por debajo de lo especificado, cambiara de estado.|
| **GO_HOME** | Estado controlado por un Behavoir Tree en el que el animal escoge la casa a la que debe ir y se dirige a ella. Una vez dentro, espera unos segundos mientras recarga por completo la energía. Una vez llena por completo, el animal saldrá de su hogar y comenzará a merodear de nuevo.|
| **RECHARGE** | Tras realizar la acción de comer, se encarga de esperar unos segundos y subirle el hambre al máximo antes de volver a meordear por la zona.  |
| **NONE** | Cuando un animal muere, se necesita destruir todos los BTs del resto de estados para que no se ejecuten mientras tanto. Por tanto, este estado permite manejar los errores que puedan ocasionarse por componentes que esten activos cuando no deben. |

```
class StateMachine extends MonoBehavior:

//Estados generales de los animlaes
 enum States = { WANDER, GO_HOME, RECHARGE, EAT, NONE }

//Referencia al estado actual, el cual comenzará siendo merodeo
currentState : States = States.WANDER

//Pizzarra global par aguardar variables de los comportamnientos
blackboard : Blackboard

//Referencia al controlador de energía del animal, el cual actualiza la energía y el hambre 
energyController : EnergyController

//Tiempo de espera a la hora de recargar el hambre o la energía
elapsedTime : float = 0
rechargeTime : float = 2

//Referencia a los BT de cada estado
behaviorExecutorList : List<BehaviorExecutor>

//Se llama cuando un animal se muere
function AnimalDied() -> void
     Destruir cada acción realizada por un BT
     currentState = States.NONE

//Preguntar si una accion es la que se está realizando actualmente
function CheckActiveAction(accion) -> bool
     return action == currentState

 function ChangeAction() -> void
     Activar el BT asociado al currentState

function DeactivateAction(accion) -> void
     Desactivar el BT asociado a la accion pasada como parámetro

function Awake() -> void
     Inicializar variables y blackboard
     Añadir variables globales al blackboard como:
        - "minHunger" : float =  75.0f
        - "minEnergy" : float = 25.0f

//Método asociado al estado de merodeo
function Wander() -> void
     if (hambre <= "minHunger": currentState = States.EAT
     else if energía <= "minEnergy" : currentState = States.GO_HOME;
     else : currentState = States.WANDER

//Método asociado al estado de recarga
function Recharge() -> void 
     if elapsedTime >= rechargeTime :
         Poner en marcha el energyController para que comience a actualizar el hambre y la energía dr nuevo
     	 currentState = States.WANDER;
     else :
         Sumar elapsedTime
         Restaurar el hambre, ponerlo al máximo
         Parar el energyController para que no actualice el hambre y la energía mientras tanto

//Método asociado al estado de ir a casa, sobreescrito por los hijos
function GoHome() -> virtual void

//Método asociado al estado de comer, sobreescrito por los hijos
function Eat() -> virtual void

//Método que devuelve la casa a la que hay que ir en caso de tener sueño, sobreescrito por los hijos
function GetHouse() -> virtual GameObject

function Update() -> void
    //Cambio entre estados con un switch
     switch (currentState)
         case States.WANDER: Wander()
         case States.RECHARGE Recharge()
         case States.EAT: Eat()
         case States.GO_HOME: GoHome()
```
### Behavior Trees Globales
![Wander+GoHomeBTs](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/e5488fdf-2d88-4af9-b5d0-801c30372fda)

### Comportamientos individuales
Debido a la diferencia entre algunos comportamientos en ambos animales, como puede ser comer, de esta FSM general podrán heredas dos máquinas de estados distintas (ciervo y lobo) para sobreescribir dichas acciones.

#### Estado EAT CIERVO
En primer lugar harán la comprobación más prioritaria, siendo esta la de su sentido del oído, para comprobar si están siendo acechados por algún lobo y en ese caso iniciar la huida.

A continuación comprobarán sus niveles de hambre, en caso de necesitarlo buscarán el arbusto más cercano, y si está disponible comerán de él. Si por el contrario el arbusto está ocupado, o siguen teniendo hambre, repetirán esa búsqueda con el siguiente arbusto más cercano hasta quedar satisfechos.

Una vez comprobada el hambre, comprobarán su energía, que en el caso de estar baja, les obligará a volver a su "guarida" para reponerla.

En caso de que no se cumpla ninguna de las condiciones anteriores (ha detectado a un enemigo, tiene hambre o tiene sueño), el animal se dedicará a merodear por el escenario

 #### Estado EAT LOBO
 En el caso de los lobos, como no están en peligro de ser perseguidos su primera comprobación será su olfato. Esto es así porque en caso de chequear primero el hambre y no encontrar ningún rastro nunca saldría de ese bucle.

En caso de haber encontrado un rastro y tener hambre, seguirán el rastro de la presa hasta estar lo suficientemente cerca como para verla, momento en el cual comenzarán a perseguirla hasta cazarla o perder su rastro.

En segundo lugar comprobarán su energía, lo cual funcionará igual que en el caso de los ciervos, en caso de estar baja irán a la "guarida" a descansar.

Y por último, también símil a los ciervos, si todas estas comprobaciones fallan, se limitarán a merodear por el escenario.

![Ciervo+LoboEat](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/162b98ae-aa66-41f3-a650-78b0ff1ac009)

### FMS individuales

## Sentido del olfato

## Input, cámaras y HUD
### Cámara principal 
Durante la partida habrá distintos tipos de cámaras. La principal mostrará un plano cenital del escenario, acompañado de seis botones en la parte superior de la pantalla. Estos botones representan un animal del juego y, al hacer click en ellos, aparecerá un indicativo en la parte inferior de dicho animal para hubicarle mejor. Esto esta pensado para facilitar futuras pruebas y un mejor entendimiento sobre el panoráma general y acciones individuales. En la parte central superior, esta situado otro llamado **P.O.V** que permitirá cambiar a una cámara picáda, en tercera persona, de ese animal en concreto. A la derecha aparecen dos barras, una de energía y otra de hambre, que irán variando según sus necesidades.  

![CameraPrinicpalExplicado](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/2dbddc04-2cd7-4dce-b3b3-f9ddc51bcb05)

Ya que se trata de una simulación, el único input que podrá realizar el jugador será hacer click sobre los botones en pantalla.

### Cámaras individuales
Una vez cambiado a la cámara individual de un animal, aparecerá arriba a la izquierda su información personal (energía y hambre). En la parte inferior derecha se encontrará un boton **Main Camera** con el cual podremos volver a la vista cenital.
![CamarasAnimales](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/6492b4fa-e7ea-4720-8238-1263f32488e7)

### Main Menú
![MainMenu](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/5ff68812-3026-415a-b1c0-ac8de509b4e3)
Al comienzo de la simulación se podrá especificár cuantos arbustos y conejos totales se quieren en la partida. En el caso de que todos los herbívoros mueran, se volverá al menú principal.

## Pruebas y métricas
Para comprobar el correcto funcionamiento de la aplicación, la someteremos a diversas pruebas:

### Prueba A: Generación del entorno
Para comprobar que la generación del escenario es adecuada, iniciaremos la aplicación X número de veces, cambiando los parámetros de generación de arbustos y conejos cada vez, y comprobando que el terreno resultante es distinto en cada ejecución.

- [Vídeo de la prueba A: Próximamente]()

### Prueba B: Sentidos
Para comprobar la eficacia de los sentidos de los lobos y de los ciervos, cambiaremos sus rangos de percepción y comprobaremos que los lobos siguen el rastro de olor de un ciervo, y que los ciervos huyen al oír a un lobo.

- [Vídeo de la prueba B: Próximamente]()

### Prueba C: Gestión de recursos
Comprobaremos que los ciervos se mueven correctamente entre arbustos, y en caso de que su arbusto objetivo haya sido ocupado, se darán cuenta y buscarán otro del que alimentarse.

- [Vídeo de la prueba C: Próximamente]()

### Prueba D: Behaviour Bricks
Para confirmar el correcto funcionamiento de los comportamientos implementados mediante behaviour bricks, cambiaremos los nieveles de hambre y energía de ciervos y de lobos y comprobaremos que sus acciones se corresponden con lo esperado: volver a la guarida cuando tienen poca energía, buscar comida cuando tienen hambre, merodear cuando no tienen necesidades, etc.

- [Vídeo de la prueba D: Próximamente]()

### Prueba E: NavMesh
Nos aseguraremos de que cada especie se mueva por las zonas que hayamos determinado mediante el uso de NavMesh, y para confirmar que funciona, guiaremos a cada especie a los bordes de su área correspondiente y comprobaremos que no se salen de su perímetro.

- [Vídeo de la prueba E: Próximamente]()

## Producción
### Reparto de tareas
Las tareas se han realizado y el esfuerzo ha sido repartido entre los autores.

| Estado  |  Tarea  |  Fecha  |  
|:-:|:--|:-:|
| ✔ | Read me |16-05-2024|
| ✔ | Creación incial del proyecto |8-04-2024|
| ✔ | Botones de HUD (cambio entre cámaras) |13-04-2024|
| ✔ | Creación Main Menu |23-04-2024|
| ✔ | Spawn de conejos (con máximo total) |23-04-2024|

## Licencia
Claudia Zarzuela, Andrea Vega Saugar, autores de la documentación, código y recursos de este trabajo, no concedemos permiso permanente a los profesores de la Facultad de Informática de la Universidad Complutense de Madrid para utilizar nuestro material, con sus comentarios y evaluaciones, con fines educativos o de investigación; ya sea para obtener datos agregados de forma anónima como para utilizarlo total o parcialmente reconociendo expresamente nuestra autoría.

Una vez superada con éxito la asignatura se prevee publicar todo en abierto (la documentación con licencia Creative Commons Attribution 4.0 International (CC BY 4.0) y el código con licencia GNU Lesser General Public License 3.0).

## Referencias

- *Generación aleatoria de Perlin* (https://riull.ull.es/xmlui/bitstream/handle/915/1395/Generacion+aleatoria+de+terrenos+3D+con+Unity.pdf;jsessionid=8C0D709D170ADE765FE348DCC336A62B?sequence=1)
- *AI for Games*, Ian Millington.
- *Edirlei Lima* (https://edirlei.com/aulas/game-ai-2020/GAME_AI_Lecture_07_Steering_Behaviours_2020.html)
