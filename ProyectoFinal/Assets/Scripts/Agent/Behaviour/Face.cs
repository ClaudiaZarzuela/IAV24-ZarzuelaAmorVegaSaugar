//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UCM.IAV.Movimiento;
//using UnityEngine;
//using static UnityEditor.FilePathAttribute;
//using static UnityEngine.GraphicsBuffer;

//namespace UCM.IAV.Movimiento
//{
//    public class Face : ComportamientoAgente
//    {
//        public override Direccion GetDireccion()
//        {
//            Direccion dir = new Direccion();
//            Vector3 objetive = objetivo.transform.position;
//            dir.lineal = objetive - transform.position;
//            dir.lineal.Normalize();
//            dir.lineal *= agente.velocidadMax;
//            dir.angular = 0;

//            if (dir.lineal.x != 0 || dir.lineal.z != 0)
//            {
//                //Rotación del personaje hacia donde camina (suavizado)
//                float anguloDestino = Mathf.Atan2(dir.lineal.x, dir.lineal.z) * Mathf.Rad2Deg;
//                //Esto es raro pero Brackeys dice que funciona
//                float anguloSuave = Mathf.SmoothDampAngle(agente.transform.eulerAngles.y, anguloDestino, ref agente.velocidadGiroSuave, 0.1f);

//                agente.transform.rotation = Quaternion.Euler(0f, anguloSuave, 0f);
//            }

//            dir.lineal = Vector3.zero;
//            return dir;
//        }
//    }
//}
