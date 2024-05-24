/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class Arrival : ComportamientoAgente
    {
        // El radio para llegar al objetivo
        public float rObjetivo;

        // El radio en el que se empieza a ralentizarse
        public float rRalentizado;  

        public void SetObjective(GameObject obj)
        {
            objetivo= obj;        
        }
        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            Vector3 direction = objetivo.transform.position-transform.position;
            float distance = direction.magnitude;

            float objectiveSpeed = 0;
            if(distance < rObjetivo)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                direccion.lineal = Vector3.zero;
                return direccion;
            }
            else if(distance <= rRalentizado)
            { 
                objectiveSpeed = agente.velocidadMax * (distance / rRalentizado);
            }

            Vector3 objetiveVelocity = direction;
            objetiveVelocity.Normalize();
            objetiveVelocity *= objectiveSpeed;
            direccion.lineal= objetiveVelocity;
            direccion.angular = 0;
            return direccion;
        }
    }
}
