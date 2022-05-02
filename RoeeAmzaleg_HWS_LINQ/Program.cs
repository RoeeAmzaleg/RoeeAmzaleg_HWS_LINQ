using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RoeeAmzaleg_HWS_LINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n*********** RoeeAmzaleg_HWS_LINQ ***********");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** Exr 4 ***");
            Console.ForegroundColor = ConsoleColor.Cyan;

            List<int> nums = new List<int>() { 10, 20, 30, 40, 50 }.ToList();
            List<string> stringIntsList = nums.Select(x => x.ToString() + ",").ToList(); //method syntax
            stringIntsList.ForEach(x => Console.WriteLine(x));
            var numQuery = (from ints in stringIntsList
                            select ints.ToString()).ToList(); //query syntax

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** Exr 5 ***");
            Console.ForegroundColor = ConsoleColor.Cyan;
            List<string> planet = new List<string>() { "Earth", "Neptune ", "Jupiter", "Mars", "Moon" }.ToList();
            List<string> planetsSize = planet.Where(x => x.Length > 4).ToList(); //method syntax
            planetsSize.ForEach(x => Console.WriteLine(x));
            var planetQuery = (from plt in planetsSize
                               where plt.Length > 4
                               select plt.ToString()).ToList(); //query syntax

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** Exr 6 ***");
            Console.ForegroundColor = ConsoleColor.Cyan;
            List<int> orderByNumbers = nums.OrderBy(x => x.ToString()).ToList();
            List<int> orderByNumbers2 = nums.OrderByDescending(x => x.ToString()).ToList(); //method syntax
            orderByNumbers.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("\n---ByDescending---");
            orderByNumbers2.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("\n--------");
            var orderByNumbersQuery = (from obn in orderByNumbers
                                       orderby obn descending
                                       select obn).ToList(); //query syntax

            List<string> orderByplanet = planet.OrderBy(x => x.Length).ToList();
            List<string> orderByplanet2 = planet.OrderByDescending(x => x.Length).ToList();
            orderByplanet.ForEach(x => Console.WriteLine(x));
            var orderByplanetQuery = (from obp in orderByplanet
                                      orderby obp 
                                       select obp).ToList(); //query syntax
            Console.WriteLine("\n---ByDescending---");

            orderByplanet2.ForEach(x => Console.WriteLine(x));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** Exr 7 ***");
            Console.ForegroundColor = ConsoleColor.Cyan;
            List<int> num1 = new List<int>() { 10, 45, 30, 23, 50 }.ToList();
            List<int> num2 = new List<int>() { 10, 20, 15, 40, 50 }.ToList();
            List<int> sameNumbers = num1.Intersect(num2).ToList();
            List<int> notSameNumbers = num1.Except(num2).ToList();
            var numbersQuery = (from numbers in sameNumbers
                                select numbers.ToString()).ToList(); //query syntax

            Console.WriteLine(".A. = Numbers on the same list");
            sameNumbers.ForEach(x => Console.WriteLine(x));
            Console.WriteLine();

            Console.WriteLine("\n.B. = Numbers not on the same list");
            notSameNumbers.ForEach(x => Console.WriteLine(x));

            Console.WriteLine("\n.C. = Print two list without duplicate numbers ");
            List<int> concatTwoLists = num1.Except(num2).Concat(num2.Except(num1)).OrderBy(x => x).ToList();
            concatTwoLists.ForEach(x => Console.WriteLine(x));

            Console.WriteLine("\n.D. = First number over than 12 ");
            List<int> concat = num1.Concat(num2).OrderBy(x => x).ToList();
            var firstNumber = concat.FirstOrDefault(std => std> 12);
            Console.WriteLine(firstNumber);

            Console.WriteLine("\n.E. = Highest number of both lists ");
            Console.WriteLine(num1.Concat(num2).Max());

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** Exr 8 ***");
            Console.ForegroundColor = ConsoleColor.Cyan;
            List<Agent> agents = new List<Agent>();
            agents.Add(new Agent(3, "Nati"));
            agents.Add(new Agent(4, "Eden"));
            agents.Add(new Agent(2, "Roni"));
            agents.Add(new Agent(1, "Dani"));

            List<Client> clients = new List<Client>();
            clients.Add(new Client(3, "Omer"));
            clients.Add(new Client(6, "Roee"));
            clients.Add(new Client(7, "Chen"));
            clients.Add(new Client(1, "Yosi"));
            GroupedAgentByClientId(clients, agents);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*** Exr 9 ***");
            Console.ForegroundColor = ConsoleColor.Cyan;
            List<Order> orders = new List<Order>();
            orders.Add(new Order("Shirt", 25, 3));
            orders.Add(new Order("Bag", 100, 3));
            orders.Add(new Order("Shoe", 250, 6));
            orders.Add(new Order("Belt", 80, 6));
            OrdersByCustomers(clients, orders);
        }
        static void GroupedAgentByClientId(List<Client> clientsId, List<Agent> agentsId)
        {
            //method syntax
            var groupedAgentByClientId = clientsId.Join(agentsId, client => client.ClientId, agent => agent.AgentId,
                                         (client, agent) => new
                                         {
                                             Clientname = client.ClientName,
                                             Agentname = agent.AgentName,
                                         }).ToList();
            //query syntax
            var result = from client in clientsId 
                         join agent in agentsId
                         on client.ClientId equals agent.AgentId
                         select new
                         {
                             Clientname = client.ClientName,
                             Agentname = agent.AgentName,
                         };

            groupedAgentByClientId.ForEach(agentsWithClients =>
            {
                Console.WriteLine($"Client Name: {agentsWithClients.Clientname + "\nAgent Name:"  + agentsWithClients.Agentname}.");
            });
        }
        static void OrdersByCustomers(List<Client> clients, List<Order> orders)
        {
            var ordersByClients = clients.GroupJoin(orders, clt => clt.ClientId, ord => ord.StockNumber, (clt, ord) => new
            {
                ClientName = clt.ClientName,
                ProductName = ord,
                Price = ord,
            }).ToList();

            foreach (var item in ordersByClients)
            {
                Console.WriteLine($"\nClient Name: {item.ClientName}.");
                foreach (var c in item.ProductName)
                {
                    Console.WriteLine($"Product Name: {c.ProductName + "\nPrice:"  + c.Price}.");
                }
            }
        }
    }

}