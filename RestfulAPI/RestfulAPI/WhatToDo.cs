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
        /// <returns></returns>
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

        /// <summary>
        /// Gets one Item from the table Person
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{index}")]
        public IActionResult GetOneItem(int index)
        {
            Person person = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(this.GetSqlConnectionString().ConnectionString))
                {
                    connection.Open();

                    String sql = "select * from person where PersonID = "+index+";";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                person = new Person(Int32.Parse(reader.GetSqlValue(0).ToString()), reader.GetSqlValue(1).ToString(), reader.GetSqlValue(2).ToString(), reader.GetSqlValue(3).ToString());
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

            return Ok(person);
        }

        /// <summary>
        /// Creates a new Person in the database
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreatePerson([FromBody] Person person)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.GetSqlConnectionString().ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"INSERT INTO PERSON (PersonID, prename, surname, email) VALUES ({person.PersonID}, '{person.prename}', '{person.surname}', '{person.email}');";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok($"Person with PersonID {person.PersonID} and Name {person.prename} {person.surname} was created!");
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

        /// <summary>
        /// Updates one Item of Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{index}")]
        public IActionResult UpdateOneItem([FromBody]Person person, int index)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.GetSqlConnectionString().ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE PERSON SET prename = '{person.prename}', surname = '{person.surname}', email = '{person.email}' WHERE PersonID = {index};";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok($"Person with PersonID {index} and Name {person.prename} {person.surname} has been updated!");
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
