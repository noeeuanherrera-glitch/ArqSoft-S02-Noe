namespace Ahorcado
{
    public class MotorAhorcado
    {
        public string PalabraSecreta { get; }
        public List<char> LetrasUsadas { get; }
        public int IntentosRestantes { get; private set; }

        public MotorAhorcado(string palabra)
        {
            PalabraSecreta = palabra.ToLower();
            LetrasUsadas = new List<char>();
            IntentosRestantes = 6;
        }

        public bool ProcesarLetra(char letra)
        {
            letra = char.ToLower(letra);
            if (LetrasUsadas.Contains(letra)) return false;

            LetrasUsadas.Add(letra);

            if (!PalabraSecreta.Contains(letra))
            {
                IntentosRestantes--;
            }
            return true;
        }

        public bool JugadorGano()
        {
            foreach (char c in PalabraSecreta)
            {
                if (!LetrasUsadas.Contains(c)) return false;
            }
            return true;
        }

        public bool JugadorPerdio() => IntentosRestantes <= 0;
    }
}