//using System.Collections;
//using System.Collections.Generic;
//using UCM.IAV.Movimiento;
//using UnityEngine;

//public class Cohesion : Seek
//{
//    public float viewAngle = 60f;

//    public GestorJuego game = null;

//    public override Direccion GetDireccion()
//    {
//        Direccion result = new Direccion();

//        Vector3 centerMass = Vector3.zero;
//        int count = 0;
//        if (game != null)
//        {
//           foreach(GameObject target in game.rats) 
//           {
//                Vector3 dir = target.transform.position - transform.position;
//                if(Vector3.Angle(dir,transform.forward) < viewAngle)
//                {
//                    centerMass += target.transform.position;
//                    count++;
//                }
//           }
//           if(count>0)
//           {
//                centerMass = centerMass / count;
//                SetDireccion(result, centerMass);
//           }
//        }
//        return result;
//    }
//}

