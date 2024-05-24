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
    public class Seek : ComportamientoAgente
    {
        public override Direccion GetDireccion()
        {            
            Direccion direccion = new Direccion();
            SetDireccion(direccion, objetivo.transform.position);
            return direccion;
        }
        public void SetDireccion(Direccion dir, Vector3 objetive)
        {

            dir.lineal = objetive - transform.position;
            dir.lineal.Normalize();
            dir.lineal *= agente.velocidadMax;
            dir.angular = 0;
            if(dir.lineal.magnitude > agente.aceleracionMax)
            {
                dir.lineal.Normalize();
                dir.lineal *= agente.aceleracionMax;
            }
        }

    }
}
