using System;
using BookingApp.Models;
using BookingApp.Composite;
using BookingApp.Flyweight;
using BookingApp.Decorators;
using BookingApp.Proxies;

namespace BookingApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("=== ТЕСТУВАННЯ DECORATOR ===\n");

            IReservationComponent myReservation = new BaseReservation();
            

            myReservation = new TableDecorationDecorator(myReservation);
            

            myReservation = new LiveMusicDecorator(myReservation);


            Console.WriteLine("\n--- Підсумковий рахунок ---");
            Console.WriteLine($"Опис: {myReservation.GetDescription()}");
            Console.WriteLine($"Загальна сума: {myReservation.GetCost()} грн.\n");



            Console.WriteLine("\n=== ТЕСТУВАННЯ PROXY ===\n");

            int targetReservationId = 1042;


            Console.WriteLine("--- Спроба скасування: Звичайний користувач ---");
            IReservationManager userProxy = new ReservationManagerProxy("Guest");
            userProxy.CancelReservation(targetReservationId);

            Console.WriteLine("\n----------------------------------\n");

            Console.WriteLine("--- Спроба скасування: Адміністратор ---");
            IReservationManager adminProxy = new ReservationManagerProxy("Admin");
            adminProxy.CancelReservation(targetReservationId);

            Console.ReadLine();

            
        }
    }
}