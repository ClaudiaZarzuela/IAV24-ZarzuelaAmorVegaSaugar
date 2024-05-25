//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UCM.IAV.Movimiento;
//using UnityEngine;
//using static UnityEditor.FilePathAttribute;
//using static UnityEngine.GraphicsBuffer;

//namespace UCM.IAV.Movimiento
//{
//    public class LookWhereYoureGoing : ComportamientoAgente
//    {
//        public override Direccion GetDireccion()
//        {
//            if (agente.GetDirection().lineal.x != 0 || agente.GetDirection().lineal.z != 0)
//            {
//                //Rotación del personaje hacia donde camina (suavizado)
//                float anguloDestino = Mathf.Atan2(agente.GetDirection().lineal.x, agente.GetDirection().lineal.z) * Mathf.Rad2Deg;
//                //Esto es raro pero Brackeys dice que funciona
//                float anguloSuave = Mathf.SmoothDampAngle(agente.transform.eulerAngles.y, anguloDestino, ref agente.velocidadGiroSuave, 0.1f);

//                agente.transform.rotation = Quaternion.Euler(0f, anguloSuave, 0f);
//            }
//            return agente.GetDirection();
//        }
//    }
//}
