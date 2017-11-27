using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPI
{
    [Route("api/person")]
    public class WhatToDo : Controller
    {
        //Variablen
        ArrayList personlist;//= new ArrayList();

        //Konstruktor
        public WhatToDo()
        {
            this.personlist = new ArrayList();
        }
        
        /// <summary>
        /// Gets all Items from Person
        /// </summary>
        /// <returns>JSON String of the Database Response</returns>
        [HttpGet]
        public IActionResult GetAllItems()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.GetSqlConnectionString().ConnectionString))
                {
                    connection.Open();

                    String sql = "select * from person;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Person person = new Person(Int32.Parse(reader.GetSqlValue(0).ToString()), reader.GetSqlValue(1).ToString(), reader.GetSqlValue(2).ToString(), reader.GetSqlValue(3).ToString());
                                personlist.Add(person);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                return BadRequest();
            }
            
            return Ok(this.personlist);
        }


        [HttpPost]
        public IActionResult CreatePerson([FromBody] string test)//int PersonID, [FromBody] string prename, [FromBody] string surname, [FromBody] string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.GetSqlConnectionString().ConnectionString))
                {
                    /*
                    connection.Open();

                    String sql = $"INSERT INTO PERSON (PersonID, prename, surname, email) VALUES ({PersonID}, {prename}, {surname}, {email});";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    */
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            //string x = prename + " " + surname;
            string stringlein = "teststring --> "+test;
            return Ok(stringlein);
            //return Ok($"Person with PersonID {PersonID} and Name {prename} {surname} was created!");
        }

        /// <summary>
        /// Deletes one record from Person
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteOneItem(int index)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.GetSqlConnectionString().ConnectionString))
                {
                    connection.Open();

                    String sql = $"delete from person where PersonID = {index};";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                return BadRequest();
            }
            return Ok($"Person with PersonID {index} was deleted!");
        }

        //non API Methods
        public SqlConnectionStringBuilder GetSqlConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "posecsharphomeworkserver.database.windows.net";
            builder.UserID = "Dejavu";
            builder.Password = "Passwort123";
            builder.InitialCatalog = "homeworkdatabase";

            return builder;
        }
    }
}
