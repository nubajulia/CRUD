using ApiCaller;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class conexion
    {
        public SqlConnection? Connect { get; set; }

        //Método para establecer la conexión a la base de datos.
        public void connect() {

            Connect = new SqlConnection();  //Inicializa una nueva instancia de SqlConnection.

            Connect.ConnectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CRUD;Data Source=JULIA"; //Establece la cadena de conexión.
            Connect.Open();     //Abre la conexión a la base de datos.
        }

        //Método para realizar una prueba de conexión a la base de datos y obtener todos los registros de la tabla TUsers.
        public DataSet TestDB()
        {
            DataSet dt = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Selección de todos los registros de la tabla TUsers.
            adapter.SelectCommand = new SqlCommand("select * from TUsers;", Connect);

            //Llenar el DataSet con los resultados de la consulta.
            adapter.Fill(dt);

            return dt;
        }

        //Método para realizar consultas genéricas a procedimientos almacenados.
        public DataSet queryGenericStored(string  query, List<KeyValuePair<string,dynamic>> parameters = null)
        {
            DataSet dt = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, Connect);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Clear();

            //Agrega parámetros al comando.
            foreach (KeyValuePair<string,dynamic> param in parameters)
            {
                AddParameter(ref adapter, param);
            }

            //Llena el DataSet con los resultados de la consulta.
            adapter.Fill(dt);
            return dt;
        }

        //Método para agregar parámetros a un comando.
        public void AddParameter(ref SqlDataAdapter sel, KeyValuePair<string,dynamic> val)
        {
            if (sel== null)
            {
                sel.SelectCommand.Parameters.AddWithValue(val.Key, val.Value);
            }
        }

        //Método para obtener una lista de usuarios según los parámetros de búsqueda.
        public IList <Users> GetUsers(Users user_to_search)
        {
            //Crear una lista de parámetros para el procedimiento almacenado.
            List<KeyValuePair<string,dynamic>> userparam = new List<KeyValuePair<string,dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@cUser", user_to_search.cUser));

            //Realiza la consulta almacenada y convierte los resultados en una lista de objetos Users.
            DataSet ds = queryGenericStored("svp_user_consult", userparam);
            IList<Users> items = ds.Tables[0].AsEnumerable().Select(row =>

            new Users
            {
                idUser = row.Field<int>("idUser"),
                cUser = row.Field<string>("cUser"),
                cPass = row.Field<string> ("cPass"),
                cEmail = row.Field<string>("cEmail"),
                nAdministrator = row.Field<int>("nAdministrator"),
                nManager = row.Field<int>("nManager"),
                idNegocio = row.Field<int>("idNegocio"),
                nValidated = row.Field<int>("nValidated")

            }).ToList();

            return items;
        }

        //Método para crear un nuevo usuario.
        public bool CreateUsers(Users newUser)
        {
            try
            {
                //Crear una lista de parámetros para el procedimiento almacenado.
                List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();

                //Agregar los parametros.
                userparam.Add(new KeyValuePair<string, dynamic>("@idUser", newUser.idUser));
                userparam.Add(new KeyValuePair<string, dynamic>("@cUser", newUser.cUser));
                userparam.Add(new KeyValuePair<string, dynamic>("@cPass", newUser.cPass));
                userparam.Add(new KeyValuePair<string, dynamic>("@cEmail", newUser.cEmail));
                userparam.Add(new KeyValuePair<string, dynamic>("@nAdministrator", newUser.nAdministrator));
                userparam.Add(new KeyValuePair<string, dynamic>("@nManager", newUser.nManager));
                userparam.Add(new KeyValuePair<string, dynamic>("@idNegocio", newUser.idNegocio));
                userparam.Add(new KeyValuePair<string, dynamic>("@nValidated", newUser.nValidated));

                //Ejecuta la consulta almacenada.
                DataSet result = queryGenericStored("svp_user_create", userparam);

                //Verificar si la consulta se ejecutó correctamente.
                return result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear usuario: {ex.Message}");
                return false;
            }
        }

        //Método para modificar información de un usuario existente.
        public bool ModifyUsers(Users userModify)
        {
            try
            {
                //Crear una lista de parámetros para el procedimiento almacenado.
                List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();

                //Agregar los parametros.
                userparam.Add(new KeyValuePair<string, dynamic>("@idUser", userModify.idUser));
                userparam.Add(new KeyValuePair<string, dynamic>("@cUser", userModify.cUser));
                userparam.Add(new KeyValuePair<string, dynamic>("@cNewPass", userModify.cPass));
                userparam.Add(new KeyValuePair<string, dynamic>("@cNewEmail", userModify.cEmail));
                userparam.Add(new KeyValuePair<string, dynamic>("@nNewAdministrator", userModify.nAdministrator));
                userparam.Add(new KeyValuePair<string, dynamic>("@nNewManager", userModify.nManager));
                userparam.Add(new KeyValuePair<string, dynamic>("@idNewNegocio", userModify.idNegocio));
                userparam.Add(new KeyValuePair<string, dynamic>("@nNewValidated", userModify.nValidated));

                //Ejecuta la consulta almacenada.
                DataSet result = queryGenericStored("svp_user_modify", userparam);

                //Verificar si la consulta se ejecutó correctamente.
                return result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0;
            }
            catch(Exception ex) { 
                
                Console.WriteLine($"Error al modificar usuario: {ex.Message}");
                return false;
            }
        }

        //Método para eliminar usuarios según el ID proporcionado.
        public void DeleteUsers(int? userId)
        {
            //Crear una lista de parámetros para el procedimiento almacenado.
            List<KeyValuePair<string, dynamic>> userparam = new List<KeyValuePair<string, dynamic>>();
            userparam.Add(new KeyValuePair<string, dynamic>("@idUser", userId));

            //Ejecuta la consulta almacenada.
            queryGenericStored("svp_user_delete", userparam);
        }
    }
}
