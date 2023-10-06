using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Proyecto_David_Angulo.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;


namespace Proyecto_David_Angulo.Controllers
{


    public class LoginController : Controller
    {

        static string conexion = "Data Source=LAPTOP-AMP5OCIH;Initial Catalog=BD_proyecto;Integrated Security=True";

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegistrarU()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegistrarU(Usuario OUsuario)
        {
            bool registrado;
            string mensaje;

            if (OUsuario.Clave == OUsuario.ConfirmarClave) {
            
                OUsuario.Clave = (OUsuario.Clave);                  
            }

            else
            {
                ViewData["Mensaje"] = "Las constraseña no Coinciden";
                return View();
            }

            using(SqlConnection conn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario",conn);
                cmd.Parameters.AddWithValue("Correo", OUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", OUsuario.Clave);
                cmd.Parameters.Add("Registrado",SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();

            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Login(Usuario OUsuario)
        {
            OUsuario.Clave = OUsuario.Clave;

            using (SqlConnection conn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", conn);
                cmd.Parameters.AddWithValue("Correo", OUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", OUsuario.Clave);
             
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                OUsuario.IdUsuario =Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if(OUsuario.IdUsuario != 0) {

                Session["usuario"] = OUsuario;
                return RedirectToAction("Index", "Menu");

            }
            else
            {
                ViewData["Mensaje"] = "Usuario No Encontrado";
                return View();

            }




        }

    }
}