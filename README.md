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

Para dar vida a la simulación contaremos con tres tipos de animales. Para representar a los herbívoros usaremos ciervos, para los carnívoros lobos y añadiremos conejos para equilibrar el hábitat y evitar que los lobos acaben con los ciervos rápidamente. Estos conejos simplemente se usarán como cebo y se encargarán exclusivamente de merodear. En caso de oler a ambos animales, los lobos preferirán cazar a los ciervos. 

Cada animal tendrá que tener cuidado de no dejar bajar sus niveles demasiado, ya que éstos tendrán un efecto en sus habilidades. Ambos comenzarán merodeando tranquilamente por el bosque, bajando constantemente  poco a poco su nivel de energía y rápidamente su nivel de hambre. 

![Animales](https://github.com/ClaudiaZarzuela/IAV24-ZarzuelaAmorVegaSaugar/assets/100291375/a18be436-1821-4cbc-96f9-c168641cff51)

## Terreno
El hábitat consta de distintas zonas valiosas:
- A los extremos encontramos los hogares de ambos animales, la derecha siendo de los carnívoros y la izquierda de herbívoros. En estas áreas, los animales podrán resguardarse con seguridad de cualquier animal ya que solo los de su misma especie pueden entrar en dicho lugar. Aquí los animales descansarán hasta que ambas barras, de energía y salud, esten completas. Una vez recuperados, continuarán con sus rutinas normales.

- Esparcidos por el terreno, podremos encontrar arbustos con frutos para alimentar a los herbívoros. Estos arbustos se posicionarán de manera aleatoria utilizando **Perlin Noise**, un tipo de ruido basado en gradientes, desarrollado por Ken Perlin en 1983. La ventaja que tiene el Perlin Noise frente a otros ruidos clásicos ( como puede ser el White Noise ) es que es un ruido aleatorio, pero coherente. Después de que un ciervo se pare a comer, deberán transcurrir unos segundos antes de que salgan nuevos frutos.

- Durante la partida habra un número constante de conejos rondando el área. En caso de perder alguno, saldrán de sus madrigueras nuevos conejos, hasta llegar al número predefinido deseado. Este podrá ser modificado por el usuario para realizar distintas pruebas.

  
**DIAGRAMA DEL TERRENO** 

**Ciervo**


**Lobos**

Arbustos **GENERACION DE ARBUSTOS CON PERLIN**

Hogares


---------------------------------------------------------
**EXPLICAR HUD Y LAS DIFERENTES CÁMARAS**
**INPUT DE JUGADOR** 

 
## Punto de partida

Se parte de las tres prácticas ya realizadas para la asignatura, las cuales podremos usar como base para el desarrollo de los distintos comportamientos, sistemas de percepción, y métodos de navegación.

La idea es desarrollar los diferentes comportamientos y estados de los animales usando behaviour bricks, y teniendo en cuenta el estado de cada uno de ellos y cómo afecta a sus acciones. Su desplazamiento usaría comportamientos como el de huída o persecución o merodeo pero aplicados a un NavMesh como el usado en la tercera práctica, e integrándolos con el uso de waypoints. 

## Diseño de la solución
### Mecánicas de juego
Las mecánicas de juego que se pretenden implementar son las siguientes:
![Mecánicas Proyecto Final]()


## Diagrama de comportamientos
Partimos del siguiente diagrama inicial:
![Diagrama Inicial BB]()

Merodeo
Perseguir
Huir

## Pruebas y métricas

## Producción

## Licencia
Claudia Zarzuela, Andrea Vega Saugar, autores de la documentación, código y recursos de este trabajo, no concedemos permiso permanente a los profesores de la Facultad de Informática de la Universidad Complutense de Madrid para utilizar nuestro material, con sus comentarios y evaluaciones, con fines educativos o de investigación; ya sea para obtener datos agregados de forma anónima como para utilizarlo total o parcialmente reconociendo expresamente nuestra autoría.

Una vez superada con éxito la asignatura se prevee publicar todo en abierto (la documentación con licencia Creative Commons Attribution 4.0 International (CC BY 4.0) y el código con licencia GNU Lesser General Public License 3.0).

## Referencias
