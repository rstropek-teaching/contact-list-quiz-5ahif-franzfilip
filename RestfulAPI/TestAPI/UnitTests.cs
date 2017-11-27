using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestfulAPI;
using System;
using System.Data.SqlClient;

namespace TestAPI
{
    [TestClass]
    public class UnitTests
    {
        //funktioniert die Verbindung zur Datenbank?
        [TestMethod]
        public void ConnectionEstablished()
        {
            WhatToDo needed = new WhatToDo();
            try
            {
                using (SqlConnection connection = new SqlConnection(needed.GetSqlConnectionString().ConnectionString))
                {
                    connection.Open();
                    //Gegenteil von Assert.Fail
                    return;
                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        //funktioniert das hinzufügen? wird überprüft mit einem OkObjectResult
        public void TestMethod()
        {
            WhatToDo needed = new WhatToDo();

            needed.DeleteOneItem(99);

            Person person = new Person(99, "prenametest", "surnametest", "testemail@gmail.com");
            OkObjectResult result = (OkObjectResult)needed.CreatePerson(person);
            OkObjectResult issame = new OkObjectResult("Person with PersonID 99 and Name prenametest surnametest was created!");

            Console.WriteLine(result.GetType());
            Console.WriteLine(issame.GetType());

            Assert.AreEqual(result.GetType(), issame.GetType());
            
        }
    }
}
