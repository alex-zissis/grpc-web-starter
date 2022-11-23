import { useEffect, useState } from "react";
import reactLogo from "./assets/react.svg";
import { GreeterClient } from "dev.zico.protobuf/src/HelloWorldServiceClientPb";
import { HelloRequest } from "dev.zico.protobuf/src/HelloWorld_pb";
import { useGrpcClient } from "./grpc";
import "./App.css";

function App() {
  const [count, setCount] = useState(0);
  const [message, setMessage] = useState<string>();
  const greeterClient = useGrpcClient(GreeterClient)

  useEffect(() => {
    const request = new HelloRequest().setName("Alex");

    greeterClient
      .sayHello(request, null)
      .then((reply) => {
        setMessage(reply.toObject().message);
      });
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
        <h1>Vite + React + GrpcWeb</h1>
        <h3>{message}</h3>
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
  );
}

export default App;
