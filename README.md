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
### General
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
        - "hasSlept" : bool = false

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

//Método asociado al estado de ir a casa
function GoHome() -> void
     //Si ya ha pasado el tiempo necesario para recargar la energía pasa al estado de merodeo
     if "hasSlept" :
         currentState = States.WANDER
         ChangeAction()
         //Resetea el booleano del balckboard para la próxima vez que durmamos
         "hasSlept" = false

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

### Pseudocódigo de componentes usados en los BTs Globales

## Comportamientos individuales
Debido a la diferencia entre algunos comportamientos en ambos animales, como puede ser comer, de esta FSM general podrán heredas dos máquinas de estados distintas (ciervo y lobo) para sobreescribir dichas acciones.

#### Estado EAT CIERVO
1. En primer lugar, al igual que el resto de BTs, se comprobará si se sigue estando en el estado Eat y en caso contrario se interrumpirá la acción.
2. Buscará el arbusto activo y permitido más cercano a su posición. En caso de no encontrarlo, pasará al estado de merodeo.
3. Se dirigirá hacia el arbusto deseado.
4. Activará la animación de comer.
5. Marcará el arbusto como inactivo para el resto de ciervos y se comerá los frutos que haya en él.
   
 #### Estado EAT LOBO
El estado de comer para el lobo es mucho más compleja que la anterior y se rige por subestados propios para identificar cual es su presa, en caso de haberla detectado. Se explicará en más detalle en la sección de **FMS individuales**

1. Al igual que el anterior, se comprobará si se sigue estando en el estado Eat y en caso contrario se interrumpirá la acción.
2. Se dirigirá hacia el objetivo encontrado
3. Le matará y se le comerá, habisando a su presa de que ha muerto para que reproduzca su animación de morir y se borre por completo.

![Ciervo+LoboEat](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/162b98ae-aa66-41f3-a650-78b0ff1ac009)

### FMS individuales
## Ciervo
```
class DeerSM extends StateMachine:

//Referecia a su casa para poder dirigirse a ella en caso de gastarse la energía
 deerHouse : GameObject

//Referecia a su BT que le permite realizar su estado de comer propio y único
 eat : BehaviorExecutor 

function Awake() -> void
   //Se añade a la blackboard todas las variables importantes para el manejo de estados
        "searchedBush" : bool = false
        "bush" : GameObject =  null
        "arrivedAtBush" : bool = false

//Se sobreescribe este método del padre para que, a la hora de morir, también se elimine nuestro BT de comer
function AnimalDied() -> override void
        Destruir el BT eat
        Llamar al AnimalDied() del padre

//Se encarga de desactivar los BTs que no se esten usando. Se sobreescribe este método del padre para que, si el estado actual es comer
//se desactive nuestro BT propio y, en caso contrario, los controlador por el padre
function DeactivateAction(accion) -> override void
        if accion == EAT:
            Desactivamos el BT eat
        else :
            Llamamos al DeactivateAction(accion) del padre

//Método encargado de realizar la acción propia de comer del ciervo
function Eat() -> override void
        Activamos el BT de eat

        //Si estaba buscando un arbusto y no ha encontrado ninguno del que comer pasamos al estado de merodeo
        if searchedBush && "bush" == null:
            currentState = States.WANDER
            Desactivamos el BT de eat
            // Avisamos al padre de que hemos cambiado de estado
            ChangeAction()

        //Si ya he llegado al arbusto deseado entonces paso a el estado de recarga
        else if "arrivedAtBush"

            //Se llama al método  StartEating() del arbusto que me estoy comiendo para avisarle de que me estoy comiendo du fruta.
            //Esto hará que el arbusto pase a estar inactivo para otro ciervo y desaparezcan las frutas
            //Pasado un tiempo (20 segundos), se activará otra vez y reaparecerán las frutas
            StartEating()

            //Se resetean los valores del blackboard para la siguiente vez que comamos
            "searchedBush" : bool = false
            "bush" : GameObject =  null
            "arrivedAtBush" : bool = false

            Desactivamos el BT de eat

            currentState = States.RECHARGE

//Método que devuelve la casa a la que tengo que ir en caso de gastar la energía
function GetHouse() -> override GameObject
    return deerHouse
```

## Lobo
```
class WolfSM extends StateMachine:
    //Subestados propios del estado eat
    // SMELLING : estado que permite merodear hasta encontrar un rastro
    // HUNT : una vez encontrado el rastro, lo persigue. Si el nivel de intensidad de ese rastro supera un
              máximo deseado entonces estoy lo suficientemente cerca de mi presa y voy directamente a por ella
    // EAT :  he cazado a la presa y ahora voy a pasar al estado global de recarga para tener mi hambre al máximo
    // NONE : estado por defecto si no estoy realizando la acción de comer
    enum WolfStates { SMELLING, HUNT, EAT, NONE }

    //Estado actua, por defecto NONE
    m_State : WolfStates = WolfStates.NONE;

    //Referecia a su casa para poder dirigirse a ella en caso de gastarse la energía
    wolfHouse : GameObject 

    //Referencia a los BTs propios necesitados para realizar la acción de comer
    smelling : BehaviorExecutor // BT que me permite merodear
    trace : BehaviorExecutor // BT que me permite dirigirme a mi target, normalmente un rastro de olor
                                pero si estoy lo suficientemente cerca, el animal en si

    //Refernecia a mi area que delimita mi sentido del olfato
    area : SmellArea 

   function Awake() -> void
       //Se añade a la blackboard todas las variables importantes para el manejo de estados
        "target" : GameObject =  null
        "hasEat" : bool = false

   //Se sobreescribe este método del padre para que, a la hora de morir, también se elimine nuestro BT de comer
   function AnimalDied() -> override void
        Destruir los BTs de smelling y trace
        Llamar al AnimalDied() del padre

   function IsTracking() -> bool
        return m_State == WolfStates.HUNT

   //Avisa si tu target al que te estas dirigiendo es un olor o ya el animal en sí
   function CheckIfHunting() -> bool
        return target es un olor o el target genera rastro

    //Verifica si una acción es el estado que se esta ejecutando ahora o no
    functionCheckActiveAction(action) -> override bool
        //Si estoy realizando la acción de comer, compruebo mis subestados propio
        if m_State != WolfStates.NONE :
            return action == m_State
        //Si acabo de cazar a mi presa
        else if "hasEat" :
            Reinicio el balckboard para la siguiente y desactivo mis BTs propios
            return false
        //Sino llamo al del padre
        else return CheckActiveAction(action) del padre

    //Método que permite activar y desactivar los BTs dependiendo del subestado
    function SwitchAction() -> void
        if m_State == WolfStates.SMELLING :
            activo el BT de smelling
        else :
            activo el BT de trace

   //Se encarga de desactivar los BTs que no se esten usando. Se sobreescribe este método del padre para que, si el estado actual es comer
   //se desactive nuestro BT propio y, en caso contrario, los controlador por el padre
    function DeactivateAction(action) -> override void
        if m_State != WolfStates.NONE
            if action == WolfStates.SMELLING :
                Desactivo el BT smelling
            else :
                Desactivo el BT trace

        else Llamo al DeactivateAction(action) del padre

   function Update() -> void
        //Si estoy en el estado de comer
        if m_State != WolfStates.NONE
            //Compruebo si ha entrado algo en mi rango de detección olfativo
            WolfSmelling()
            //Si he detectado, empiezo a cazar
            if m_State == WolfStates.HUNT :
                WolfHunt()
         else llamo al Update() del padre


    function WolfSmelling() -> void
        if area.HasDetectedSmell()
            m_State = WolfStates.HUNT
        else
            m_State = WolfStates.SMELLING
            SwitchAction()


    function GetTarget() -> GameObject
        return "target" //mi target del blackboard

    //He detectado en mi area olfativa, asigno el target a lo que haya detectado para luego ir a por él
    function WolfHunt() -> void
        target : GameObject = area.GetScent()
        if target != null
            maxIntensity : float = 0.7f //por defecto
            if animal generador del olor == ciervo :
                maxIntensity = 0.9f; //Ya que el olor del ciervo es mayor

            // Si estoy lo suficientemente cerca me guardo el animal
            if intesidad del olor detectado >= maxIntensity && animal original no ha muerto :
               "target" =  area.GetPrey()
            // Sino me guardo el olor
            else :
                "target" =  area.GetScent()
            SwitchAction()

    //Método al que se llama cuando he cogido a mi presa
    function WolfHasPrey() -> void
        Aviso al LifeController de mi presa de que ha muerto, llamando a su método Die()
        m_State = WolfStates.NONE //subestado por defecto
        hasEat" = true //variable del blackboard que indica si acabo de cazar
        currentState = States.RECHARGE //estado global

    //Estado sobreescrito con comportamiento único del lobo
    function Eat() ->  override void
        //Si no estaba ya en marcha, comienzo merodeando para detectar un rastro
        if m_State == WolfStates.NONE
            m_State = WolfStates.SMELLING;
            SwitchAction()

    //Método que devuelve la casa a la que tengo que ir en caso de gastar la energía
    function GetHouse() -> override GameObject
        return wolfHouse
```
### Pseudocódigo de componentes usados en los BTs individuales de comer

#### Class CheckFinised
```
class CheckFinished extends GOCondition

    // parametro de entrada 
    action : int

    // Referencia a la stateMachine
    StateMachine stateMachine = null

    // Booleano que indica si la accion ha terminado
    bool finished = false

    // Metodo que comprueba si la accion ha terminado
    function Check -> bool
        if (tag del gameobject == "Wolf")
            stateMachine = stateMachine de los lobos del gameObject
            finished = !stateMachine.(metodo que devuelve si la action es igual a la accion que se esta ejecutando)
        else if (tag del gameobject == "Stag")
            stateMachine = stateMachine de los ciervos del gameObject
            finished = !stateMachine.(metodo que devuelve si la action es igual a la accion que se esta ejecutando)
        else return false
        if (finished)
            stateMachine.(metodo que desactiva la action)
        return finished
```

#### class StartAnimation

```
class StartAnimation extends GOAction

    // Nombre del clip de animacion que debe reproducirse
    animationClip : string

    // Metodo que le envia al animator del gameobject el clip que tiene que reproducir
    function OnStart -> void
        animator :Animator -> componente Animator del gameObject
        animator.Play(animationClip)

```

#### class Sleep

```
class Sleep extends GOAction
    
    // Referencia a la guarida del animal
    house : GameObject
    // Referencia a la tateMachine del gameobject
    sM : StateMachine

    // Metodo que desactiva todo el movimiento del animaly lo mete a la casa
    public override void OnStart
        desactiva el navMesh del gameobject
        setea su rigid body a kinemático
        detiene la bajada de energia del gameObject
        if (sM != null)
            gameObject position = house position
```

#### class WakeUp

```
class WakeUp extends GOAction
    
    // Referencia a la guarida del animal
    public GameObject house
    // Referencia a la stateMAchine del gameobject
    private StateMachine sM

    // Funcion que devuelve al animal al terreno y le sube la energia
    function OnStart -> void
        reactiva el navMesh del gameObject
        setea el rigidbody del gameobject como no kinematico
        vuelve a activar la bajada de la energia del gameobject
        if (sM != null)
            gameObject position = house position
            setea un bool de la blackboar que indica si ha dormido a true
        gameObject.RestoreMaxEnergy()
```

#### class FindClosestBush

```
class FindClosestBush extends GOAction
    
    // Referencia a la lista de arbustos
    list : List<GameObject>
    
    // Parametro de salida (el arbusto escogido)
    foundGameObject : GameObject -> null
    
    // Referencia a la StateMachine del ciervo
    dSM : DeerSM
    
    // Funcion que encuentra el arbusto disponible mas cercano y lo marca como ocupado
    function OnStart -> void
        float dist = float.MaxValue
        foreach(GameObject go in list)
            bush : BushBehaviour -> go.componente BushBehaviour
            if (bush.GetIsAvailable())
                newdist : float -> distancia entre gameObject position y bush position
                if(newdist < dist)
                    dist = newdist
                    foundGameObject = go
        if(foundGameObject != null) 
            foundGameObject.marca el arbusto como ocupado

        setea booleanos de la blackboard que indican que ha encontrado un arbusto 
```

#### class EatBush

```
class EatBush extends GOAction

    // parametro de entrada que representa el arbusto 
    target : GameObject 
    
    // referencia a la maquina de estados del ciervo
    dSM : DeerSM

    function OnStart -> void
        setea un booleano de la blacboard que indica que se ha comido el arbusto
        gameObject LookAt(target);
```

#### class Eating

```
class Eating extends GOAction

    public override void OnStart()
    
         eatComponent : WolfSM -> componente WolfSM del gameobject
        if (eatComponent.CheckIfHunting())
            eatComponent.WolfHasPrey()
            animator : Animator -> componente animator del gameobject
            animator.Play("Eat")
```

## Sentido del olfato
El sentido del olfato se rige por tres clases principales : **GenerateSmell**, **Scent** y **SmellArea**.

Ambos, ciervo y conejo, van dejando continuamente un rastro de olor (_Scent_) representado con distintos colores y escalas. En nuestro caso, la escala indica la intesidad del olor, la cual va disminuyendo con el tiempo hasta desaparecer por completo.


### Generate Smell
```
 timeToSpawn : float
 scent : GameObject
 elapsedTime : float
 parent: Transform

 function Update() -> void
     if elapsedTime >= timeToSpawn
         elapsedTime = 0

         Instancia un olor con el método propio de Unity Istanciate()

         Le pone al olor como padre el parent guardado para que cada rastro instanciado
         de un mismo animal este encapsulado en un objeto único en el editor

         Guarda la referencia al animal que ha dejado el rasto mediante el método SetOriginator()

     else elapsedTime += Time.deltaTime
```
![TiposDeRastros](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/4f9a061a-f205-4e7a-94bd-421f52a9048f)

La clase Scent hereda de IComparable para poder comparar dos olores y así poder meterlo a la lista de olores percibidos cumpliendo
un orden de prioridad.
### Scent
```
class Scent extends MonoBehaviour, IComparable<Scent>:
    timeToLive : float
    decreaseFactor : float
    _renderer : Renderer
    elapsedTime : float
    alive : bool = true
    originator : GameObject
    typeScent : string

    SetOriginator(GameObject or) -> void
        originator = or

    GetOriginator() -> GameObject
        return originator

    GetIntensity() -> float
        return escala del objeto

    GetTypeScent() -> string
        return typeScent

    Update() -> void
        if alive:
            if escala <= 0 elapsedTime >= timeToLive
                Destruyo el objeto
                alive = false
            else
                elapsedTime += Time.deltaTime
                //Disminuir escala con el tiempo
                escala -= Vector3(decreaseFactor / timeToLive, 0, decreaseFactor / timeToLive) * tiempo
                //Aclarar el color con el tiempo
                color.g += 0.1f * tiempo

    //Método que compara dos olores dependiendo de su tipo y su intensidad
    // Un olor de tipo Ciervo es más prioritario que uno de un Conejo pero, en caso de ser del mismo tipo, a mayor intensidad mayor preferencia
    CompareTo(Scent other) -> int
        //Devolver 1 significa que es más prioritario y -1 menos prioritario
        if other == null: return 1

        isStagThis : bool = typeScent == "Stag"
        isStagOther : bool = other.GetTypeScent() == "Stag"

        if isStagThis && !isStagOther: return -1
        if !isStagThis && isStagOther : return 1

        return other.GetIntensity().CompareTo(this.GetIntensity())
```

El lobo consta de un área de detección para el olfato por la que, en caso de entrar por ella, registrará los olores nuevos y los añadirá en una lista según su preferencia. Si entran dos rastros del mismo animal, solo se quedará con la de mayor intensidad. La lista estará ordenda según el tipo de animal y la intensidad, 
favoreciendo a aquellos rastros originados por los ciervos y con mayor intensidad.

```
class SmellArea extends MonoBehaviour :
    listScent : List<Scent>

    function HasDetectedSmell() -> bool
        return tamaño de la listScent > 0

    function GetPrey() -> GameObject
        if tamaño de la listScent > 0 :
           return animal creador del rastro más potente, el primero en la lista
        else :
           return null
    
    GetScent() -> GameObject
       if tamaño de la listScent > 0 :
           return rastro más potente, el primero en la lista
        else :
           return null
    
    OnTriggerEnter(Collider other) -> void
        if other es de tipo Scent :
            //Comprueba si todos los objetod de la lista siguen existiendo, evita errores entre ticks
            CheckIfStillSmells()

            //Compruba si en la lista ya hay un rastro proveniente del mismo animal
            if (!CheckIfAlreadyInList(otherScent)) listScent.Add(otherScent);

            //Ordena la lista
            listScent.Sort(); 


    OnTriggerExit(Collider other) -> void
        if other es de tipo Scent :
            //Comprueba si todos los objetod de la lista siguen existiendo, evita errores entre ticks
            CheckIfStillSmells()
            //Saca de la lista el olor en caso de estar
            listScent.Remove(otherScent);

    Update() -> void
        if tamaño de la listScent > 0 :
            Comprueba si el rastro más potente, el primero de la lista, sigue activo y, en caso de no estarlo, lo elimina
            if listScent[0] == null : 
                listScent.Remove(listScent[0])

    CheckIfAlreadyInList(Scent newScent) -> bool
        foreach Scent scent in listScent :
            //Si ya hay un rastro del mismo animal que ha creado el nuevo
            if scent.GetOriginator() == newScent.GetOriginator()
                //Compara con cual nos interesa quedarnos, lo añade y elimina el otro
                if scent.CompareTo(newScent) > 0
                    listScent.Add(newScent);
                    listScent.Remove(scent);
                return true

        return false

    CheckIfStillSmells() -> void
        Elimina todos los rastros que ya hayan desaparecido de la lista
```

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

https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/675277a2-a51e-417b-a43a-db05679a8434

### Prueba B: Gestión de recursos
Comprobaremos que los ciervos se mueven correctamente entre arbustos, y en caso de que su arbusto objetivo haya sido ocupado, se darán cuenta y buscarán otro del que alimentarse. En caso de encontrar un arbusto, irá a por el más cercano.

https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/e4e3f536-c077-4477-a330-dbd27fbdeef0


### Prueba C: Hambre VS Sueño
Debido a que el hambre baja más rapido que la energía, los animales priorizarán el hambre en caso de agotarse ambos.

https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/b21ba4d9-2800-46de-aa6d-573a14be0970

### Prueba D: Preferencia de olfato del lobo
El lobo deberá escoger a sus presas dependiendo de su prioridad, ciervos antes que conejos y rastros de mayor intensidad antes que menos. Si ha dejado de detectar el rastro, volvera a merodear hasta detectar otro nuevo.

https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/25b3609e-26e9-47eb-b19c-fb0409d1245b
https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/bb17306b-415c-43d2-bce2-98d1a1f06908



## Producción
### Reparto de tareas
Andrea Vega Saugar se ha encargado de la generación aleatoria de Perlin, mientras que Claudia Zarzuela Amor ha realizado el olfato del lobo. El resto de tareas necesarias para crear el proyecto se han realizado conjuntamente y el esfuerzo ha sido repartido entre los autores.

| Estado  |  Tarea  |  Fecha  |  
|:-:|:--|:-:|
| ✔ | Read me |16-05-2024|
| ✔ | Creación incial del proyecto |8-04-2024|
| ✔ | Botones de HUD (cambio entre cámaras) |13-04-2024|
| ✔ | Creación Main Menu |23-04-2024|
| ✔ | Spawn de conejos (con máximo total) |23-04-2024|
| ✔ | Olfato |27-04-2024|
| ✔ | Perlin |26-04-2024|
| ✔ | BTs con todos los comportamientos |28-05-2024|
| ✔ | Maquina de estados general |30-05-2024|
| ✔ | Maquina de estados ciervo |30-05-2024|
| ✔ | Maquina de estados lobo |30-05-2024|
| ✔ | Readme actualizado |31-05-2024|
## Licencia
Claudia Zarzuela, Andrea Vega Saugar, autores de la documentación, código y recursos de este trabajo, no concedemos permiso permanente a los profesores de la Facultad de Informática de la Universidad Complutense de Madrid para utilizar nuestro material, con sus comentarios y evaluaciones, con fines educativos o de investigación; ya sea para obtener datos agregados de forma anónima como para utilizarlo total o parcialmente reconociendo expresamente nuestra autoría.

Una vez superada con éxito la asignatura se prevee publicar todo en abierto (la documentación con licencia Creative Commons Attribution 4.0 International (CC BY 4.0) y el código con licencia GNU Lesser General Public License 3.0).

## Referencias

- *Generación aleatoria de Perlin* (https://riull.ull.es/xmlui/bitstream/handle/915/1395/Generacion+aleatoria+de+terrenos+3D+con+Unity.pdf;jsessionid=8C0D709D170ADE765FE348DCC336A62B?sequence=1)
- *AI for Games*, Ian Millington.
- *Edirlei Lima* (https://edirlei.com/aulas/game-ai-2020/GAME_AI_Lecture_07_Steering_Behaviours_2020.html)
