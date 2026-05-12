using System;
using System.Collections.Generic;
using System.Linq;

namespace Ahorcado
{
    public class Juego
    {
        // Variables de estado del juego (Clase Dios)
        private List<string> _palabras = new()
        {
            "arquitectura", "interfaz", "polimorfismo", "encapsulamiento", "herencia"
        };
        private string _palabraSecreta;
        private List<char> _letrasUsadas;
        private int _intentosRestantes;

        public Juego()
        {
            var random = new Random();
            _palabraSecreta = _palabras[random.Next(_palabras.Count)];
            _letrasUsadas = new List<char>();
            _intentosRestantes = 6;
        }

        public void Jugar()
        {
            Console.Clear();
            Console.WriteLine("=== AHORCADO ===");

            while (_intentosRestantes > 0)
            {
                MostrarTablero();

                if (VerificarVictoria())
                {
                    Console.WriteLine($"\n¡Ganaste! La palabra era: {_palabraSecreta}");
                    if (PreguntarOtraVez())
                    {
                        new Juego().Jugar();
                    }
                    return;
                }

                Console.Write("\nIngresa una letra: ");
                string entrada = Console.ReadLine();

                if (string.IsNullOrEmpty(entrada)) continue;

                char letra = entrada.ToLower()[0];

                if (_letrasUsadas.Contains(letra))
                {
                    Console.WriteLine("Ya usaste esa letra. Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                _letrasUsadas.Add(letra);

                if (!_palabraSecreta.Contains(letra))
                {
                    _intentosRestantes--;
                }
            }

            MostrarTablero();
            Console.WriteLine($"\nPerdiste. La palabra era: {_palabraSecreta}");
            if (PreguntarOtraVez())
            {
                new Juego().Jugar();
            }
        }

        private bool VerificarVictoria()
        {
            foreach (char c in _palabraSecreta)
            {
                if (!_letrasUsadas.Contains(c)) return false;
            }
            return true;
        }

        private void MostrarTablero()
        {
            Console.Clear();
            MostrarAhorcado();
            Console.WriteLine($"Intentos restantes: {_intentosRestantes}");
            Console.WriteLine($"Letras usadas: {string.Join(", ", _letrasUsadas)}");
            Console.Write("Palabra: ");

            foreach (char c in _palabraSecreta)
            {
                Console.Write(_letrasUsadas.Contains(c) ? $"{c} " : "_ ");
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
                "  -----\n  |   |\n      |\n      |\n      |\n      |\n=========", // 0 fallos
                "  -----\n  |   |\n  O   |\n      |\n      |\n      |\n=========", // 1 fallo
                "  -----\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========", // 2 fallos
                "  -----\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========", // 3 fallos
                "  -----\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========", // 4 fallos
                "  -----\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========", // 5 fallos
                "  -----\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="  // 6 fallos
            };

            // Muestra la etapa según los fallos (6 - intentos)
            Console.WriteLine(etapas[6 - _intentosRestantes]);
        }
    }
}