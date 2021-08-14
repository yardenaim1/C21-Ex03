using System;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    

    public static class ConsoleUI
    {
        public static void Run()
        {
            Vehicle v1 = new Truck("bla", "bla");
            v1.InitWheelsAndEnergy("oba", 5.4f, 20);


            Console.WriteLine(v1.m_Wheels[0].ToString());
        }
    }
}
