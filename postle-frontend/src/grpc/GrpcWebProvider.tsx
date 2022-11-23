import React from "react";
import { GrpcWebContext } from "./GrpcWebContext";

interface GrpcWebProviderProps {
  address: string;
}

function GrpcWebProvider({
  address,
  children,
}: React.PropsWithChildren<GrpcWebProviderProps>) {
  return (
    <GrpcWebContext.Provider value={{ address }}>
      {children}
    </GrpcWebContext.Provider>
  );
}

export {GrpcWebProvider};
