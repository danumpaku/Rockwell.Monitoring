# Recomendaciones

* Una breve explicación de que se trata el proyecto.
* Las librerias usadas para pruebas debén estar de `devDependencies`, puedes revisar [aquí](https://stackoverflow.com/questions/18875674/whats-the-difference-between-dependencies-devdependencies-and-peerdependencies)
* no es necesario un gran export al final, puedes hacerlo al definir cada variable
* Aveces es mejor tener una carpeta para el componente App cuando usa a su vez varios archivos /App/index.js /App/App.js /App/App.test.js Logo.png
* La organización de los archivos es un poco rarita para ScrapeJobs, te propongo está, donde se demuestra la jerarquia de las dependencias de algunos componentes
``` tree
SRC\COMPONENTS\SCRAPEJOBS
│   ScrapeJobAdder.js
│   ScrapeJobAdder.module.css
│   ScrapeJobsContainer.js
│   ScrapeJobsContainer.module.css
│   ScrapeJobsTable.js
│   ScrapeJobsTable.module.css
│   
└───ScrapeJobInfo
    │   ScrapeJobInfo.js
    │   
    └───ExecutionInfo
            ExecutionInfo.js
            ExecutionInfo.module.css
```
* uso de index.js, para dar un sentido a la modo de uso de los componentes también es bueno

[Rockwell.Monitoring.App\src\components\ScrapeJobs\index.js](Rockwell.Monitoring.App\src\components\ScrapeJobs\index.js)

```js
export { default as ScrapeJobs } from "./ScrapeJobsContainer";
```

[Rockwell.Monitoring.App\src\App.js](Rockwell.Monitoring.App\src\App.js)

```js
import ScrapeJobs from './components/ScrapeJobs';
...
<main className="App-main">
    <ScrapeJobs/>
</main>
...
```
