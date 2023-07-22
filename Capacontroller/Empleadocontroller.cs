using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace EnviandoPDF.Capacontroller
{
    class Empleadocontroller
    {
        public DataTable getempleados()
        {
            SqlConnection Con = new Conexion().GetConexion("BDDConexion");
            Con.Open();



            /*
            DataTable dataTable = new DataTable();
            string query = " select CodigoEmpleado,  NombreEmpleado , ApellidoEmpleado ,FechaIngreso  ,Antiguedad  , Salario   from empleado ";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.CommandTimeout = 99999;
            new SqlDataAdapter(cmd).Fill(dataTable);
            */


            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("showusuario", Con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(dataTable);


            Con.Close();
            return dataTable;



        }


    }
}

    

