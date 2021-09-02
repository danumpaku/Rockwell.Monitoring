import logo from './Logo.png';
import './App.css';
import ScrapeJobs from './components/ScrapeJobs';

const App = () => 
  <div className="App">
    <header className="App-header">
      <img src={logo} className="App-logo" alt="Rockwell Monitoring logo" />
    </header>
    <main className="App-main">
      <ScrapeJobs/>
    </main>
    <footer className="App-footer">
      <p>Prueba Técnica realizada por Daniel Numpaque</p>
    </footer>
  </div>

export default App;
