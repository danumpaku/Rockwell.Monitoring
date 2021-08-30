Ejecutar la aplicacion localmente:

1. Descargue el repositorio
2. Ejecutar localmente los 3 servicios de la solución Rockwell.Monitoring:
 - Rockwell.Monitoring.Scraper
 - Rockwell.Monitoring.Scheduler
 - Rockwell.Monitoring.ExecutionsApi
Para ejecutarlos, utilice el comando "dotnet run" desde el directorio que contiene cada servicio
3. Desde el directorio Rockwell.Monitoring.App ejecute "npm i"
4. Desde el directorio Rockwell.Monitoring.App ejecute "npm start"

Tener en cuenta que el scheduler tiene su JobStore en memoria por ahora, por lo que si se reinicia se cancelan todos los crones anteriores

 