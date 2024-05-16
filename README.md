# IAV-Propuesta final: WILDLIFE SIMULATOR

## Autores
- Claudia Zarzuela Amor: Claudia Zarzuela - https://github.com/ClaudiaZarzuela
- Andrea Vega Saugar: AndreaVegaSaugar - https://github.com/AndreaVegaSaugar

## Propuesta
Este documento refleja la propuesta de proyecto final para la asignatura de Inteligencia Artificial para Videojuegos del Grado en Desarrollo de Videojuegos de la UCM.
![portada](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/c11f77eb-25d6-45cf-862f-99abc7fde9f9)


Nos hemos inspirado en el comportamiento animal salvaje según su puesto en la cadena alimenticia y en el uso de sus diferentes sentidos para sobrevivir.

Wildlife Simulator será un simulador en el que distintos tipos de animales convivirán juntos, manteniendo sus niveles de energía y hambre para seguir con vida. Nos centraremos específicamente en el comportamiento carnívoro y herbívoro:

- Los herbívoros se alimentarán de plantas y usarán su avanzado sentido del oído para tratar de detectar a posibles depredadores, huyendo a su nido cuando se sientan atacados o necesiten descansar. 
- Los carnívoros merodearán por el terreno y, cuando comiencen a tener hambre, tratarán de cazar algún animal usando su agudo sentido del olfato, teniendo constantemente en cuenta su nivel de energía, el cual bajará al correr y, en caso de agotarse, volverán a su hogar.

Para dar vida a la simulación contaremos con tres tipos de animales. Para representar a los herbívoros usaremos ciervos, a los carnívoros lobos y añadiremos conejos para equilibrar el hábitat y evitar que los lobos acaben con los ciervos rápidamente. Estos conejos simplemente se usarán como cebo y se encargarán exclusivamente de merodear. En caso de oler a ambos animales, los lobos preferirán cazar a los ciervos. 

Cada animal tendrá que tener cuidado de no dejar bajar sus niveles demasiado, ya que éstos tendrán un efecto en sus habilidades. Ambos comenzarán merodeando tranquilamente por el bosque, bajando constantemente  poco a poco su nivel de energía y rápidamente su nivel de hambre. 

![Animales](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/a18be436-1821-4cbc-96f9-c168641cff51)

## Mecánicas de juego
### Terreno
El hábitat consta de distintas zonas valiosas:
- A los extremos encontramos los hogares de ambos animales, la derecha siendo de los carnívoros y la izquierda de herbívoros. En estas áreas, los animales podrán resguardarse con seguridad de cualquier animal ya que solo los de su misma especie pueden entrar en dicho lugar. Aquí los animales descansarán hasta que ambas barras, de energía y hambre, esten completas. Una vez recuperados, continuarán con sus rutinas normales.

- Esparcidos por el terreno, podremos encontrar arbustos con frutos para alimentar a los herbívoros. Estos arbustos se posicionarán de manera aleatoria utilizando **Perlin Noise**, un tipo de ruido basado en gradientes, desarrollado por Ken Perlin en 1983. La ventaja que tiene el Perlin Noise frente a otros ruidos clásicos ( como puede ser el White Noise ) es que es un ruido aleatorio, pero coherente. Después de que un ciervo se pare a comer, deberán transcurrir unos segundos antes de que salgan nuevos frutos.

- Durante la partida habra un número constante de conejos rondando el área. En caso de perder alguno, saldrán de sus madrigueras nuevos conejos, hasta llegar al número predefinido deseado. Este podrá ser modificado por el usuario para realizar distintas pruebas.

  ![Diagrama terreno](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/5cc055e6-0b0c-44c3-9b0e-10fed633d730)

### Ciervo
El esquema de comportamiento en Behaviour Bricks de los ciervos es el siguiente:

 ![herbivoros](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/99989921/4f87fbc7-fe76-4c35-8952-f94b6ed484b5)
 
En primer lugar harán la comprobación más prioritaria, siendo esta la de su sentido del oído, para comprobar si están siendo acechados por algún lobo y en ese caso iniciar la huida.

A continuación comprobarán sus niveles de hambre, en caso de necesitarlo buscarán el arbusto más cercano, y si está disponible comerán de él. Si por el contrario el arbusto está ocupado, o siguen teniendo hambre, repetirán esa búsqueda con el siguiente arbusto más cercano hasta quedar satisfechos.

Una vez comprobada el hambre, comprobarán su energía, que en el caso de estar baja, les obligará a volver a su "guarida" para reponerla.

En caso de que no se cumpla ninguna de las condiciones anteriores (ha detectado a un enemigo, tiene hambre o tiene sueño), el animal se dedicará a merodear por el escenario.


### Lobos
El esquema de comportamiento en Behaviour Bricks de los lobos es el siguiente:

![carnivoros](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/99989921/cce034d8-0bc6-44bd-9d9e-6d45cd097987)

En el caso de los lobos, como no están en peligro de ser perseguidos su primera comprobación será su olfato. Esto es así porque en caso de chequear primero el hambre y no encontrar ningún rastro nunca saldría de ese bucle.

En caso de haber encontrado un rastro y tener hambre, seguirán el rastro de la presa hasta estar lo suficientemente cerca como para verla, momento en el cual comenzarán a perseguirla hasta cazarla o perder su rastro.

En segundo lugar comprobarán su energía, lo cual funcionará igual que en el caso de los ciervos, en caso de estar baja irán a la "guarida" a descansar.

Y por último, también símil a los ciervos, si todas estas comprobaciones fallan, se limitarán a merodear por el escenario.

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

## Punto de partida
Se parte de las tres prácticas ya realizadas para la asignatura, las cuales podremos usar como base para el desarrollo de los distintos comportamientos, sistemas de percepción, y métodos de navegación.

La idea es desarrollar los diferentes comportamientos y estados de los animales usando behaviour bricks, y teniendo en cuenta el estado de cada uno de ellos y cómo afecta a sus acciones. Su desplazamiento usaría comportamientos como el de huída, persecución o merodeo pero aplicados a un NavMesh como el usado en la tercera práctica, e integrándolos con el uso de waypoints. 

#### Wander
```
class Wander extends AgentBehaviour:
    wanderOffset : float = 1.5f
    wanderRadius : float = 4f
    wanderRate : float = 0.4f
    wanderOrientation : float = 0f

    timeToWait : float = 0.1f
    float auxTime : float = 0f
	
    function RandomBinomial() -> float
        return (Random.value : float - Random.value : float)
	
    function OrientationToVector(orientation : float) -> Vector3
	return (Cos(orientation), 0, Sin(orientation)) : Vector3

    function Start() -> void
	agente.orientacion = Random(0f, 360f);
	
    function GetDireccion() -> Direccion
	result : Direccion
        if (auxTime > timeToWait)
        {
	    wanderOrientation += RandomBinomial() * wanderRate
	    targetOrientation : float = wanderOrientation + agent.orientation
	    targetPosition : Vector3 = agent.transform.position + (wanderOffset * OrientationToVector(agent.orientation))	
	    targetPosition += wanderRadius * OrientationToVector(targetOrientation)
	    result.lineal = targetPosition - agent.transform.position
	    result.lineal.Normalize()
	    result.lineal *= agent.accelerationMax
	    return result
	    auxTime = 0
	}
	else result = agente.direction
	auxTime += deltaTime
```

#### Pursue
```
class Pursue extends Seek:
    maxPrediction : float = 2.0f
    
    function SetObjetive(obj : GameObject) -> void:
        objetive = obj

    function getDirection() -> Direction:
        direction : Direction

        dir : Vector3 = objetive.position - character.position
        distance : float = dir.magnitude

        speed : float = Rigidbody.velocity.magnitude
        prediction : float

        if speed <= distance / maxPrediction:
            prediction = maxPrediction
        else:
            prediction = distance / speed

        predictionObjetive : Vector3 = objetive.position + objetive.Rigidbody.velocity * prediction
        SetDirection(direction, predictionObjetive)
        return direction
```

#### Flee
```
class Flee extends AgentBehaviour:
    distance : float = 7
    timeToTarget : float = 0.1f

    function GetDirection() -> Direction
        direction : Direction 
        dir : Vector3 = transform.position - objetive.transform.position
        distance = dir.magnitude
        speed : float = 0
        speed = agent.velocityMax
        dir.Normalize()
        dir *= speed
        direction.lineal = dir - agent.velocity
        direction.lineal /= timeToTarget

        if direction.lineal.magnitude > agent.acelerationMax
            direction.lineal.Normalize()
            direction.lineal *= agent.acelerationMax

        direction.angular = 0
        return direction
```

##### LookAt
Estado que apunta al target
```
class LookAt extends State
    _finished : bool
    function void Update() -> void
        transform.LookAt(EnemyBlackboard.target)
        _finished = true
```

##### MoveToGameObject
Estado en el que se mueve a una entidad
```
class MoveToGameObject extends State
    _target : GameObject
    _closeDistance : float
    _lockToFirstGameObjectPosition : bool

    _navAgent : NavMeshAgent
    _targetTransform : Transform

    function Enter() -> void:
        if _target is null:
            return

        _targetTransform = _target.transform

        _navAgent = _gameObject.NavMeshAgent
        if _navAgent is null:
            _navAgent = _gameObject.AddComponent<NavMeshAgent>()

        path : NavMeshPath = new NavMeshPath();
        if _navAgent.CalculatePath(_targetTransform.position, path):
            corners = path.corners
            fullDistance : float = 0f

            for int i = 1; i < corners.Length; i++:
                fullDistance += Distance(corners[i - 1], corners[i])

            if fullDistance > _closeDistance:
                _navAgent.SetDestination(_targetTransform.position)


            if UNITY_5_6_OR_NEWER:
                _navAgent.isStopped = false;
            else:
                navAgent.Resume();

    function void Update() -> void:
        if not _lockToFirstGameObjectPosition and _navAgent.destination not equals _targetTransform.position:
            _navAgent.SetDestination(_targetTransform.position)

    function Exit() -> void:
        if UNITY_5_6_OR_NEWER:
            if _navAgent not equals null:
                _navAgent.isStopped = true
            else:
                if navAgent not equals null
                    navAgent.Stop()
```
## Pruebas y métricas

## Producción
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
