﻿using System;
using System.Collections.Generic;

namespace VendingConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var vm = VendingMachine.CreateDefault();
            vm.Run();
        }
    }

    public sealed class VendingMachine
    {
        private readonly List<Item> _items;

        private readonly decimal[] _acceptedValue = [1, 2, 5, 10, 50, 100, 200, 500, 1000];

        private decimal _balance = 0m;
        private decimal _machineMoney = 0m;
        private decimal _collectedMoney = 0m;

        private const string _adminPassword = "1234567890";

        private VendingMachine(List<Item> items) => _items = items;

        public static VendingMachine CreateDefault() => new VendingMachine([
            new Item(1, "Вода 'Святой источник'",  55.00m, 10),
            new Item(2, "Сок 'Добрый'",  40.00m, 6),
            new Item(3, "Кола 'Добрый'", 70.00m, 9),
            new Item(4, "Сникерс",      90.00m, 4),
            new Item(5, "Твикс",    85.00m, 7),
            new Item(6, "Сэндвич с ветчиной и сыром",    110.00m, 8),
            new Item(7, "Энергетик Burn",    170.00m, 3),
            new Item(8, "M&M's",    55.00m, 7),
            new Item(9, "Чипсы 'Lays'",    105.00m, 6),
            new Item(10, "Мини круассаны с шоколадом",    140.00m, 2),
        ]);

        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"Баланс: {_balance:0.00}");
                Console.WriteLine("1. Показать товары");
                Console.WriteLine("2. Внести деньги");
                Console.WriteLine("3. Купить товар");
                Console.WriteLine("4. Вернуть сдачу");
                Console.WriteLine("5. Администраторский режим");
                Console.WriteLine("0. Выйти из автомата");
                Console.Write("Выберите пункт: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowItems(); break;
                    case "2": MoneyInput(); break;
                    case "3": Purchase(); break;
                    case "4": ReturnTheChange(); break;
                    case "5": AdminMode(); break;
                    case "0":
                        return;
                }
            }
        }

        private void ShowItems()
        {
            Console.WriteLine("{0,-3} | {1,-30} | {2,-8} | {3,-6}", "ID", "Наименование", "Цена", "Количество");
            Console.WriteLine(new string('-', 60));
            foreach (var it in _items)
            {
                Console.WriteLine("{0,-3} | {1,-30} | {2,-8} | {3,-6}", it.Id, it.Name, it.Price, it.Quantity);
            }
        }

        private void MoneyInput()
        {
            Console.WriteLine("Автомат принимает: " + string.Join(", ", _acceptedValue));
            Console.Write("Введите количество денег, которые хотите внести: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal coin) && coin > 0)
            {
                if (Array.Exists(_acceptedValue, c => c == coin))
                {
                    _balance += coin;
                    Console.WriteLine($"Принято {coin:0.00}. Баланс: {_balance:0.00}");
                }
                else
                {
                    Console.WriteLine("Такой номинал не принимается, внести другую купюру");
                }
            }
        }

        private void Purchase()
        {
            ShowItems();
            Console.Write("Введите ID товара (или напишите 'отмена' для выхода): ");
            var input = Console.ReadLine();
            if (input?.Trim().ToLower() == "отмена")
            {
                Console.WriteLine("Операция покупки отменена");
                ReturnTheChange();
                return;
            }
            if (!int.TryParse(input, out int id))
                return;

            var item = _items.Find(i => i.Id == id);
            if (item == null)
            {
                Console.WriteLine("Такого товара нет");
                return;
            }
            if (item.Quantity <= 0)
            {
                Console.WriteLine("Этого товара нет в наличии, выберите другой");
                return;
            }
            if (_balance < item.Price)
            {
                Console.WriteLine("Недостаточно средств");
                return;
            }

            _balance -= item.Price;
            _machineMoney += item.Price;
            item.Quantity--;
            Console.WriteLine($"Вы купили: {item.Name}. Остаток: {_balance:0.00}");
        }

        private void ReturnTheChange()
        {
            if (_balance > 0)
            {
                Console.WriteLine($"Возвращено: {_balance:0.00}");
                _balance = 0m;
            }
            else
            {
                Console.WriteLine("Сдачи нет");
            }
        }

        private void AdminMode()
        {
            Console.Write("Введите пароль администратора: ");
            var password = Console.ReadLine();
            if (password != _adminPassword)
            {
                Console.WriteLine("Введен неверный пароль");
                return;
            }

            while (true)
            {
                Console.WriteLine("\n******* Администраторский режим *******");
                Console.WriteLine("1. Добавить новый товар");
                Console.WriteLine("2. Пополнить количество товара");
                Console.WriteLine("3. Посмотреть баланс автомата");
                Console.WriteLine("4. Собрать все собранные средства");
                Console.WriteLine("0. Выйти из администраторского режима");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите название товара: ");
                        var name = Console.ReadLine();
                        Console.Write("Введите цену товара: ");
                        var price = decimal.Parse(Console.ReadLine()!);
                        Console.Write("Введите количество: ");
                        var qty = int.Parse(Console.ReadLine()!);
                        var newId = _items.Count > 0 ? _items[^1].Id + 1 : 1;
                        _items.Add(new Item(newId, name!, price, qty));
                        Console.WriteLine($"Добавлен товар '{name}' по цене {price:0.00} в количестве {qty}.");
                        break;

                    case "2":
                        ShowItems();
                        Console.Write("Введите ID товара для пополнения: ");
                        if (!int.TryParse(Console.ReadLine(), out int id) || !_items.Exists(i => i.Id == id))
                        {
                            Console.WriteLine("Неверный ID товара");
                            break;
                        }
                        var item = _items.Find(i => i.Id == id)!;
                        Console.Write("Введите количество для добавления: ");
                        var addQty = int.Parse(Console.ReadLine()!);
                        item.Quantity += addQty;
                        Console.WriteLine($"Количество товара '{item.Name}' увеличено до {item.Quantity}.");
                        break;

                    case "3":
                        Console.WriteLine($"Текущий баланс автомата: {_machineMoney:0.00}");
                        break;

                    case "4":
                        _collectedMoney += _machineMoney;
                        Console.WriteLine($"Снято {_machineMoney:0.00}. Общая сумма собранных средств: {_collectedMoney:0.00}");
                        _machineMoney = 0m;
                        break;

                    case "0":
                        Console.WriteLine("Выход из администраторского режима");
                        return;
                }
            }
        }

        private sealed class Item
        {
            public int Id { get; }
            public string Name { get; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

            public Item(int id, string name, decimal price, int quantity)
            {
                Id = id; Name = name; Price = price; Quantity = quantity;
            }
        }
    }
}
