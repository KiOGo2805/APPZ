using System;
using System.Collections.Generic;

namespace BookingApp.Pools
{
    /// <summary>
    /// Представляє об'єкт, який повторно використовується в пулі: квитанцію або чек замовлення.
    /// </summary>
    public class OrderTicket
    {
        /// <summary>
        /// Отримує або задає ідентифікатор чека.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Отримує або задає деталі замовлення, що друкуються на чеку.
        /// </summary>
        public string OrderDetails { get; set; } = string.Empty;

        /// <summary>
        /// Очищує стан об'єкта перед поверненням у пул.
        /// </summary>
        public void Reset()
        {
            OrderDetails = string.Empty;
        }

        /// <summary>
        /// Виводить інформацію про чек у консоль.
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"[OrderTicket] Друк чека #{Id}. Інформація: {OrderDetails}");
        }
    }

    /// <summary>
    /// Представляє пул об'єктів для управління життєвим циклом чеків.
    /// </summary>
    public class OrderTicketPool
    {
        private readonly Queue<OrderTicket> _availableTickets = new Queue<OrderTicket>();
        private int _counter = 1;

        /// <summary>
        /// Отримує вільний чек із пулу або створює новий, якщо вільних об'єктів немає.
        /// </summary>
        /// <returns>Чек для використання.</returns>
        public OrderTicket Acquire()
        {
            if (_availableTickets.Count > 0)
            {
                Console.WriteLine("[OrderTicketPool] Перевикористання існуючого чека з пулу.");
                return _availableTickets.Dequeue();
            }

            Console.WriteLine("[OrderTicketPool] Створення нового чека (пул порожній).");
            return new OrderTicket { Id = _counter++ };
        }

        /// <summary>
        /// Повертає чек назад у пул для повторного використання.
        /// </summary>
        /// <param name="ticket">Чек, який потрібно повернути до пулу.</param>
        public void Release(OrderTicket ticket)
        {
            ticket.Reset();
            _availableTickets.Enqueue(ticket);
            Console.WriteLine($"[OrderTicketPool] Чек #{ticket.Id} очищено та повернуто в пул.");
        }
    }
}