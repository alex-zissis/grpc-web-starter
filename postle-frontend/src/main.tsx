import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { GrpcWebProvider } from "./grpc";
import "./index.css";

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <GrpcWebProvider address="http://localhost:8080">
      <App />
    </GrpcWebProvider>
  </React.StrictMode>
);
