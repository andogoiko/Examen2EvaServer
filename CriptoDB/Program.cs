﻿using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using Cripto.Models;

namespace EFPrueba
{
    class Program
    {
        static void CrearBD()
        {
            using (var db = new CryptoContext())
            {
                bool deleted = db.Database.EnsureDeleted();
                WriteLine($"Database deleted: {deleted}");
                bool created = db.Database.EnsureCreated();
                WriteLine($"Database created: {created}");
            }
        }
        static void CrearMonedasYCarteras()
        {
            using (var db = new CryptoContext())
            {

                db.Moneda.RemoveRange(db.Moneda);
                db.Cartera.RemoveRange(db.Cartera);
                db.Contrato.RemoveRange(db.Contrato);
                db.SaveChanges();

                db.Moneda.AddRange(
                    new Moneda { MonedaId = "Bitcoin", Maximo = 80M, Actual = 70M },
                    new Moneda { MonedaId = "Etherum", Maximo = 70M, Actual = 60M },
                    new Moneda { MonedaId = "Litecoin", Maximo = 60M, Actual = 60M },
                    new Moneda { MonedaId = "Cardano", Maximo = 50M, Actual = 40M },
                    new Moneda { MonedaId = "Polkadot", Maximo = 40M, Actual = 40M },
                    new Moneda { MonedaId = "Stellar", Maximo = 30M, Actual = 20M },
                    new Moneda { MonedaId = "Dogecoin", Maximo = 20M, Actual = 20M },
                    new Moneda { MonedaId = "ShibaInu", Maximo = 10M, Actual = 10M }
                );
                db.SaveChanges();

                db.Cartera.AddRange(
                    new Cartera { CarteraId = 1, Nombre = "Cartera1", Exchange = "Binance" },
                    new Cartera { CarteraId = 2, Nombre = "Cartera2", Exchange = "Kucoin" },
                    new Cartera { CarteraId = 3, Nombre = "Cartera3", Exchange = "Kraken" },
                    new Cartera { CarteraId = 4, Nombre = "Cartera4", Exchange = "Coinbase" },
                    new Cartera { CarteraId = 5, Nombre = "Cartera5", Exchange = "Binance" },
                    new Cartera { CarteraId = 6, Nombre = "Cartera6", Exchange = "Kucoin" },
                    new Cartera { CarteraId = 7, Nombre = "Cartera7", Exchange = "Binance" }
                );
                db.SaveChanges();
            }
        }

        static void CrearContratos()
        {
            using (var db = new CryptoContext())
            {

                db.Contrato.RemoveRange(db.Contrato);
                db.SaveChanges();

                // Lista de contratos a implementar

                // Creacion de contratos

                db.Contrato.AddRange(
                    // Cartera #1: ("Bitcoin",2),("Litecoin",3),("Polkadot",4)
                    new Contrato { ContratoId = 1, CarteraId = 1, MonedaId = "Bitcoin", Cantidad = 2 },
                    new Contrato { ContratoId = 2, CarteraId = 1, MonedaId = "Litecoin", Cantidad = 3 },
                    new Contrato { ContratoId = 3, CarteraId = 1, MonedaId = "Polkadot", Cantidad = 4 },
                    // Cartera #2: ("Dogecoin",3),("ShibaInu",4),("Litecoin",3)
                    new Contrato { ContratoId = 4, CarteraId = 2, MonedaId = "Dogecoin", Cantidad = 3 },
                    new Contrato { ContratoId = 5, CarteraId = 2, MonedaId = "ShibaInu", Cantidad = 4 },
                    new Contrato { ContratoId = 6, CarteraId = 2, MonedaId = "Litecoin", Cantidad = 3 },
                    // Cartera #3: ("Etherum",4),("Cardano",2),("Stellar",1),("Dogecoin",4)
                    new Contrato { ContratoId = 7, CarteraId = 3, MonedaId = "Etherum", Cantidad = 4 },
                    new Contrato { ContratoId = 8, CarteraId = 3, MonedaId = "Cardano", Cantidad = 2 },
                    new Contrato { ContratoId = 9, CarteraId = 3, MonedaId = "Stellar", Cantidad = 1 },
                    new Contrato { ContratoId = 10, CarteraId = 3, MonedaId = "Dogecoin", Cantidad = 4 },
                    // Cartera #4: ("Bitcoin",2),("ShibaInu",3),("Stellar",4),("Litecoin",3)
                    new Contrato { ContratoId = 11, CarteraId = 4, MonedaId = "Bitcoin", Cantidad = 2 },
                    new Contrato { ContratoId = 12, CarteraId = 4, MonedaId = "ShibaInu", Cantidad = 3 },
                    new Contrato { ContratoId = 13, CarteraId = 4, MonedaId = "Stellar", Cantidad = 4 },
                    new Contrato { ContratoId = 14, CarteraId = 4, MonedaId = "Litecoin", Cantidad = 3 },
                    // Cartera #5: ("Polkadot",3),("Cardano",1)
                    new Contrato { ContratoId = 15, CarteraId = 5, MonedaId = "Polkadot", Cantidad = 3 },
                    new Contrato { ContratoId = 16, CarteraId = 5, MonedaId = "Cardano", Cantidad = 1 },
                    // Cartera #6: ("Etherum",4),("Litecoin",2),("Polkadot",1)
                    new Contrato { ContratoId = 17, CarteraId = 6, MonedaId = "Etherum", Cantidad = 4 },
                    new Contrato { ContratoId = 18, CarteraId = 6, MonedaId = "Litecoin", Cantidad = 2 },
                    new Contrato { ContratoId = 19, CarteraId = 6, MonedaId = "Polkadot", Cantidad = 1 },
                    // Cartera #7: ("ShibaInu",2),("Stellar",4)
                    new Contrato { ContratoId = 20, CarteraId = 7, MonedaId = "ShibaInu", Cantidad = 2 },
                    new Contrato { ContratoId = 21, CarteraId = 7, MonedaId = "Stellar", Cantidad = 4 }
                );


                db.SaveChanges();
            }
        }

        static void ConsultarCriptoDB()
        {
            using (var db = new CryptoContext())
            {
                // 0 Ejemplo
                WriteLine("0.- Ejemplo de presentación de datos anónimos");
                var list0 = new string[] {
                                "Brachiosaurus",
                                "Amargasaurus",
                                "Mamenchisaurus"
                            }
                            .Select((d, i) => new
                            {
                                id = i,
                                nombre = d
                            })
                            .ToList();
                list0.ForEach(Console.WriteLine);

                // 1 donete
                WriteLine("1.- Monedas con valor actual superior a 50€ ordenadas alfabéticamente");
                var list1 = db.Moneda.Where(m => m.Actual > 50).OrderBy(m => m.MonedaId).ToList();
                list1.ForEach(Console.WriteLine);

                // 1
                WriteLine("2.- Carteras con más de 2 monedas contratadas");
                var list2 = db.Contrato.GroupBy(c => c.CarteraId).Select(f => new
                {
                    cartera = f.Key,
                    total = f.Count()
                }).Where(f => f.total > 2).ToList();
                list2.ForEach(Console.WriteLine);

                // 1
                WriteLine("3.- Exchanges ordenados por números de carteras");
                var list3 = db.Cartera.GroupBy(c => c.Exchange).Select(f => new
                {
                    exchange = f.Key,
                    carteras = f.Count()
                }).OrderByDescending(f => f.carteras).ToList();
                list3.ForEach(Console.WriteLine);

                // 2
                WriteLine("4.- Exchanges ordenados por cantidad de monedas");
                var list4 = db.Cartera.Where(c => true)
                            .SelectMany(c => c.Contratos, (c, con) => new
                            {
                                exchange = c.Exchange,
                                moneda = con.MonedaId

                            }).GroupBy(c => c.exchange).Select(f => new
                            {
                                exchange = f.Key,
                                TotalCarteras = f.Count()
                            }).OrderByDescending(f => f.TotalCarteras).ToList();
                list4.ForEach(Console.WriteLine);


                // 2
                WriteLine("5.- Monedas en contratos ordenadas por valor total actual");
                var list5 = db.Moneda.Where(m => true)
                .SelectMany(m => m.Contrataciones, (m, con) => new
                {
                    Moneda = m.MonedaId,
                    Contrato = con.ContratoId,
                    valorContrato = (m.Actual * con.Cantidad)
                }).OrderBy(m => m.Moneda).ToList();
                list5.ForEach(Console.WriteLine);

                // 2
                WriteLine("6.- Monedas en contratos ordenadas por valor actual total en todos los contratos");
                var list6 = list5.GroupBy(m => m.Moneda).Select(f => new
                {
                    Moneda = f.Key,
                    valorTotal = f.Sum(f => f.valorContrato)
                }).OrderByDescending(f => f.valorTotal).ToList();
                list6.ForEach(Console.WriteLine);

                // 2
                WriteLine("7.- Idem contando en cuantos contratos aparecen y ordenado por número de contratos");
                var list7 = db.Moneda.Where(m => true)
                .SelectMany(m => m.Contrataciones, (m, con) => new
                {
                    Moneda = m.MonedaId,
                    Contrato = con.ContratoId,
                    valorContrato = (m.Actual * con.Cantidad)
                }).GroupBy(m => m.Moneda).Select(f => new
                {
                    Moneda = f.Key,
                    valorTotal = f.Sum(f => f.valorContrato),
                    Contratos = f.Count()
                }).OrderByDescending(f => f.Contratos).ToList();
                list7.ForEach(Console.WriteLine);

                // 3 Se que hay que juntar las tablas mediante inner join y luego hacer lo de la anterior query, pero no lo he conseguido
                WriteLine("8.- Idem pero con Exchanges ordenados por valor total");
                var list8 = db.Moneda.SelectMany(m => m.Contrataciones, (m, con) => new
                {
                    Moneda = m.MonedaId,
                    Contrato = con.ContratoId,
                    valorContrato = (m.Actual * con.Cantidad)
                }).Select(f => f).GroupBy(m => m.Moneda).Select(f => new
                {
                    Moneda = f.Key,
                    valorTotal = f.Sum(f => f.valorContrato),
                    Contratos = f.Count()
                }).ToList();
                list8.ForEach(Console.WriteLine);

                // 3 no me ha dado tiempo a esta
                var porcentaje = 90M;
                WriteLine($"9.- Las Contratos y Monedas de Binance con monedas cuyo valor actual es inferior al {porcentaje}% del valor máximo");
                var list9 = db.Cartera.ToList();
                list9.ForEach(Console.WriteLine);

            }
        }
        static void Main(string[] args)
        {
            CrearBD();
            CrearMonedasYCarteras();
            CrearContratos();
            ConsultarCriptoDB();
        }
    }
}
