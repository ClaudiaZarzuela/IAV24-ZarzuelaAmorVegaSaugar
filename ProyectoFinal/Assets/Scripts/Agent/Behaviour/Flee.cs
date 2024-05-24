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
    /// Clase para modelar el comportamiento de HUIR a otro agente
    /// </summary>
    public class Flee : ComportamientoAgente
    {
        public float distancia = 7; 

        public float timeToTarget = 0.1f;
        public override Direccion GetDireccion()
        {
            Direccion direccion = new Direccion();
            Vector3 dir =  transform.position - objetivo.transform.position;
            distancia = dir.magnitude;

            float speed = 0;
            speed = agente.velocidadMax;

            dir.Normalize();
            dir *= speed;

            direccion.lineal = dir - agente.velocidad;
            direccion.lineal /= timeToTarget;

            if (direccion.lineal.magnitude > agente.aceleracionMax)
            {
                direccion.lineal.Normalize();
                direccion.lineal *= agente.aceleracionMax;
            }

            direccion.angular = 0;
            return direccion;
           
        }
    }
}
