///*    
//   Copyright (C) 2020-2023 Federico Peinado
//   http://www.federicopeinado.com

//   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
//   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

//   Autor: Federico Peinado 
//   Contacto: email@federicopeinado.com
//*/

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using UnityEngine;

//namespace UCM.IAV.Movimiento
//{
//    /// <summary>
//    /// Clase para modelar el comportamiento de WANDER a otro agente
//    /// </summary>
//    public class Merodear : ComportamientoAgente
//    {

//        [SerializeField] private float wanderOffset = 1.5f;
//        [SerializeField] private float wanderRadius = 4f;
//        [SerializeField] private float wanderRate = 0.8f;
//        [SerializeField] private float wanderOrientation = 0f;

//        private float timeToWait = 0.1f;
//        private float auxTime = 0f;

//        private float RandomBinomial()
//        {
//            return UnityEngine.Random.value - UnityEngine.Random.value;  
//        }

//        private Vector3 OrientationToVector(float orientation)
//        {
//            return new Vector3(Mathf.Cos(orientation), 0, Mathf.Sin(orientation));
//        }

//        private void Start()
//        {
//            agente.orientacion = UnityEngine.Random.Range(0f, 360f);
//        }

//        public override Direccion GetDireccion()
//        {
//            Direccion result = new Direccion();

//            if (auxTime > timeToWait)
//            {
//                wanderOrientation += RandomBinomial() * wanderRate;

//                float targetOrientation = wanderOrientation + agente.orientacion;

//                Vector3 targetPosition = agente.transform.position + (wanderOffset * OrientationToVector(agente.orientacion));

//                targetPosition += wanderRadius * OrientationToVector(targetOrientation);

//                result.lineal = targetPosition - agente.transform.position;
//                result.lineal.Normalize();

//                result.lineal *= agente.aceleracionMax;

//                auxTime = 0;
//            }
//            else result = agente.GetDirection();
//            auxTime += UnityEngine.Time.deltaTime;
            
//            return result;
//        }

//    }
//}
