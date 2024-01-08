using System.Data.SqlClient; 
using ApiCaller;  
using ClassLibrary1; 

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        //M�todo de prueba para verificar la conexi�n a la base de datos y la obtenci�n de registros de la tabla TUsers.
        [TestMethod]
        public void TestMethod1()
        {
            conexion t = new conexion();  //Crea una instancia de la clase 'conexion'.
            t.connect();  //Establece una conexi�n a la base de datos.
            t.TestDB();  //Realiza una prueba de conexi�n y obtiene registros de la tabla TUsers.

            //Verifica que el n�mero de filas obtenidas sea igual a 1.
            Assert.AreEqual(5, t.TestDB().Tables[0].Rows.Count);

            //Imprime la cadena de conexi�n para prop�sitos de depuraci�n.
            System.Diagnostics.Trace.WriteLine(t.Connect.ConnectionString);
        }

        //M�todo de prueba para obtener usuarios espec�ficos de la base de datos.
        [TestMethod]
        public void TestMethodGetUsers()
        {
            conexion t = new conexion(); 
            t.connect();  

            //Intenta obtener la lista de usuarios cuyo nombre de usuario sea el siguiente.
            var list = t.GetUsers(new Users() { cUser = "julia"});

            //Imprime la cantidad de usuarios encontrados para prop�sitos de depuraci�n.
            System.Diagnostics.Trace.WriteLine($"N�mero de usuarios encontrados: {list.Count}");

            //Verifica que la lista de usuarios no est� vac�a.
            Assert.IsTrue(list.Count > 0, "La lista est� vac�a");

        }

        //M�todo de prueba para crear nuevos usuarios en la base de datos.
        [TestMethod]
        public void TestMethodCreateUsers()
        {
            conexion t = new conexion();
            t.connect();

            Users userCreado = new Users() 
            {
                idUser = 8,
                cUser = "UsuarioTest",
                cPass = "Contrase�aTest",
                cEmail = "emailtest@gmail.com",
                nAdministrator = 0,
                nManager = 1,
                idNegocio = 1,
                nValidated = 0
            };

            //Verificar que el campo cUser no sea nulo antes de llamar a CreateUser.
            Assert.IsNotNull(userCreado.cUser, "El campo cUser no puede ser nulo");

            bool resultadoCreacion = t.CreateUsers(userCreado);

            //Verifica que la creaci�n del usuario fue exitosa.
            Assert.IsTrue(resultadoCreacion,"Error al crear usuario");

            //Comentario final sobre el resultado y detalles de la prueba.
            System.Diagnostics.Trace.WriteLine("Prueba \"TestCreateUser\" exitosa. " +
            "Filas obtenidas: " + t.TestDB().Tables[0].Rows.Count);
        }

        //M�todo de prueba  para modificar informaci�n de usuarios existentes en la base de datos.
        [TestMethod]
        public void TestMethodModifyUsers()
        {
            conexion t = new conexion();
            t.connect();
            t.TestDB();

            //Crear un usuario para actualizar.
            Users userModify = new Users()
            {
                idUser = 7,
                cUser = "UsuarioTest2",
                cPass = "Contrase�aTest2",
                cEmail = "emailTest2@gmail.com",
                nAdministrator = 2,
                nManager = 0,
                idNegocio = 1,
                nValidated = 0
            };

            //Ejecutar la funci�n.
            t.ModifyUsers(userModify);

            var list = t.GetUsers(new Users() { cUser = "UsuarioTest2"});

            //Comprobar que la lista no est� vac�a.
            Assert.IsTrue(list.Count > 0, "La lista est� vac�a");

            //Comentario final sobre el resultado y detalles de la prueba.
            System.Diagnostics.Trace.WriteLine("Prueba \"TestModifyUser\" exitosa. " +
            "Filas obtenidas: " + t.TestDB().Tables[0].Rows.Count);
        }

        //M�todo de prueba para eliminar usuarios de la base de datos.
        [TestMethod]
        public void TestMethodDeleteUsers()
        {
            conexion t = new conexion();  
            t.connect(); 
            t.TestDB();

            int userDelete = 6;

            t.DeleteUsers(userDelete);
            
            var list = t.GetUsers(new Users() { idUser = userDelete });

            //Comprobar que la lista no est� vac�a.
            Assert.IsTrue(list.Count == 0, "El usuario no ha sido borrado");

            //Comentario final sobre el resultado y detalles de la prueba.
            System.Diagnostics.Trace.WriteLine("Prueba \"TestDeleteUser\" exitosa. " +
            "Filas obtenidas: " + t.TestDB().Tables[0].Rows.Count);

        }
    }
}
