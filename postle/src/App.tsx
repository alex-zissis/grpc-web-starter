import { useEffect, useState } from 'react';
import reactLogo from './assets/react.svg';
import {GreeterClient} from 'dev.zico.protobuf/src/HelloWorldServiceClientPb';
import './App.css'
import { HelloRequest } from 'dev.zico.protobuf/src/HelloWorld_pb';

function App() {
  const [count, setCount] = useState(0)

  useEffect(() => {
    const greeter = new GreeterClient("http://localhost:8080", {}, {});
    const request = new HelloRequest().setName("Alex");

    greeter.sayHello(request, {}).then(reply => {
      console.log(reply.toObject());
    }).catch(console.error);
  }, []);

  return (
    <div className="App">
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src="/vite.svg" className="logo" alt="Vite logo" />
        </a>
        <a href="https://reactjs.org" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </div>
  )
}

export default App
