using System.Collections.Generic;

namespace WebServer.Models
{
    public class Repository
    {
        private static int counter { get; set; }
        public static IDictionary<int, Person> People = new Dictionary<int, Person>();

        public static Person GetPersonById(int id)
        {
            return People[id];
        }

        public static void AddPerson(Person person)
        {
            int id = counter++;
            if (!People.ContainsKey(id))
            {
              person.ID = id;
              People.Add(id, person);
            }
        }

        public static void AddPerson(int id, Person person)
        {
            if (!People.ContainsKey(id))
              People.Add(id, person);
        }

        public static void RemovePersonById(int id)
        {
            if (People.ContainsKey(id))
              People.Remove(id);
        }

        public static void ReplacePersonByID(int id, Person person) 
        {
            if (People.ContainsKey(id))
              People[id] = person;
        }

        public static int Count()
        {
            return People.Count;
        }
    }
}
