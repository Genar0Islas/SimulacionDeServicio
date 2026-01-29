# SimulacionDeServicio

Manual de Uso

Descargar la carpeta de SimulacionDeServicio.
En el cmd acceder a la ruta en la que se guardo y ejecutar SimulacionDeServicio.exe

Una vez abierto el programa se veran dos espacios para escribir en la esquina superior izquierda
en el de la izquierda se ingresaran los datos de llegada e los clientes
estos se separaran por un espacio ej: 2 5 8 4. Cada espacio significa un valor diferente.

De igual manera en el segundo campo se llenara el espacio para tiempo de servicio, de igual manera
se separaran por espacios los valores.

NOTA: se debera poner un valor adicional en el campo de tiempos de servicio ej: 
Tiempos de llegada: 3 7 3 8 5 
Tiempos de servicio: 5 7 2 4 7 4

Esto debido a que el programa tiene la posicion 0 de los tiempos de llegada como 0 ya que el 
primer cliente llega en el tiempo 0.

Adicionalmente en al esquina superior derecha hay un campo para añadir un porcentaje de mejora
el rango va desde -100 a 100 numeros menores a 0 supondran un aumento en el tiempo de servicio
por ende tardaran mas, nuemros mayores a 0 supondran una reduccion en el tiempo de servicio.

Una ves llenados los campos se precionara el boton "mostrar" en el lateral derecho de la ventana.

Se generaran las tablas y se podran cambiar entre "Datos originales","Mejora del 20%" 
y "Mejora pesonalizada" mediante unas pestañas debajo de los campos.

Al generar las tablas se generaran 3 tablas adicionales indicando los resultados de las tablas
y se dara el porcentaje de reduccion de tiempo de espera.
