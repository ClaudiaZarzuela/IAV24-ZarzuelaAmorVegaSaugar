using System;
using System.Collections;
using System.Collections.Generic;
using UCM.IAV.Movimiento;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

namespace UCM.IAV.Movimiento
{
    public class Align : ComportamientoAgente
    {
        //Sive tambien para movimiento en manada con un array de Transforms
        private Transform target; 
        [SerializeField] private float alignDistance = 8.0f;
        private void Start()
        {
            target = objetivo.transform;
        }

        public override Direccion GetDireccion()
        {

            Direccion direccion = new Direccion();
            direccion.lineal = Vector3.zero;
           
            Vector3 targetDir = target.position - transform.position;
            if (targetDir.magnitude < alignDistance)
                direccion.lineal += target.GetComponent<Rigidbody>().velocity;

            if (direccion.lineal.magnitude > agente.aceleracionMax) 
                direccion.lineal = direccion.lineal.normalized * agente.aceleracionMax; 

            return direccion;
        }

    }

}
