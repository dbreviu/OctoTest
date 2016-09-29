using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;
using OctoTest.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OctoTest.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        // GET: /<controller>/
        private static List<Person> _people;

        private List<Person> GetPeople()
        {
            if (_people == null)
            {
                using (StreamReader file = System.IO.File.OpenText("people.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    _people = (List<Person>)serializer.Deserialize(file, typeof(List<Person>));//, typeof(Person[]));

                }
            }
            return _people;

        }

        public List<Person> SearchPerson([FromQuery] string name, [FromQuery] string phone, [FromQuery] string zip)
        {
           
                var result = GetPeople();
            if (!string.IsNullOrWhiteSpace(name))
            {
                if (name.Contains(" "))
                {
                    var firstName = name.Split(' ')[0];
                    var lastName = name.Split(' ')[1];
                    result = result.Where(p => p.FirstName.StartsWith(firstName) || p.LastName.StartsWith(lastName)).ToList();

                }
                else
                {
                    result = result.Where(p => p.FirstName.StartsWith(name) || p.LastName.StartsWith(name)).ToList();
                }

                }
                if (!string.IsNullOrWhiteSpace(phone))
                    result = result.Where(p => p.Phone.Contains(phone)).ToList();
                if (!string.IsNullOrWhiteSpace(zip))
                    result = result.Where(p => p.Zip.Contains(zip)).ToList();


                return result;
            }
        }
    }
