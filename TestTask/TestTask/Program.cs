using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;


namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Response r1 = new Response();
            Json js = new Json();
            js.Deserialization(r1.Get());
            r1.response.Close();


        }
    }
    public class Response
    {
        public WebResponse response;
        public Stream Get()
        {
            WebRequest request = WebRequest.Create("http://api.lod-misis.ru/testassignment");
            response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            return stream;
        }
    }
    public class Json
    {
        public void Deserialization(Stream stream)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Player>));

            List<Player> players = (List<Player>)ser.ReadObject(stream);
            var TeamGroups = from t in players group t by t.Team;
            foreach (var Team in TeamGroups)
            {
                var SortScore = from t in Team orderby t.Score descending select t;
                Console.WriteLine("\nКоманда " + Team.Key);
                Console.WriteLine("Имя игрока:" + "\t\tОчки:");
                foreach (var Player in SortScore)
                {
                    Console.WriteLine("{0}\t\t{1}", Player.PlayerName, Player.Score);
                }

            }
            
            Console.ReadKey();
        }
    }

    public class Player
    {
        public string PlayerName { get; set; }
        public string Team { get; set; }
        public int Score { get; set; }

    }
}

