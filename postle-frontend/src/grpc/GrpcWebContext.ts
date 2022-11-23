import { createContext } from "react";

interface GrpcWebContextData {
  address: string;
}

const GrpcWebContext = createContext<GrpcWebContextData>({address: ''});

export {GrpcWebContext}
export type {GrpcWebContextData};
