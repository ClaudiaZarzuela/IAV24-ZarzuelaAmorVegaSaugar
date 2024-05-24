/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Separacion : ComportamientoAgente
    {
        // Umbral en el que se activa
        [SerializeField]
        float threshold;

        // Coeficiente de reducción de la fuerza de repulsión
        [SerializeField]
        float decayCoefficient;

        public GestorJuego game=null;


        public override Direccion GetDireccion()
        {
            Direccion result = new Direccion();
            result.lineal = Vector3.zero;
            if (game != null)
            {
                foreach (GameObject target in game.rats)
                {
                    if (target != gameObject)
                    {
                        Vector3 dir = target.transform.position - transform.position;
                        float dist = dir.magnitude;

                        if (dist < threshold)
                        {
                            float strength = Mathf.Min(decayCoefficient / (dist * dist), agente.aceleracionMax);

                            dir.Normalize();
                            result.lineal += (strength * dir);
                        }
                    }
                }
            }

            return result;
        }
    }
}