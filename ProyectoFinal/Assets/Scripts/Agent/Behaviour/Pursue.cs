///*    
//   Copyright (C) 2020-2023 Federico Peinado
//   http://www.federicopeinado.com

//   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
//   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

//   Autor: Federico Peinado 
//   Contacto: email@federicopeinado.com
//*/
//using System;
//using System.Numerics;
//using UnityEngine;
//using Vector3 = UnityEngine.Vector3;

//namespace UCM.IAV.Movimiento
//{
//    public class Pursue : Seek
//    {
//        [SerializeField] private float maxPrediction = 2f;
//        public void SetObjective(GameObject obj)
//        {
//            objetivo = obj;
//        }
//        public override Direccion GetDireccion()
//        {
//            Direccion direccion = new Direccion();

//            Vector3 dir = objetivo.transform.position - transform.position;
//            float distancia = dir.magnitude;

//            float speed = GetComponent<Rigidbody>().velocity.magnitude;
//            float prediction; 
//            if (speed <= (distancia / maxPrediction))
//                prediction = maxPrediction; 
//            else prediction = distancia / speed;

//           Vector3 predictedObjetive = objetivo.transform.position +
//                (objetivo.GetComponent<Rigidbody>().velocity * prediction);

//            SetDireccion(direccion,predictedObjetive);
//            return direccion;
//        }
//    }
//}
