using System;
using Ex03.GarageLogic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public static class ConsoleUI
    {
        public static void Run()
        {
            Vehicle v1 = new Car("mazda", "12345", new FuelEnergy());
            v1.InitWheels("galgal",20);
            v1.InitEnergySource(40);
            
            string[] paramsQuestions = v1.GetParamsQuestions();
            StringBuilder answer = new StringBuilder();

            foreach (string question in paramsQuestions)
            {
                Console.WriteLine(question);
                answer.AppendLine(Console.ReadLine());
            }

            v1.InitParams(answer.ToString());
           Console.WriteLine(v1.ToString());
        }
    }
}
