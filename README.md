# Análisis de Violaciones SOLID en Juego.cs

En la implementación inicial (Clase Dios), se identifican las siguientes violaciones:

1. **SRP (Responsabilidad Única):** La clase `Juego` gestiona la lógica, el almacenamiento de palabras y la interfaz de usuario (Consola).
2. **OCP (Abierto/Cerrado):** Para cambiar la fuente de palabras o el idioma, es necesario modificar el código interno de la clase.
3. **DIP (Inversión de Dependencias):** La clase depende directamente de `System.Console` y de una lista estática, lo que impide pruebas unitarias o cambio de interfaz.