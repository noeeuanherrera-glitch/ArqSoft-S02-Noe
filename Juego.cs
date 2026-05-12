using System;
using System.Collections.Generic;
using System.Linq;

namespace Ahorcado
{
    public class Juego
    {
        private readonly IRepositorioPalabras _repositorio;
        private MotorAhorcado _motor;

        public Juego()
        {
            // Usamos la interfaz y la implementación que creaste antes
            _repositorio = new PalabrasEnMemoria();
            string palabra = _repositorio.ObtenerPalabraAleatoria();

            // Inicializamos el motor con la palabra obtenida
            _motor = new MotorAhorcado(palabra);
        }

        public void Jugar()
        {
            while (!_motor.JugadorGano() && !_motor.JugadorPerdio())
            {
                MostrarTablero();

                Console.Write("\nIngresa una letra: ");
                string entrada = Console.ReadLine();

                if (string.IsNullOrEmpty(entrada)) continue;

                char letra = entrada.ToLower()[0];

                if (!_motor.ProcesarLetra(letra))
                {
                    Console.WriteLine("Ya usaste esa letra o es inválida. Presiona cualquier tecla...");
                    Console.ReadKey();
                }
            }

            MostrarTablero();

            if (_motor.JugadorGano())
                Console.WriteLine($"\n¡Ganaste! La palabra era: {_motor.PalabraSecreta}");
            else
                Console.WriteLine($"\nPerdiste. La palabra era: {_motor.PalabraSecreta}");

            if (PreguntarOtraVez())
            {
                // Reiniciamos un nuevo juego
                new Juego().Jugar();
            }
        }

        private void MostrarTablero()
        {
            Console.Clear();
            Console.WriteLine("=== AHORCADO (Refactorizado: Motor Extraído) ===");

            // El dibujo sigue aquí (se irá en el siguiente paso: ConsolaUI)
            MostrarAhorcado();

            // Ahora le pedimos los datos al motor
            Console.WriteLine($"Intentos restantes: {_motor.IntentosRestantes}");
            Console.WriteLine($"Letras usadas: {string.Join(", ", _motor.LetrasUsadas)}");
            Console.Write("Palabra: ");

            foreach (char c in _motor.PalabraSecreta)
            {
                Console.Write(_motor.LetrasUsadas.Contains(c) ? $"{c} " : "_ ");
            }
            Console.WriteLine();
        }

        private bool PreguntarOtraVez()
        {
            Console.Write("\n¿Jugar otra vez? (s/n): ");
            return Console.ReadLine()?.ToLower() == "s";
        }

        private void MostrarAhorcado()
        {
            string[] etapas = new string[]
            {
                "  -----\n  |   |\n      |\n      |\n      |\n      |\n=========",
                "  -----\n  |   |\n  O   |\n      |\n      |\n      |\n=========",
                "  -----\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========",
                "  -----\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========",
                "  -----\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========",
                "  -----\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========",
                "  -----\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="
            };

            Console.WriteLine(etapas[6 - _motor.IntentosRestantes]);
        }
    }
}